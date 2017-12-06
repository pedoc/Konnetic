/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Konnetic.Sip
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal sealed class SR
        {
        #region Fields 
        internal const string InvalidUsernameString = "InvalidUsernameString";
        internal const string InvalidPasswordString = "InvalidPasswordString";
        internal const string IllegalParameterNameException = "IllegalParameterNameException";
        internal const string DuplicateHeaderParameter = "DuplicateHeaderParameter";
        internal const string OutOfRangeParameter = "OutOfRangeParameter";
        internal const string OutOfRangeException = "OutOfRangeException";
        internal const string GeneralParseException = "GeneralParseException";
        internal const string OverflowException = "OverflowException";
        internal const string IntegerConvertException = "IntegerConvertException";
        internal const string FloatConvertException = "FloatConvertException";
        internal const string ByteConvertException = "ByteConvertException";
        internal const string DefaultSipScheme = "SIP";
        internal const char DisplayNameEnd = ' ';
        internal const char DisplayNameStart = ' ';
        internal const string EmptyString = "EmptyString";
        internal const string FromHeaderFieldHiddenDisplayName = "FromHeaderFieldHiddenDisplayName";
        internal const string HeaderFieldDuplicate = "HeaderFieldDuplicate";
        internal const char HeaderFieldSeperator = ':';
        internal const string HiddenAddress = "HiddenAddress";
        internal const char MediaSubTypeDefault = '*';
        internal const char MediaTypeDefault = '*';
        internal const char MediaTypeSeperator = '/';
        internal const char MultiheaderFieldSuffix = ',';
        internal const string NullCollectionMember = "NullCollectionMember";
        internal const string OutOfRangeByte = "OutOfRangeByte";
        internal const string OutOfRangeDouble = "OutOfRangeDouble";
        internal const string OutOfRangeFloat = "OutOfRangeFloat";
        internal const string OutOfRangeLong = "OutOfRangeLong";
        internal const string OutOfRangeString = "OutOfRangeString";
        internal const char ParameterPrefix = ';';
        internal const char ParameterSeperator = '=';
        internal const string SIPVersionNumber = "2.0";
        internal const string SecureHiddenAddress = "SecureHiddenAddress";
        internal const char SecurityUriEnd = '"';
        internal const char SecurityUriStart = '"';
        internal const char ServerProductSeperator = '/';
        internal const char SingleWhiteSpace = ' ';
        internal const string SipVersion = "SIP/2.0";
        internal const char SipVersionSeperator = '/';
        internal const char UriEnd = '>';
        internal const char UriStart = '<';
        internal const char VersionSeperator = '.';
        internal const char ViaProtocolSeperator = '/';
		internal const string PropertyParseException = "PropertyParseException";		
		internal const string ParseException = "ParseException";
        internal const string InvalidTokenString = "InvalidTokenString";
        internal const string InvalidQuotedTokenWithCommaString = "InvalidQuotedTokenWithCommaString";
        internal const string InvalidHostString = "InvalidHostString";
        internal const string InvalidIPAddressString = "InvalidIPAddressString";
        
        
        private static SR loader;
        private static object s_InternalSyncObject;

        private ResourceManager resources;

        #endregion Fields

        #region Properties

        private static object InternalSyncObject
        {
            get
                {
                if(s_InternalSyncObject == null)
                    {
                    object obj2 = new object();
                    System.Threading.Interlocked.CompareExchange(ref s_InternalSyncObject, obj2, null);
                    }
                return s_InternalSyncObject;
                }
        }

        #endregion Properties

        #region Constructors

        internal SR()
        {
            this.resources = new ResourceManager("Konnetic.Sip.Strings", base.GetType().Assembly);
        }

        #endregion Constructors

        #region Methods

        public static string GetString(string name)
        {
            SR loader = GetLoader();
            if((object)loader == null)
                {
                return null;
                }
            return loader.resources.GetString(name);
        }

        internal static string ByteOutOfRange(byte v, byte min, byte max)
        {
            return SR.GetString(SR.OutOfRangeByte, v, min, max);
        }

        internal static string DoubleOutOfRange(double v, double min, double max)
        {
            return SR.GetString(SR.OutOfRangeDouble, v, min, max);
        }

        internal static string DuplicateHeaderField(string fieldName)
        {
            return SR.GetString(SR.HeaderFieldDuplicate, fieldName);
        }

        internal static string FloatOutOfRange(float? v, float min, float max)
            {
            return SR.GetString(SR.OutOfRangeFloat, v, min, max);
            }
        internal static string FloatOutOfRange(float v, float min, float max)
        {
            return SR.GetString(SR.OutOfRangeFloat, v, min, max);
        }

        internal static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
            {
            return string.Format(CultureInfo.InvariantCulture, GetString(key), new object[] { arg0, arg1, arg2, arg3 });
            } 
        internal static string GetString(string key, object arg0, object arg1, object arg2)
        {
            return string.Format(CultureInfo.InvariantCulture, GetString(key), new object[] { arg0, arg1, arg2 });
        }

        internal static string GetString(string key, object arg0, object arg1)
        {
            return string.Format(CultureInfo.InvariantCulture,GetString(key), new object[] { arg0, arg1});
        }

        internal static string GetString(string key, object arg0)
        {
            return string.Format(CultureInfo.InvariantCulture, GetString(key), new object[] { arg0 });
        }

        internal static string LongOutOfRange(long? v, long min, long max)
            {
            return SR.GetString(SR.OutOfRangeLong, v, min, max);
            }

        internal static string LongOutOfRange(long v, long min, long max)
        {
            return SR.GetString(SR.OutOfRangeLong, v, min, max);
        }

        internal static string StringOutOfRange(string v, long min, long max)
        {
            return SR.GetString(SR.OutOfRangeString, v, min, max);
        }

		internal static string PropertyParseExceptionMessage(string value)
			{
			return SR.GetString(SR.PropertyParseException, value);
			}
		internal static string ParseExceptionMessage(string value)
			{
			return SR.GetString(SR.ParseException, value);
			}
        private static SR GetLoader()
        {
        if((object)loader == null)
                {
                lock(InternalSyncObject)
                    {
                    if((object)loader == null)
                        {
                        loader = new SR();
                        }
                    }
                }
            return loader;
        }

        internal static string ValueNotATokenString(string v)
            {
            return SR.GetString(SR.InvalidTokenString, v);
            }
        internal static string ValueNotAQuotedTokenWithCommaString(string v)
            {
            return SR.GetString(SR.InvalidQuotedTokenWithCommaString, v);
            }
        internal static string ValueNotAHostString(string v)
            {
            return SR.GetString(SR.InvalidHostString, v);
            }

        internal static string ValueAReservedUsername(string v)
            {
            return SR.GetString(SR.InvalidUsernameString, v);
            }
        internal static string ValueAReservedPassword(string v)
            {
            return SR.GetString(SR.InvalidPasswordString, v);
            }
        internal static string ValueNotAnIPAddressString(string v)
            {
            return SR.GetString(SR.InvalidIPAddressString, v);
            }
        #endregion Methods
    }
}