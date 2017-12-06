/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public static class RequestFactory
    {
        #region Methods

        /// <summary>
        /// Creates the request.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Request CreateRequest(string name)
        {
            return new Invite();
        }

        /// <summary>
        /// Creates the request.
        /// </summary>
        /// <param name="requestLine">The request line.</param>
        /// <returns></returns>
        public static Request CreateRequest(RequestLineHeaderField requestLine)
        {
            Request r;
            switch(requestLine.Method)
                {
                case "INVITE":
                    r = new Invite();
                    break;
                default:
                    r = new Invite();
                    break;
                }

            r.RequestLine = requestLine;
            return r;
        }

        #endregion Methods
    }
}