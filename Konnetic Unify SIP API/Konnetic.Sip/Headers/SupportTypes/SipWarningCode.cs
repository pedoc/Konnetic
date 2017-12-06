/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;

namespace Konnetic.Sip.Headers
{
    #region Enumerations

    /// <summary>
    /// The Warning general-header field is used to carry additional information about the status or transformation of a message which might not be reflected in the message.
    /// </summary>
    public enum SipWarningCode
		{
        /// <summary>
        /// The default enumeration.
        /// </summary>
		[DescriptionAttribute("Unknown Code.")]
		None = 0,
        /// <summary>
        /// One or more network protocols contained in the session description are not available.
        /// </summary>
        [DescriptionAttribute("One or more network protocols contained in the session description are not available.")]
        IncompatibleNetworkProtocol = 300,
        /// <summary>
        /// One or more network address formats contained in the session description are not available.
        /// </summary>
        [DescriptionAttribute("One or more network address formats contained in the session description are not available.")]
        IncompatibleNetworkAddressFormats = 301,
        /// <summary>
        /// One or more transport protocols described in the session description are not available.
        /// </summary>
        [DescriptionAttribute("One or more transport protocols described in the session description are not available.")]
        IncompatibleTransportProtocol = 302,
        /// <summary>
        /// One or more bandwidth measurement units contained in the session description were not understood.
        /// </summary>
        [DescriptionAttribute("One or more bandwidth measurement units contained in the session description were not understood.")]
        IncompatibleBandwidthUnits = 303,
        /// <summary>
        /// One or more media types contained in the session description are not available.
        /// </summary>
        [DescriptionAttribute("One or more media types contained in the session description are not available.")]
        MediaTypeNotAvailable = 304,
        /// <summary>
        /// One or more media formats contained in the session description are not available.
        /// </summary>
        [DescriptionAttribute("One or more media formats contained in the session description are not available.")]
        IncompatibleMediaFormat = 305,
        /// <summary>
        /// One or more of the media attributes in the session description are not supported.
        /// </summary>
        [DescriptionAttribute("One or more of the media attributes in the session description are not supported.")]
        AttributeNotUnderstood = 306,
        /// <summary>
        /// A parameter other than those listed above was not understood.
        /// </summary>
        [DescriptionAttribute("A parameter other than those listed above was not understood.")]
        SessionDescriptionParameterNotUnderstood = 307,
        /// <summary>
        /// The site where the user is located does not support multicast.
        /// </summary>
        [DescriptionAttribute("The site where the user is located does not support multicast.")]
        MulticastNotAvailable = 330,
        /// <summary>
        /// The site where the user is located does not support unicast communication.
        /// </summary>
        [DescriptionAttribute("The site where the user is located does not support unicast communication.")]
        UnicastNotAvailable = 331,
        /// <summary>
        /// The bandwidth specified in the session description or defined by the media exceeds that known to be available.
        /// </summary>
        [DescriptionAttribute("The bandwidth specified in the session description or defined by the media exceeds that known to be available.")]
        InsufficientBandwidth = 370,
        /// <summary>
        /// The warning text can include arbitrary information to be presented to a human user or logged.
        /// </summary>
        [DescriptionAttribute("The warning text can include arbitrary information to be presented to a human user or logged.")]
        MiscellaneousWarning = 399
    }

    #endregion Enumerations
}