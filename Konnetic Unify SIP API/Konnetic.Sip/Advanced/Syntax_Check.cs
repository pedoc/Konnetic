/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip
    {
    internal static partial class Syntax
        {
        #region Fields

        /// <summary>
        /// 
        /// </summary> 
        private static readonly string AA_ALPHANUM = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// 
        /// </summary> 
        private static readonly string AA_ESCAPED = "%";

        /// <summary>
        /// 
        /// </summary> 
        private static readonly string AA_MARK = "-_.!~*'()"; 
        private static readonly string AA_NUMBERS = "0123456789";

        /// <summary>
        /// 
        /// </summary> 
        private static readonly string AA_RESERVED = ";/?:@&=+$,";

        /// <summary>
        /// 
        /// </summary> 
        private static readonly string AA_TOKENMARKS = "-.!%*_+`'~";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string ALLPARAMETERUNRESERVED = AA_ESCAPED + AA_MARK + AA_TOKENMARKS + "[]/:&+$";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string HEADERUNRESERVED = AA_ESCAPED + AA_MARK + "[]/?:+$";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string HOSTUNRESERVED = AA_ESCAPED + ":-.[]";
        private static readonly string IPADDRESS = AA_ESCAPED + ".:" + AA_NUMBERS + "[]";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string PARAMETERUNRESERVED = AA_ESCAPED + AA_MARK + "[]/:&+$";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string PASSWORDUNRESERVED = AA_ESCAPED + AA_MARK + "&=+$,";
        //private static readonly string TOKEN = AA_ALPHANUM + AA_TOKENMARKS;

        /// <summary>
        /// 
        /// </summary>
        private static readonly string USERUNRESERVED = AA_ESCAPED + AA_MARK + "&=+$,;?/";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Checks the is reserved.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public static bool IsReservedUriComponent(string text, SipUriComponents component)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                { 
                if(IsReservedUriComponent(text[i], component))
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Checks the is reserved.
        /// </summary>
        /// <param name="ch">The ch.</param>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        public static bool IsReservedUriComponent(char ch, SipUriComponents component)
            {
            switch(component)
                {
                case SipUriComponents.SerializationInfoString:
                case SipUriComponents.SipRequestUrl:
                case SipUriComponents.AbsoluteUri:
                    if(!Syntax.IsReserved(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.UserInfo:
                    if((ch == ':') || (ch == '@'))
                        {
                        return true;
                        }
                    if(IsReservedUriComponent(ch, SipUriComponents.UserName) && IsReservedUriComponent(ch, SipUriComponents.Password))
                        {
                        break;
                        }
                    return true;
                case SipUriComponents.Scheme:
                    if((ch != 's') && (ch != 'i') && (ch != 'p') && (ch != ':'))
                        {
                        break;
                        }
                    return true;
                case SipUriComponents.UserName:
                    if(Syntax.IsUnReservedUserName(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.Password:
                    if(Syntax.IsUnReservedPassword(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.MulticastAddress:
                case SipUriComponents.Host:
                    if(Syntax.IsUnReservedHost(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.TimeToLive:
                case SipUriComponents.Port:
                    if(Syntax.IsNumeric(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.LooseRouter:
                    if(!((ch >= 'l') && (ch <= 'r') && (ch >= 'L') && (ch <= 'R')))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.Transport:
                case SipUriComponents.UserParameter:
                case SipUriComponents.Method:
                    if(Syntax.IsToken(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.Parameters:
                    if(Syntax.IsUnReservedParameter(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.Headers:
                    if(Syntax.IsUnReservedHeader(ch))
                        {
                        break;
                        }
                    return true;

                case SipUriComponents.None:
                    if(!Syntax.IsReserved(ch))
                        {
                        break;
                        }
                    return true;
                }
            return false;
            }




        public static bool IsQuotedString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }
            text = text.Trim();
            //34 = Quote (")
            if((Int32)text[0] == 34)
                {
                if((Int32)text[text.Length - 1] == 34)
                    {
                    return true;
                    }
                }
            return false;
            }


        /// <summary>
        /// Checks the is reserved.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="component">The component.</param>
        /// <returns></returns>
        internal static bool IsReservedUriComponent(string text, string component)
            {
            SipUriComponents componentEnum = (SipUriComponents)Enum.Parse(typeof(SipUriComponents), component, true);
            return IsReservedUriComponent(text, componentEnum);
            }

        /// <summary>
        /// Determines whether the specified text is alpha.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is alpha; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsAlpha(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsAlpha(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsAlphaNumeric(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsAlphaNumeric(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsAlphaNumericWithQuotes(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                //34 is QUOTE
                if((Int32)c != 34)
                    {
                    if(!IsAlphaNumeric(c))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }

        internal static bool IsAlphaWithDash(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                //Dash is 45 (-)
                if((Int32)c != 45)
                    {
                    if(!IsAlpha(c))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }

        internal static bool IsLHex(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }
            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsLHex(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsLHexWithQuotes(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }
            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                //34 is QUOTE
                if((Int32)c != 34)
                    {
                    if(!IsLHex(c))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether the specified text is numeric.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is numeric; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsNumeric(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsNumeric(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsQuotingCharacter(char c)
            {
            return (Int32)c == 92;
            }

        internal static bool IsReservedInComment(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");
            if(string.IsNullOrEmpty(text))
                {
                return true;
                }
            char previousChar = ' ';
            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                if(!IsReservedCommentChar(c))
                    {
                    if(!IsQuotingCharacter(previousChar))
                        {
                        return true;
                        }
                    }
                previousChar = c;
                }
            return false;
            }

        /// <summary>
        /// Determines whether the specified text is token.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is token; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsToken(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsToken(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsTokenWithCommaAndQuotes(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                Int32 ci = (Int32)c;
                //Comma is 44,
                if(ci != 44)
                    {
                    //34 is QUOTE
                    if(ci != 34)
                        {
                        //34 is SINGLE SPACE
                        if(ci != 32)
                            {
                            if(!IsToken(c))
                                {
                                return false;
                                }
                            }
                        }
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved all parameter] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved all parameter] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedAllParameter(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedAllParameter(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved header] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved header] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedHeader(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedHeader(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved host] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved host] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedHost(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedHost(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsUnReservedIPAddress(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedIPAddress(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsUnReservedLanguageRange(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                //*, / and - chars are OK
                Int32 ci = (Int32)c;
                if(ci != 45 & ci != 47 & ci != 42)
                    {
                    if(!IsAlpha(c))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved parameter] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved parameter] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedParameter(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedParameter(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved password] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved password] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedPassword(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedPassword(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether [is un reserved user name] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved user name] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsUnReservedUserName(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return true;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsUnReservedUserName(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsWord(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }
            for(int i = 0; i < text.Length; ++i)
                {
                if(!IsWord(text[i]))
                    {
                    return false;
                    }
                }
            return true;
            }

        internal static bool IsWordWithAtSign(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return false;
                }

            for(int i = 0; i < text.Length; ++i)
                {
                char c = text[i];
                if((Int32)c != 64)
                    {
                    if(!IsWord(c))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }


        /// <summary>
        /// Determines whether the specified c is alpha.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if the specified c is alpha; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsAlpha(char c)
            {
            Int32 ci = (Int32)c;
            return (ci >= 65 && ci <= 90) || (ci >= 97 && ci <= 122);
            }

        private static bool IsAlphaNumeric(char c)
            {
            Int32 ci = (Int32)c;
            return (ci >= 48 && ci <= 57) || (ci >= 65 && ci <= 90) || (ci >= 97 && ci <= 122);
            }

        private static bool IsLHex(char c)
            {
            //% + DIGIT + a-f (lowercase)
            Int32 ci = (Int32)c;
            return ci == 37 || (ci >= 48 && ci <= 57) || (ci >= 97 && ci <= 102);
            }

        /// <summary>
        /// Determines whether the specified c is numeric.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if the specified c is numeric; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsNumeric(char c)
            {
            Int32 ci = (Int32)c;
            return (ci >= 48 && ci <= 57);
            }

        /// <summary>
        /// Determines whether the specified c is reserved.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if the specified c is reserved; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsReserved(char c)
            { 
            byte l = checked((Byte)AA_RESERVED.Length); 
            for(byte i = 0; i < l; ++i)
                {
                if(AA_RESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        private static bool IsReservedCommentChar(char c)
            {
            Int32 ci = (Int32)c;
            if(ci == 40 || ci == 41 || ci == 92)
                {  //( or //) or //BACKSLASH
                return true;
                }


            return false;
            }

        private static bool IsReservedQuotedChar(char c)
            {
            Int32 ci = (Int32)c;
            if(ci == 34 || ci == 92)
                {  //QUOTE or  //BACKSLASH
                return true;
                }

            return false;
            }

        /// <summary>
        /// Determines whether the specified c is token.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if the specified c is token; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsToken(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)AA_TOKENMARKS.Length); 
            for(byte i = 0; i < l; ++i)
                {
                if(AA_TOKENMARKS[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved all parameter] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved all parameter] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedAllParameter(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)ALLPARAMETERUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(ALLPARAMETERUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved header] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved header] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedHeader(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)HEADERUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(HEADERUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved host] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved host] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedHost(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)HOSTUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(HOSTUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        private static bool IsUnReservedIPAddress(char c)
            {
            byte l = checked((Byte)IPADDRESS.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(IPADDRESS[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved parameter] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved parameter] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedParameter(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)PARAMETERUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(PARAMETERUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved password] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved password] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedPassword(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)PASSWORDUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(PASSWORDUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        /// <summary>
        /// Determines whether [is un reserved user name] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is un reserved user name] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnReservedUserName(char c)
            {
            if(IsAlphaNumeric(c))
                {  //ALPHANUM
                return true;
                }

            byte l = checked((Byte)USERUNRESERVED.Length);
            for(byte i = 0; i < l; ++i)
                {
                if(USERUNRESERVED[i] == c)
                    {
                    return true;
                    }
                }
            return false;
            }

        private static bool IsWord(char c)
            {
            if(IsAlphaNumeric(c))
                {
                return true;
                }
            Int32 ci = (Int32)c;
            if(ci <= 20 || ci >= 127 || ci == 124 || ci == 94 || ci == 64 || ci == 61 || ci == 59 || ci == 44 || ci == 38 || ci == 36 || ci == 35)
                {
                return false;
                }
            return true;
            }

        #endregion Methods
        }
    }