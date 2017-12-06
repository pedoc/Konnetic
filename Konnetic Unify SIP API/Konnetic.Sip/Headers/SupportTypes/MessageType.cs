/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Konnetic.Sip.Headers
{
    #region Enumerations

    /// <summary>
    /// SIP messages consist of requests from client to server and responses from server to client.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
    ///A request message from a client to a server.
        /// </summary>
        Request,
        /// <summary>
        ///A response message from a server to a client.
        /// </summary>
        Response
    }

    #endregion Enumerations
}