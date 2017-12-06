/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// An abstract class used as a base class for other formatters, (for example Konnetic.Sip.Headers.RequestMessageFormatter).
    /// </summary>
    public abstract class MessageFormatter
    {
        #region Fields

        /// <summary>
        /// The Konnetic.Sip.Headers.SipMessage associated with the formatter.
        /// </summary>
        private SipMessage _message;

        /// <summary>
        /// Indicates whether mutiple field headers should be combined into one HeaderField. The resulting output is condensed.
        /// </summary>
        private bool _combineMultipleValues;

        /// <summary>
        /// Indicates whether the formatter should attempt to format the message using the short form for names.
        /// </summary>
        private bool _useCompactForm;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the formatter should combine multiple values if possible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if combine multiple values is on; otherwise, <c>false</c>.
		/// </value>
        public bool CombineMultipleValues
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _combineMultipleValues; }
            set { _combineMultipleValues = value; }
        }

        /// <summary>
        /// Gets Konnetic.Sip.Headers.SipMessage associated with the formatter.
        /// </summary>
        /// <value>The Konnetic.Sip.Headers.SipMessage associated with the formatter.</value>
        public SipMessage Message
        {
            get{return _message;}
        }

        /// <summary>
        /// Gets or sets a value indicating whether the formatter should attempt to format names using the short form.
        /// </summary>
		/// <value><c>true</c> if the formatter uses the short form; otherwise, <c>false</c>.</value>
        public bool UseCompactForm
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _useCompactForm; }
            set { _useCompactForm = value; }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageFormatter"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        protected MessageFormatter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageFormatter"/> class.
        /// </summary>
        /// <param name="message">A Konnetic.Sip.Headers.SipMessage to associated with the formatter.</param>
        protected MessageFormatter(SipMessage message)
        {
            PropertyVerifier.ThrowOnNullArgument(message, "message");
            _message = message;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Verifies whether the specified byte array contains a valid SipMessage item
        /// </summary>
        /// <param name="message">A byte array to check.</param>
        /// <returns>true, if the byte array contains a valid SIP Message, otherwise false.</returns>
        public abstract bool CanRead(byte[] message);

        /// <summary>
        /// Reads in a Konnetic.Sip.Headers.SipMessage from the specified byte array.
        /// </summary>
        /// <param name="stream">A byte array ro read.</param>
        /// <returns>A Konnetic.Sip.Headers.SipMessage created from the byte array.</returns>
        public abstract SipMessage ReadFrom(byte[] stream);

        /// <summary>
        /// Writes the SipMessage associated with the formatter to the specified byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public abstract void WriteTo(out byte[] stream);

        /// <summary>
        /// Writes the SipMessage to the specified byte array.
        /// </summary>
        /// <param name="message">A Konnetic.Sip.Headers.SipMessage to write out to a byte array.</param>
        /// <param name="stream">A byte array to populate from the SipMessage</param>
        public abstract void WriteTo(SipMessage message, byte[] stream);

        /// <summary>
        /// Associates a SipMessage instance with the MessageFormatter.
        /// </summary>
        /// <param name="message">A Konnetic.Sip.Headers.SipMessage to associated with the formatter.</param>
        protected internal virtual void SetMessage(SipMessage message)
        {
            PropertyVerifier.ThrowOnNullArgument(message, "message");
            _message = message;
        }

        #endregion Methods
    }
}