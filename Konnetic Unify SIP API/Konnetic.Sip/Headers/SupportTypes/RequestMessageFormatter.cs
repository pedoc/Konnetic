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
    /// A class that serializes a Request message instance to and from byte array format.
    /// </summary>
    public class RequestMessageFormatter : MessageFormatter
    {
        #region Events

        /// <summary>
        /// Occurs just before the formatter parses a HeaderField.
        /// </summary>
        public event EventHandler<HeaderFieldEventArgs> BeforeParsing;

        #endregion Events

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestMessageFormatter"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        public RequestMessageFormatter(Request request)
        {
            PropertyVerifier.ThrowOnNullArgument(request, "request");
            SetMessage(request);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Verifies whether the specified byte array contains a valid SipMessage item
        /// </summary>
        /// <param name="message">A byte array to check.</param>
        /// <returns>
        /// true, if the byte array contains a valid SIP Message, otherwise false.
        /// </returns>
		public override bool CanRead(byte[] data)
        {
            //TODO implement
            return true;
        }

        /// <summary>
        /// Reads in a Konnetic.Sip.Headers.SipMessage from the specified byte array.
        /// </summary>
        /// <param name="stream">A byte array ro read.</param>
        /// <returns>
        /// A Konnetic.Sip.Headers.SipMessage created from the byte array.
        /// </returns>
        public override SipMessage ReadFrom(byte[] data)
        {
            //TODO implement
            return new Invite();
        }

        /// <summary>
        /// Writes to.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
		public override void WriteTo(out byte[] data)
        {
		((Request)Message).GetBytes(out data);
        }

        /// <summary>
        /// Writes the SipMessage to the specified byte array.
        /// </summary>
        /// <param name="message">A Konnetic.Sip.Headers.SipMessage to write out to a byte array.</param>
        /// <param name="stream">A byte array to populate from the SipMessage</param>
		public override void WriteTo(SipMessage message, byte[] data)
        {
            PropertyVerifier.ThrowOnNullArgument(message, "message");
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}