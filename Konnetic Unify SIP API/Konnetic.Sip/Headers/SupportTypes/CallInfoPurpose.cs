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
    /// The purpose of the URI in the <see cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/> is described by the purpose enumeration.
    /// </summary>
    [Serializable]
    public enum CallInfoPurpose
    {
    /// <summary>
    /// Default purpose.
    /// </summary>
        None,
        /// <summary>
        /// The icon enumeration designates an image suitable as an iconic representation of the caller or callee.
        /// </summary>
        Icon,
        /// <summary>
        /// The info enumeration describes the caller or callee in general, for example, through a web page.
        /// </summary>
        Info,
        /// <summary>
        /// The card enumeration provides a business card, for example, in vCard or LDIF formats.
        /// </summary>
        Card
    }

    #endregion Enumerations
}