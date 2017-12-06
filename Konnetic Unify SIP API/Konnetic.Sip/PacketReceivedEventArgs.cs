/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Konnetic.Sip
{
    /// <summary>
    /// This class provides data for <b>OnPacketReceived</b> event.
    /// </summary>
    internal class PacketReceivedEventArgs : EventArgs
    {
        #region Fields

        private IPEndPoint _remoteEndPoint;
        private Socket _socket;
        private StreamReader _data;
        private TransportType _transport;

        #endregion Fields

        #region Properties

        public StreamReader Data
        {
            get { return _data; }
        }

        public IPEndPoint LocalEndPoint
        {
            get { return (IPEndPoint)_socket.LocalEndPoint; }
        }

        public IPEndPoint RemoteEndPoint
        {
            get { return _remoteEndPoint; }
        }

        public TransportType Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }

        internal Socket @Socket
        {
            get { return _socket; }
        }

        #endregion Properties

        #region Constructors

        internal PacketReceivedEventArgs(Socket socket, IPEndPoint remoteEndPoint, StreamReader data, TransportType transport)
        {
            _socket = socket;
            _remoteEndPoint = remoteEndPoint;
            _data = data;
            _transport = transport;
        }

        #endregion Constructors

        #region Methods

        public void SendResponse(byte[] data, int offset, int count)
        {
            if(data == null)
                {
                throw new ArgumentNullException("data");
                }

            _socket.Send(data, offset, count, SocketFlags.None);
        }

        #endregion Methods
    }
}