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
    /// Describes how the message body or, for multipart messages, a message body part is to be interpreted by the client or server.
    /// </summary>
    [Serializable]
    public enum DispositionType
    {
    /// <summary>
    /// The default enumeration
    /// </summary>
        None,
        /// <summary>
        /// Indicates that the body part should be displayed or otherwise rendered to the user.
        /// </summary>
        Render,
        /// <summary>
        /// Indicates that the body part describes a session, for either calls or early (pre-call) media.
        /// </summary>
        Session,
        /// <summary>
        /// Indicates that the body part contains an image suitable as an iconic representation of the caller or callee
        /// </summary>
        /// <remarks>The body could be rendered informationally by a user agent when a message has been received, or persistently while a dialog takes place.</remarks>
        Icon,
        /// <summary>
        /// Indicates that the body part contains information, such as an audio clip
        /// </summary>
        Alert,
        /// <summary>
        /// The bodypart is intended to be displayed automatically upon display of the message.
        /// </summary>
        /// <remarks>The body should be rendered by the user agent in an attempt to alert the user to the receipt of a request, generally a request that initiates a dialog; this alerting body could for example be rendered as a ring tone for a phone call after a 180 Ringing provisional response has been sent.</remarks>
        Inline,
        /// <summary> 
        /// Indicates that they are separate from the main body of the mail message, and that their display should not be automatic, but contingent upon some further action of the user.
        /// </summary>
        Attachment
    }

    #endregion Enumerations
}