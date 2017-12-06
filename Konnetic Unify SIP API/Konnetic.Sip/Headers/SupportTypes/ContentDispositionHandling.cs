/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    #region Enumerations

    /// <summary>
    /// Describes how the server should react if it receives a message body whose <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/> or <see cref="T:Konnetic.Sip.Headers.ContentDispositionHeaderField"/> it does not understand.
    /// </summary>
    [Serializable]
    public enum ContentDispositionHandling
    {
    /// <summary>
    /// The default enumeration.
    /// </summary>
        None,
        /// <summary>
        /// The server must ignore the message body.
        /// </summary>
        Optional,
        /// <summary>
        /// The server must return 415 (Unsupported Media Type)
        /// </summary>
        Required
    }

    #endregion Enumerations
}