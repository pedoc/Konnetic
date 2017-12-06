/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    internal static class SipGuid
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private const int PROCESSINGGAP = 1000000000;

        #endregion Fields

        #region Methods

        /// <summary>
        /// New int SIP GUID.
        /// </summary>
        /// <remarks>Returns a cryptographically unique 32bit numeric. Ensures adequate process gap to to max int</remarks>
        /// <returns>Cryptographically unique 32bit numeric</returns>
        public static int NewIntSipGuid()
        {
            byte[] tag = new byte[4];
            RandomNumberGenerator.Create().GetBytes(tag);
            int retVal = Math.Abs(BitConverter.ToInt32(tag, 0));
            if(retVal > PROCESSINGGAP)
                {
                //Remove a 1 billion entries so we are not ever near the max for an int.
                //Specification recommends the first 30 significant bit in a 32bit representation of the current second
                //We are approximating that recommendation.
                unchecked { retVal -= PROCESSINGGAP; }
                }
            return Math.Abs(retVal);
        }

        /// <summary>
        /// New SIP GUID.
        /// </summary>
        /// <param name="prefix">The prefix for the eventual GUID.</param>
        /// <param name="suffix">The suffix for the eventual GUID.</param>
        /// <returns>Cryptographically unique 32bit numeric pre and postfixed by arguments</returns>
        public static string NewSipGuid(string prefix, string suffix)
        {
            byte[] tag = new byte[4];
            StringBuilder sb = new StringBuilder();
            RandomNumberGenerator.Create().GetBytes(tag);
            sb.AppendFormat(CultureInfo.InvariantCulture,"{0}{1:x}{2:x}{3:x}{4:x}{5}", new object[] { prefix, tag[0], tag[1], tag[2], tag[3], suffix });
            return sb.ToString();
        }

        /// <summary>
        /// New SIP GUID.
        /// </summary>
        /// <returns></returns>
        public static string NewSipGuid()
        {
            return NewSipGuid(string.Empty, string.Empty);
        }

        #endregion Methods
    }
}