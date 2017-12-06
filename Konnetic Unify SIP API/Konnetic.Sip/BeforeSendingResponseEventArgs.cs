/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public class BeforeSendingResponseEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Response _response;

        /// <summary>
        /// 
        /// </summary>
        private bool _cancel;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeforeSendingResponseEventArgs"/> is cancel.
        /// </summary>
		/// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _cancel; }
              set { _cancel = value; }
        }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public Response Response
        {
            get { return _response; }
              set { _response = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeSendingResponseEventArgs"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        internal BeforeSendingResponseEventArgs(Response response )
        {
            _response = response;
        }

        #endregion Constructors
    }
}