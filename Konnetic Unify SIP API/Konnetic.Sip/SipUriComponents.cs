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
    [Flags]
    public enum SipUriComponents
    {
    /// <summary>
    /// 
    /// </summary>
        [DescriptionAttribute("UNKNOWN")]
            None = 0x0,
        /// <summary>
        /// 
        /// </summary>
            [DescriptionAttribute("USERNAME")]
            UserName = 0x1,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("PASSWORD")]
            Password = 0x2,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("HOST")]
            Host = 0x4,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("PORT")]
            Port = 0x8,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("USER")]
            UserParameter = 0x100,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("METHOD")]
            Method = 0x200,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("MADDR")]
            MulticastAddress = 0x400,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("TTL")]
            TimeToLive = 0x800,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("TRANSPORT")]
            Transport = 0x1000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("LR")]
            LooseRouter = 0x2000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("PARAMETERS")]
            Parameters = 0x4000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("HEADERS")]
            Headers = 0x8000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("USERINFO")]
            UserInfo = 0x10000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("ABSOLUTEURI")]
            AbsoluteUri = 0x20000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("SIPREQUESTURI")]
            SipRequestUrl = 0x40000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("HOSTPORT")]
            HostPort = 0x80000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("SCHEME")]
            Scheme = 0x100000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("FRAGMENT")]
            Fragment = 0x200000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("SERIALIZATIONINFOSTRING")]
            SerializationInfoString = 0x400000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("HTTPREQUESTURL")]
            HttpRequestUrl = 0x800000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("SCHEMEANDSERVER")]
            SchemeAndServer = 0x1000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("PATHANDQUERY")]
            PathAndQuery = 0x2000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("PATH")]
            Path = 0x4000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("KEEPDELIMITER")]
            KeepDelimiter = 0x8000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("QUERY")]
            Query = 0x10000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("STRONGAUTHORITY")]
            StrongAuthority = 0x20000000,
            /// <summary>
            /// 
            /// </summary>
            [DescriptionAttribute("STRONGPORT")]
            StrongPort = 0x40000000
    }

    #endregion Enumerations
}