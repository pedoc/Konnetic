/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// Represents the SIP Transport. Brokers between the underlying OSI Network and Transport layers and the higher level SIP logic.
    /// </summary>	
    public static class TransportServer
    {
        #region Fields

        public static ManualResetEvent clientConnected = new ManualResetEvent(false);

        #endregion Fields

        #region Events

        internal static event PacketReceivedEventHandler OnPacketRecieved;

        #endregion Events

        #region Methods

        public static void Start()
        {
            TcpListener listener = null;
            Int32 port = 5060;
            IPAddress localAddr = IPAddress.Loopback;
            listener = new TcpListener(localAddr, port);

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state)
            {
                listener.Start();
                TcpBeginAcceptSocket(listener);
                }));

            IPEndPoint localEP = new IPEndPoint(localAddr, port);
            UdpClient udpListener = new UdpClient(localEP);

            UdpState s = new UdpState();
            s.EndPoint = localEP;
            s.Client = udpListener;

            udpListener.BeginReceive(new AsyncCallback(UdpMessageArrive), s);
        }

        // Process the client connection.
        public static void TcpAcceptSocketCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            // End the operation and display the received data on the
            //console.
            Socket socket = null;
            NetworkStream ns =null;
            StreamReader reader = null;
            try
                {
                socket = listener.EndAcceptSocket(ar);
                ns = new NetworkStream(socket);
                reader = new StreamReader(ns);
                 


                if(OnPacketRecieved != null)
                    {
                    //OnPacketRecieved(new PacketReceivedEventArgs(socket, (IPEndPoint)socket.RemoteEndPoint, reader));
                    //Only send to one of the delegates. Because we are passing a stream then listeners can consume the stream depriving other listeners of the contents.
                    Delegate[] listeners = OnPacketRecieved.GetInvocationList();
                    if(listeners.Length > 0)
                        {
                        ((PacketReceivedEventHandler)listeners[0]).Invoke(new PacketReceivedEventArgs(socket, (IPEndPoint)socket.RemoteEndPoint, reader, TransportType.Tcp));
                        }
                    }

                }
            finally
                {
                //TODO Should we dispose of these here and now?
                reader.Dispose();
                ns.Dispose();
                socket.Close();
                clientConnected.Set();
                }
        }

        public static void TcpBeginAcceptSocket(TcpListener listener)
        {
            // Set the event to nonsignaled state.
            clientConnected.Reset();

            // Accept the connection.
            // BeginAcceptSocket() creates the accepted socket.
            listener.BeginAcceptSocket(
                new AsyncCallback(TcpAcceptSocketCallback), listener);
            // Wait until a connection is made and processed before
            // continuing.
            clientConnected.WaitOne();
        }

        /// <summary>
        /// UDPs the message arrive.
        /// </summary>
        /// <param name="ar">The ar.</param>
        public static void UdpMessageArrive(IAsyncResult ar)
        {
            UdpClient client = (UdpClient)((UdpState)(ar.AsyncState)).Client;
            IPEndPoint endPoint = (IPEndPoint)((UdpState)(ar.AsyncState)).EndPoint;

            MemoryStream mstream = null;
            StreamReader reader = null;
            try
                {
                mstream = new MemoryStream(client.EndReceive(ar, ref endPoint));
                reader = new StreamReader(mstream);
                if(OnPacketRecieved != null)
                    {
                    Delegate[] listeners = OnPacketRecieved.GetInvocationList();
                    if(listeners.Length > 0)
                        {
						((PacketReceivedEventHandler)listeners[0]).Invoke(new PacketReceivedEventArgs(client.Client, endPoint, reader, TransportType.Udp));
                        }
                    }
                }
            finally
                {
                //TODO Do we dispose here and now?
                reader.Dispose();
                mstream.Dispose();
                client.Close();
                }
        }

        #endregion Methods

        #region Nested Types

        private class UdpState
        {
            #region Fields

            public IPEndPoint EndPoint;
            public UdpClient Client;

            #endregion Fields
        }

        #endregion Nested Types
    }
}