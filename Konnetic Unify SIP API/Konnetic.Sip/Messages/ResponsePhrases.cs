/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ResponsePhrases
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Int16, string> Phrases = new Dictionary<short, string>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ResponsePhrases"/> class.
        /// </summary>
        static ResponsePhrases()
        {
            Phrases.Add(100, "Trying");
            Phrases.Add(180, "Ringing");
            Phrases.Add(181, "Call Is Being Forwarded");
            Phrases.Add(182, "Queued");
            Phrases.Add(183, "Session Progress");
            Phrases.Add(200, "OK");
            Phrases.Add(300, "Multiple Choices");
            Phrases.Add(301, "Moved Permanently");
            Phrases.Add(302, "Moved Temporarily");
            Phrases.Add(305, "Use Proxy");
            Phrases.Add(380, "Alternative Service");
            Phrases.Add(400, "Bad Request");
            Phrases.Add(401, "Unauthorized");
            Phrases.Add(402, "Payment Required");
            Phrases.Add(403, "Forbidden");
            Phrases.Add(404, "Not Found");
            Phrases.Add(405, "Method Not Allowed");
            Phrases.Add(406, "Not Acceptable");
            Phrases.Add(407, "Proxy Authentication Required");
            Phrases.Add(408, "Request Timeout");
            Phrases.Add(410, "Gone");
            Phrases.Add(413, "Request Entity Too Large");
            Phrases.Add(414, "Request-URI Too Long");
            Phrases.Add(415, "Unsupported Media Type");
            Phrases.Add(416, "Unsupported URI Scheme");
            Phrases.Add(420, "Bad Extension");
            Phrases.Add(421, "Extension Required");
            Phrases.Add(423, "Interval Too Brief");
            Phrases.Add(480, "Temporarily Unavailable");
            Phrases.Add(481, "Call/Transaction Does Not Exist");
            Phrases.Add(482, "Loop Detected");
            Phrases.Add(483, "Too Many Hops");
            Phrases.Add(484, "Address Incomplete");
            Phrases.Add(485, "Ambiguous");
            Phrases.Add(486, "Busy Here");
            Phrases.Add(487, "Request Terminated");
            Phrases.Add(488, "Not Acceptable Here");
            Phrases.Add(491, "Request Pending");
            Phrases.Add(493, "Undecipherable");
            Phrases.Add(500, "Server Internal Error");
            Phrases.Add(501, "Not Implemented");
            Phrases.Add(502, "Bad Gateway");
            Phrases.Add(503, "Service Unavailable");
            Phrases.Add(504, "Server Time-out");
            Phrases.Add(505, "Version Not Supported");
            Phrases.Add(513, "Message Too Large");
            Phrases.Add(600, "Busy Everywhere");
            Phrases.Add(603, "Decline");
            Phrases.Add(604, "Does Not Exist Anywhere");
            Phrases.Add(606, "Not Acceptable");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Lookups the phrase.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static string LookupPhrase(Int16 code)
        {
            return Phrases[code] ?? string.Empty;
        }

        /// <summary>
        /// Registers the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="phrase">The phrase.</param>
        public static void Register(Int16 code, string phrase)
        {
            if(code < 100 || code > 699)
                {
                throw new SipException("Code must be between 100 and 699");
                }

            if(Phrases.ContainsKey(code))
                {
                throw new SipException("Code already exists");
                }

            if(phrase.Length > 255)
                {
                throw new SipException("Phrase too long. Must be less than 255 characters.");
                }

            Phrases.Add(code, phrase);
        }

        #endregion Methods
    }
}