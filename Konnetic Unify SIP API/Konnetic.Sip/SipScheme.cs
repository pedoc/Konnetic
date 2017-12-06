/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;

namespace Konnetic.Sip
{
    #region Enumerations

    /// <summary>
    /// 
    /// </summary>
    public enum SipScheme
    {
        /// <summary>
        /// Is a Sip message. Not secure.
        /// </summary>
        [Description("sip")]
        Sip = 0,
        /// <summary>
        /// Is a Sip message. Secure transport.
        /// </summary>
        [Description("sips")]
        Sips = 1,
        /// <summary>
        /// Is a an unknown scheme.
        /// </summary>
        [Description("unknown")]
        Unknown = 2
    }

    #endregion Enumerations
}