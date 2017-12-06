/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// Represents a SIP-based call, known as a 'Dialog' in SIP parlance (formally refered to as a Call-Leg). Manages a conversation with one or many correspondents.
    /// </summary>
    public sealed class Dialog
    {
        #region Fields

        /// <summary>
        /// Represents the current state of the call/conversation. Responsible for conveying infomation about the condition of the call/conversation.
        /// </summary>
        private DialogState _state;

        /// <summary>
        /// Represents the collection of message-based transactions between users. Responsible for keeping an historical record of the conversation. 
        /// </summary>
        private Dictionary<string, Transaction> _transactions;

        /// <summary>
        /// Represents the unique Call-ID. Each Dialog is identified by a unique Call-ID, a local tag and remote tag.
        /// </summary>
        /// <remarks>
        /// Call-ID is case-sensitive. A single multimedia conference can give rise to several calls with different Call-IDs, for example, if a user invites a sinlge individual several times to the same conference.
        /// </remarks>
        /// <seealso cref="RFC3261 20.8"/>
        private int _callId;

        /// <summary>
        /// Represents the caller's identifier. Responsible for identifying a particular client and their human understandable display-name.
        /// </summary>
        /// <seealso cref="RFC3261 20.20"/>
        private string _from;

        /// <summary>
        /// Represents the callee's identifier. Responsible for identifying a particular client and their human understandable display-name.
        /// </summary>
        /// <seealso cref="RFC3261 20.39"/>
        private string _to;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the conversation's unique Call-ID.
        /// </summary>
        /// <returnValue>
        /// An integer Call-ID of the conversation. 
        /// </returnValue>
        /// <seealso cref="RFC3261 20.8"/>
        public int CallId
        {
            get { return _callId; }
            set { _callId = value; }
        }

        /// <summary>
        /// Gets or sets the caller's identifier.
        /// </summary>
        /// <value>
        /// A string containing a SIP URI and display name.
        /// </value> 
        /// <seealso cref="RFC3261 20.20"/>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        public string Key
        {
            get {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
            sb.Append(CallId);
            sb.Append("/");
            sb.Append(To);
            sb.Append("/");
            sb.Append(From);
                return sb.ToString();}
        }

        /// <summary>
        /// Gets or sets the current state of the call/conversation.
        /// </summary>
        /// <value>
        /// A SessionState enumeration value.
        /// </value>
        public DialogState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// Gets or sets the callee's identifier.  
        /// </summary>
        /// <value>
        /// A string containing a SIP URI and display name.
        /// </value>
        /// <see cref="RFC3261 20.39"/>
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="to">The contact.</param>
        public Dialog(string to)
            : this()
        {
            To = to;
            ClientTransaction newTransaction = new ClientTransaction(to);

            newTransaction.Init();
            AddTransaction(newTransaction);
        }

        public Dialog(Request request)
            : this()
        {
            InviteServerTransaction newTransaction = new InviteServerTransaction(TransportType.Tcp);

            //newTransaction.Init();
            AddTransaction(newTransaction);
        }

        public Dialog()
            : base()
        {
            //_transactions = new Dictionary<string, Transaction>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a new transaction to the dialog. 
        /// </summary>
        /// <remarks>
        /// If the transaction already exists then it is replaced with the new transaction.
        /// </remarks>
        /// <param name="transaction">A ClientTranaction object representing a new (INVITE) Request</param>
        /// <exception cref="ArgumentNullException">Thrown when <param>transaction</param> is null.</exception>
        public void AddTransaction(Transaction transaction)
        {
        PropertyVerifier.ThrowOnNullArgument(transaction, "transaction");

            _transactions.Add(transaction.Key, transaction);
        }

		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool IsValid()
        {
            return true;
        }

        public void ProcessRequest(Request request)
        {
        }

        public void ProcessResponse(Response request)
        {
        }

        #endregion Methods
    }
}