/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.IO;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public static class TransactionRegistry
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private static readonly string[] _supportedMethods = new string[] { "INVITE" };

        /// <summary>
        /// 
        /// </summary>
        private static readonly string[] _supportedScheme = new string[] { "SIP", "SIPS" };

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ClientTransaction> _clientTransactions= new Dictionary<string, ClientTransaction>();

        /// <summary>
        /// 
        /// </summary>
		private static Dictionary<string, Dialog> _dialogs = new Dictionary<string, Dialog>();

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ServerTransaction> _serverTransactions = new Dictionary<string, ServerTransaction>();

        #endregion Fields

        #region Events

        /// <summary>
        /// Occurs when [new request].
        /// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public static event NewRequestEventHandler NewRequest;

        /// <summary>
        /// Occurs when [un matched response].
        /// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public static event UnmatchedResponseEventHandler UnMatchedResponse;

        #endregion Events

        #region Constructors
 

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Registers the client transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public static void RegisterClientTransaction(ClientTransaction transaction)
        {
            if(string.IsNullOrEmpty(transaction.Key))
                {
                throw new SipException("Transaction Key is invalid!");
                }

            if(transaction.IsValid())
                {
                throw new SipException("Transaction is in an invalid state!");
                }

            _clientTransactions.Add(transaction.Key, transaction);
        }

        /// <summary>
        /// Registers the dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        public static void RegisterDialog(Dialog dialog)
        {
            if(string.IsNullOrEmpty(dialog.Key))
                {
                throw new SipException("Dialog Key is invalid!");
                }

            if(dialog.IsValid())
                {
                throw new SipException("Dialog is in an invalid state!");
                }

            _dialogs.Add(dialog.Key, dialog);
        }

        /// <summary>
        /// Registers the server transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public static void RegisterServerTransaction(ServerTransaction transaction )
        {
            if(string.IsNullOrEmpty(transaction.Key))
                {
                throw new SipException("Transaction Key is invalid!");
                }

            if(transaction.IsValid())
                {
                throw new SipException("Transaction is in an invalid state!");
                }

            _serverTransactions.Add(transaction.Key, transaction);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
		TransportServer.OnPacketRecieved += new PacketReceivedEventHandler(PacketRecieved);
        }

        /// <summary>
        /// Gets the request key.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private static string GetRequestKey(Request request)
        {
            string key = request.Via.GetHeaderField(0).Key;
            if(request.Method == SipMethod.Cancel)
                {
                key += "/" + SipMethod.Cancel;
                }
            return key;
        }

        /// <summary>
        /// Matches the client transaction.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private static bool MatchClientTransaction(Response response, out ClientTransaction transaction)
        {
            if(!response.IsValid())
                {
                transaction = null;
                return false;
                }

            string branch = response.Via.GetHeaderField(0).Branch;
            string method = response.CSeq.Method;
            string key = branch + "/" + method;

            bool foundTransaction;
            lock(_clientTransactions)
                {
                foundTransaction = _clientTransactions.TryGetValue(key, out transaction);
                }

            return foundTransaction;
        }

        /// <summary>
        /// Matches the dialog.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        private static bool MatchDialog(Request request, out Dialog dialog)
        {
            if(request.IsValid())
                {
                string callID = request.CallId.CallId;
                string toTag = request.To.Tag;
                string fromTag = request.From.Tag;
                if(callID != null && toTag != null && fromTag != null)
                    {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
                    sb.Append(callID);
                    sb.Append("/");
                    sb.Append(toTag);
                    sb.Append("/");
                    sb.Append(fromTag);  
                    bool foundDialog;
                    lock(_dialogs)
                        {
                        foundDialog = _dialogs.TryGetValue(sb.ToString(), out dialog);
                        }

                    if(foundDialog)
                        {
                        return true;
                        }
                    }
                }
            dialog = null;
            return false;
        }

        /// <summary>
        /// Matches the dialog.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        private static bool MatchDialog(Response response, out Dialog dialog)
        {
            if(response.IsValid())
                {
                string callID = response.CallId.CallId;
                string toTag = response.To.Tag;
                string fromTag = response.From.Tag;
                if(callID != null && toTag != null && fromTag != null)
                    {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
                    sb.Append(callID);
                    sb.Append("/");
                    sb.Append(toTag);
                    sb.Append("/");
                    sb.Append(fromTag);   
                    bool foundDialog;
                    lock(_dialogs)
                        {
                        foundDialog = _dialogs.TryGetValue(sb.ToString(), out dialog);
                        }

                    if(foundDialog)
                        {
                        return true;
                        }
                    }
                }
            dialog = null;
            return false;
        }

        /// <summary>
        /// Matches the server transaction.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private static bool MatchServerTransaction(Request request, out ServerTransaction transaction)
        {
            if(!request.IsValid())
                {
                transaction = null;
                return false;
                }
            if(string.IsNullOrEmpty(request.To.Tag))
                {
                //Ongoing transaction
                string key = GetRequestKey(request);

                bool foundTransaction;
                lock(_serverTransactions)
                    {
                    foundTransaction = _serverTransactions.TryGetValue(key, out transaction);
                    }

                if(foundTransaction)
                    {
                    if(request.Method == SipMethod.Ack)
                        {
                        if(transaction.Method != SipMethod.Invite)
                            {
                            Response t = new Response(request,StandardResponseCode.BadRequest);
                            BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                            TransportClient.Send(t);
                            //TODO Make an event
                            }
                        }
                    else
                        {
                        if(request.Method != transaction.Method)
                            {
                            Response t = new Response(request,StandardResponseCode.BadRequest);
                            BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                            TransportClient.Send(t);
                            //TODO Make an event
                            }
                        }

                    return true;

                    }

                }
            transaction = null;
            return false;
        }

        /// <summary>
        /// Packets the recieved.
        /// </summary>
        /// <param name="e">The <see cref="Konnetic.Sip.PacketReceivedEventArgs"/> instance containing the event data.</param>
        private static void PacketRecieved(PacketReceivedEventArgs e)
        {
            string RLine = e.Data.ReadLine();

            if(RLine == "\r\n")
                {
                //read next line
                RLine = e.Data.ReadLine();
                }

            if(RLine == "\r\n")
                {
                //Received ping
                //Send pong
                e.Socket.Send(new byte[] { (byte)'\r', (byte)'\n' }, System.Net.Sockets.SocketFlags.None);
                }
            else if(RLine.TrimStart().StartsWith("sip/", StringComparison.OrdinalIgnoreCase))
                {
                ProcessResponse(RLine, e.Data, e.Transport);
                }
            else
                {
                ProcessRequest(RLine,e.Data,e.RemoteEndPoint,e.Transport);
                }
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="RLine">The R line.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="transport">The transport.</param>
        private static void ProcessRequest(string RLine, StreamReader reader, System.Net.IPEndPoint endPoint, TransportType transport)
        {
            RequestLineHeaderField requestLine = new RequestLineHeaderField();
            Request request = null;

            try
                {
                requestLine.Parse(RLine);
                request = RequestFactory.CreateRequest(requestLine);
                while(!reader.EndOfStream)
                    {
                    string s = reader.ReadLine();
                    try
                        {
                        request.Headers.Set(HeaderFieldFactory.CreateHeaderFieldFromLine(s));
                        }
                    catch(SipException)
                        {
                        //Just Ignore for now and attempt to process the rest of the headers as per specification
                        //If the request is in an invalid state, then the next check will take care of that.
                        }
                    }
                }
            catch(SipFormatException)
                {
                Response t = new Response(request, StandardResponseCode.BadRequest);
                t.ReasonPhrase = "Bad Request: Not able to parse request";
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!requestLine.IsValid())
                {
                Response t = new Response(request, StandardResponseCode.Ambiguous);
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!requestLine.Version.Equals(SR.GetString(SR.SIPVersionNumber), StringComparison.OrdinalIgnoreCase))
                {
                Response t = new Response(request, StandardResponseCode.VersionNotSupported);
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!StringArrayContains(_supportedMethods, requestLine.Method))
                {
                Response t = new Response(request, StandardResponseCode.MethodNotAllowed);
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!StringArrayContains(_supportedScheme, requestLine.Scheme))
                {
                Response t = new Response(request, StandardResponseCode.UnsupportedUriScheme);
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!request.IsValid())
                {
                Response t = new Response(request, StandardResponseCode.BadRequest);
                t.ReasonPhrase = "Bad Request: Not able to parse request";
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO event to see if user wants to cancel on validation
                }

            //TODO don't compare strings.
            request.Via.GetHeaderField(0).Received = endPoint.ToString();

            Dialog dialog;
            bool found = MatchDialog(request, out dialog);
            if(found)
                {
                dialog.ProcessRequest(request);
                }
            else{

            ServerTransaction serverTran;
            found = MatchServerTransaction(request, out serverTran);

            if(found)
                {
                if(serverTran.Method == SipMethod.Invite)
                    {
                    InviteServerTransaction i = null;
                    try
                        {
                        i = (InviteServerTransaction)serverTran;
                        }
                    catch(SipException ex)
                        {
                        throw new SipException("Could not convert server transaction to an InviteServerTransaction", ex);
                        }
                    try
                        {
                        i.Transport = transport;
                        i.ProcessRequest(request);
                        }
                    catch(SipException)
                        {
                        Response t = new Response(request, StandardResponseCode.ServerInternalError);
                        BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                        TransportClient.Send(t);
                        //TODO Make an event
                        }

                    }
                else
                    {
                    NoneInviteServerTransaction n = null;
                    try{
                     n =(NoneInviteServerTransaction)serverTran;
                        }
                    catch(SipException ex)
                        {
                        throw new SipException("Could not covert server transaction to an NoneInviteServerTransaction", ex);
                        }
                    try
                        {
                        n.Transport = transport;
                        n.ProcessRequest(request);
                        }
                    catch(SipException)
                        {
                        Response t = new Response(request, StandardResponseCode.ServerInternalError);
                        BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                        TransportClient.Send(t);
                        //TODO Make an event
                        }

                    }
                }
            else
                {
                if(NewRequest != null)
                    {
                    NewRequest(new NewRequestReceivedEventArgs(request,transport));
                    }
                }
            }
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="RLine">The R line.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="transport">The transport.</param>
        private static void ProcessResponse(string RLine,  StreamReader reader, TransportType transport)
        {
            StatusLineHeaderField statusLine = new StatusLineHeaderField();
            Response response = new Response();
            try
                {
                statusLine.Parse(RLine);
                response.StatusLine = statusLine;
                while(!reader.EndOfStream)
                    {
                    string s = reader.ReadLine();
                    try
                        {
                        response.Headers.Set(HeaderFieldFactory.CreateHeaderFieldFromLine(s));
                        }
                    catch(SipException)
                        {
                        //Just Ignore for now and attempt to process the rest of the headers as per specification
                        }
                    }
                }
            catch(SipFormatException)
                {
                Response t = new Response(response, StandardResponseCode.BadRequest);
                t.ReasonPhrase = "Bad Request: Not able to parse status line";
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO Make an event
                }

            if(!response.IsValid())
                {
                Response t = new Response(response,StandardResponseCode.BadRequest);
                t.ReasonPhrase = "Bad Request: Not able to parse response";
                BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                TransportClient.Send(t);
                //TODO event to see if user wants to cancel on validation
                }

            Dialog dialog;
            bool found = MatchDialog(response, out dialog);

            if(found)
                {
                dialog.ProcessResponse(response);
                }
                else
                {

            ClientTransaction inviteServerTran;
            found = MatchClientTransaction(response, out inviteServerTran);

            if(found)
                {
                inviteServerTran.ProcessResponse(response);
                }
            else
                {
                    if(UnMatchedResponse != null)
                        {
                        UnMatchedResponse(new UnmatchedResponseEventArgs(response));
                        }
                    }
                }
        }

        /// <summary>
        /// Strings the array contains.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool StringArrayContains(Array a, string value)
        {
            bool contains = false;
            foreach(string s in a)
                {
                if(s.Equals(value))
                    {
                    contains = true;
                    break;
                    }
                }
            return contains;
        }

        #endregion Methods
    }
}