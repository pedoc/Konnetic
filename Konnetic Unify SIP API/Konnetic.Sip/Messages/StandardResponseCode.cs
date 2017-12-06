/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Konnetic.Sip.Messages
{
    #region Enumerations

    /// <summary>
    /// 
    /// </summary>
    public enum StandardResponseCode
    {
        /// <summary>
        ///
        /// </summary>
        None=0,
        /// <summary>
        ///
        /// </summary>
        Trying=100,
        /// <summary>
        ///
        /// </summary>
        Ringing=180,
        /// <summary>
        ///
        /// </summary>
        CallIsBeingForwarded=181,
        /// <summary>
        ///
        /// </summary>
        Queued=182,
        /// <summary>
        ///
        /// </summary>
        SessionProgress=183,
        /// <summary>
        ///
        /// </summary>
        Ok=200,
        /// <summary>
        ///
        /// </summary>
        MultipleChoices=300,
        /// <summary>
        ///
        /// </summary>
        MovedPermanently=301,
        /// <summary>
        ///
        /// </summary>
        MovedTemporarily=302,
        /// <summary>
        ///
        /// </summary>
        UseProxy=305,
        /// <summary>
        ///
        /// </summary>
        AlternativeService=380,
        /// <summary>
        ///
        /// </summary>
        BadRequest=400,
        /// <summary>
        ///
        /// </summary>
        Unauthorized=401,
        /// <summary>
        ///
        /// </summary>
        PaymentRequired=402,
        /// <summary>
        ///
        /// </summary>
        Forbidden=403,
        /// <summary>
        ///
        /// </summary>
        NotFound=404,
        /// <summary>
        ///
        /// </summary>
        MethodNotAllowed=405,
        /// <summary>
        ///
        /// </summary>
        NotAcceptableRequest = 406,
        /// <summary>
        ///
        /// </summary>
        ProxyAuthenticationRequired=407,
        /// <summary>
        ///
        /// </summary>
        RequestTimeout=408,
        /// <summary>
        ///
        /// </summary>
        Gone=410,
        /// <summary>
        ///
        /// </summary>
        RequestEntityTooLarge=413,
        /// <summary>
        ///
        /// </summary>
        RequestUriTooLong=414,
        /// <summary>
        ///
        /// </summary>
        UnsupportedMediaType=415,
        /// <summary>
        ///
        /// </summary>
        UnsupportedUriScheme=416,
        /// <summary>
        ///
        /// </summary>
        BadExtension=420,
        /// <summary>
        ///
        /// </summary>
        ExtensionRequired=421,
        /// <summary>
        ///
        /// </summary>
        IntervalTooBrief=423,
        /// <summary>
        ///
        /// </summary>
        TemporarilyUnavailable=480,
        /// <summary>
        ///
        /// </summary>
        CallOrTransactionDoesNotExist=481,
        /// <summary>
        ///
        /// </summary>
        LoopDetected=482,
        /// <summary>
        ///
        /// </summary>
        TooManyHops=483,
        /// <summary>
        ///
        /// </summary>
        AddressIncomplete=484,
        /// <summary>
        ///
        /// </summary>
        Ambiguous=485,
        /// <summary>
        ///
        /// </summary>
        BusyHere=486,
        /// <summary>
        ///
        /// </summary>
        RequestTerminated=487,
        /// <summary>
        ///
        /// </summary>
        NotAcceptableHere=488,
        /// <summary>
        ///
        /// </summary>
        RequestPending=491,
        /// <summary>
        ///
        /// </summary>
        Undecipherable=493,
        /// <summary>
        ///
        /// </summary>
        ServerInternalError=500,
        /// <summary>
        ///
        /// </summary>
        NotImplemented=501,
        /// <summary>
        ///
        /// </summary>
        BadGateway=502,
        /// <summary>
        ///
        /// </summary>
        ServiceUnavailable=503,
        /// <summary>
        ///
        /// </summary>
        ServerTimeout=504,
        /// <summary>
        ///
        /// </summary>
        VersionNotSupported=505,
        /// <summary>
        ///
        /// </summary>
        MessageTooLarge=513,
        /// <summary>
        ///
        /// </summary>
        BusyEverywhere=600,
        /// <summary>
        ///
        /// </summary>
        Decline=603,
        /// <summary>
        ///
        /// </summary>
        DoesNotExistAnywhere=604,
        /// <summary>
        ///
        /// </summary>
        NotAcceptableGlobal = 606
    }

    #endregion Enumerations
}