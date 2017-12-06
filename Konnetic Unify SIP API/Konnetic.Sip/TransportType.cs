/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip
{
    #region Enumerations

    //[Flags]
    /// <summary>
    /// 
    /// </summary>
    public enum TransportType
    {
    /// <summary>
    /// 
    /// </summary>
        Unknown = 0,
        /// <summary>
        /// 
        /// </summary>
        Udp,
        /// <summary>
        /// 
        /// </summary>
        Tcp,
        /// <summary>
        /// 
        /// </summary>
        Tls,
        /// <summary>
        /// 
        /// </summary>
        Sctp
    }

    #endregion Enumerations
}