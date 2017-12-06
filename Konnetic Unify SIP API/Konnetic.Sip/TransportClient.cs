/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    public static class TransportClient
    {
        #region Methods
         
		public static void Send(Response message )
        {
            byte[] buffer = null;
            message.GetBytes(out buffer);
            TcpClient client = new TcpClient("localhost", 5060);
            //client.Connect("sip:bob@localhost", 5060);
            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
            client.Close();
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Send(Request message)
        {
            System.Diagnostics.Trace.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            System.IO.FileStream fs = new System.IO.FileStream(@"D:\Business\Development\Konnetic\UnifySipApi\Production\Src\Konnetic Unify SIP API\Konnetic.Sip\Testing\Actual\Output.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);

            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);

            byte[] buffer = null;
            RequestMessageFormatter formatter = new RequestMessageFormatter(message);
            formatter.UseCompactForm = true;
            formatter.CombineMultipleValues = true;
            formatter.WriteTo(out buffer);
            bw.Write(buffer);
            bw.Flush();

            TcpClient client = new TcpClient("localhost", 5060);
            //client.Connect("sip:bob@localhost", 5060);
            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
            client.Close();
        }

        #endregion Methods
    }
}