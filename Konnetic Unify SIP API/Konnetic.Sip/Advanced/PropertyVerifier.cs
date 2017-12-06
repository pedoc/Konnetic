/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip
{
    internal static class PropertyVerifier
    {
 

        #region Methods




        public static void ThrowIfByteOutOfRange(byte number, byte minValue, byte maxValue, string paramName)
        {
            if((number < minValue) || (number > maxValue))
                {
				throw new SipOutOfRangeException(paramName, SR.ByteOutOfRange(number, minValue, maxValue));
                }
        }

        public static void ThrowIfDoubleOutOfRange(double number, double minValue, double maxValue, string paramName)
        {
            if((number < minValue) || (number > maxValue))
                {
				throw new SipOutOfRangeException(paramName, SR.DoubleOutOfRange(number, minValue, maxValue));
                }
        }

        public static void ThrowIfDuplicateHeaderField(HeaderFieldCollection hfc, string fieldName)
        {
            if(hfc.Contains(fieldName))
                {
                throw new ArgumentException(fieldName, SR.DuplicateHeaderField(fieldName));
                }
        }

        public static void ThrowIfFloatOutOfRange(float? number, float minValue, float maxValue, string paramName)
            {
            if((number < minValue) || (number > maxValue))
                {
                throw new SipOutOfRangeException(paramName, SR.FloatOutOfRange(number, minValue, maxValue));
                }
            }
        public static void ThrowIfFloatOutOfRange(float number, float minValue, float maxValue, string paramName)
        {
            if((number < minValue) || (number > maxValue))
                {
				throw new SipOutOfRangeException(paramName, SR.FloatOutOfRange(number, minValue, maxValue));
                }
        }

        public static void ThrowIfIntOutOfRange(long? number, long minValue, long maxValue, string paramName)
            {
            if((number < minValue) || (number > maxValue))
                {
                throw new SipOutOfRangeException(paramName, SR.LongOutOfRange(number, minValue, maxValue));
                }
            }

        public static void ThrowIfIntOutOfRange(long number, long minValue, long maxValue, string paramName)
        {
            if((number < minValue) || (number > maxValue))
                {
				throw new SipOutOfRangeException(paramName, SR.LongOutOfRange(number, minValue, maxValue));
                }
        }

        public static void ThrowIfStringSizeOutOfRange(string s, long minValue, long maxValue, string paramName)
        {
            if((s.Length < minValue) || (s.Length > maxValue))
                {
                throw new ArgumentException(paramName, SR.StringOutOfRange(paramName, minValue, maxValue));
                }
        }

        public static void ThrowOnNullArgument(object obj, string paramName)
        {
            if(obj == null)
                {
                throw new ArgumentNullException(paramName);
                }
        }

        public static void ThrowOnNullOrEmptyString(string str, string paramName)
        {
        if((object)str == null)
                {
                throw new ArgumentNullException(paramName, SR.EmptyString);
                }
            if(string.IsNullOrEmpty(str))
                {
                throw new ArgumentException(SR.EmptyString, paramName);
                }
        }

        public static void ThrowOnInvalidToken(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsToken(value))
                {
                throw new SipFormatException(SR.ValueNotATokenString(value));
                }
            }

        public static void ThrowUriExceptionOnReservedUsername(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedUserName(value))
                {
                throw new SipUriFormatException(SR.ValueAReservedUsername(value));
                }
            }
        public static void ThrowUriExceptionOnReservedPassword(string value, string valueName)
            { 
                if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedPassword(value))
                {
                throw new SipUriFormatException(SR.ValueAReservedPassword(value));
                }
            }
        public static void ThrowUriExceptionOnInvalidToken(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsToken(value))
                {
                throw new SipUriFormatException(SR.ValueNotATokenString(value));
                }
            }
        public static void ThrowOnInvalidQuotedTokenWithComma(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsTokenWithCommaAndQuotes(value))
                {
                throw new SipFormatException(SR.ValueNotAQuotedTokenWithCommaString(value));
                }
            }

        public static void ThrowOnInvalidHostString(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedHost(value))
                {
                throw new SipFormatException(SR.ValueNotAHostString(value));
                }
            }

        public static void ThrowUriExceptionOnInvalidHostString(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedHost(value))
                {
                throw new SipUriFormatException(SR.ValueNotAHostString(value));
                }
            }
        public static void ThrowOnInvalidIPAddressString(string value, string valueName)
            {
            if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedIPAddress(value))
                {
                throw new SipFormatException(SR.ValueNotAnIPAddressString(value));
                }
            } 
        #endregion Methods
    }
}