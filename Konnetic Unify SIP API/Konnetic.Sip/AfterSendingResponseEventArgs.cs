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
    public class AfterSendingResponseEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Response _response;

        #endregion Fields

        #region Properties

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
        /// Initializes a new instance of the <see cref="AfterSendingResponseEventArgs"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        internal AfterSendingResponseEventArgs(Response response)
        {
            _response = response;
        }

        #endregion Constructors
    }
}