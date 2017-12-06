/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <remarks>
    /// Represents a SIP ClientTransaction. Each conversation is made up of one or more transactions (an initial SIP message followed by responses, other requests or errors). Manages the GetMessage Exchange Pattern for the client side of the transaction.
    /// </remarks>
    public class ClientTransaction : Transaction
    {
        #region Fields

        private ClientTransactionState _processState;
       // private SipMethod _method;

        /// <summary>
        /// Represents the callee's identifier. Responsible for identifying address and display name of the callee.
        /// </summary>
        private string _to;

        #endregion Fields

        #region Properties

        public ClientTransactionState ProcessState
        {
            get { return _processState; }
            private set { _processState = value; }
        }

        /// <summary>
        /// Gets or sets the callee identifier and display name.
        /// </summary>
        public string To
        {
            get { return _to; }
              set { _to = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Represents the constructor for the ClientTransaction type. Responsible for 
        /// </summary>
        /// <param name="to"></param>
        public ClientTransaction(string to)
        {
            To = to;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Begins the transaction. 
        /// </summary>
        /// <remarks>
        /// Client transactions initate a request (In SIP 2.0 this is an INVITE by default). The invite is automatically sent to the transport for delivery. 
        /// </remarks>
        public void Init()
        {
            //TODO: Get Initatiors address from config
            TransportClient.Send(new Invite(To, "Jim"));
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            return true;
        }

        public void ProcessResponse(Response response)
        {
            lock(this)
            {
            //switch(ProcessState)
            //    {
            //    case InviteTransactionSate.Calling:
            //        if(((int)response.Code) <= 200) { }
            //    else
            //                {
            //                SendAcknowledgement(response);

            //                }

            //    case InviteTransactionSate.Proceeding:

            //    }
            }
        }

        private void SendAcknowledgement(Response response)
        {
        }

        #endregion Methods
    }
}