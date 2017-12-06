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
        public static string ConvertToComment(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }

            text = Syntax.ReplaceFolding(text);

            int begin = 0;
            int end = text.Length;

            StringBuilder sb = new StringBuilder(end + 2);
            sb.Append('(');
            while(begin < end)
                {
                char c = text[begin];
                Int32 ci = (Int32)c;
                if(IsReservedCommentChar(c))
                    {
                    sb.Append(QuotedPairChar(c));
                    }
                else if(ci < 32 || ci == 127)
                    {
                    //CR and LF
                    if(ci == 13) //CR
                        {
                        if((Int32)text[begin + 1] == 10) //LF
                            {
                            // Replace CRLF with NOTHING
                            begin++;
                            }
                        else
                            {
                            //Not allowed Drop it.
                            // sb.Append(c);
                            }
                        }
                    else if(ci == 10) //LF
                        {
                        //Not allowed Drop it.
                        // sb.Append(c);
                        }
                    else
                        {
                        sb.Append(QuotedPairChar(c)); //QuotePair char
                        }
                    }
                else
                    {
                    sb.Append(c);
                    }
                begin++;
                }
            sb.Append(')');
            return sb.ToString();
            }

        public static string ConvertToQuotedString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return "\"\"";
                }
            text = Syntax.ReplaceFolding(text);
            int begin = 0;
            int end = text.Length;

            StringBuilder sb = new StringBuilder(end + 2);
            sb.Append('"');

            while(begin < end)
                {
                char c = text[begin];
                Int32 ci = (Int32)c;
                if(IsReservedQuotedChar(c))
                    {
                    sb.Append(QuotedPairChar(c));
                    }
                else if(ci < 32 || ci == 127)
                    {
                    //CR and LF
                    if(ci == 13) //CR
                        {
                        if((Int32)text[begin + 1] == 10)//LF
                            {
                            // Replace CRLF with NOTHING
                            begin++;
                            }
                        else
                            {
                            //Not allowed Drop it.
                            // sb.Append(c);
                            }
                        }
                    else if(ci == 10)
                        {
                        //Not allowed Drop it.
                        // sb.Append(c);
                        }
                    else
                        {
                        sb.Append(QuotedPairChar(c)); //QuotePair char
                        }
                    }
                else
                    {
                    sb.Append(c);
                    }
                begin++;
                }
            sb.Append('"');
            return sb.ToString();
            }

        public static string ConvertToTextUtf8Trim(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }

            //Trim text
            text = Syntax.ReplaceFolding(text);
            text = text.Trim(' ', '\t');
            int begin = 0;
            int end = text.Length;

            StringBuilder sb = new StringBuilder(end + 2);

            while(begin < end)
                {
                char c = text[begin];
                Int32 ci = (Int32)c;
                if(!(ci < 32 || ci == 127)) //Ignore NON Printable chars
                    {
                    sb.Append(c);
                    }
                begin++;
                }
            return sb.ToString();
            }

        public static string QuoteString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }
            return "\"" + text + "\"";
            }

        public static string UnCommentString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");
            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }

            int begin = 0;
            int length = text.Length;
            int end = 0;
            //(
            if((Int32)text[0] == 40)
                {
                begin++;
                length--;
                }
            //)
            if((Int32)text[text.Length - 1] == 41)
                {
                --length;
                }

            end = begin + length;
            StringBuilder sb = new StringBuilder(length + 2);
            bool ignoreChar = false;
            while(begin < end)
                {
                char c = text[begin];
                if((Int32)c == 92) //Backslash escape char
                    {
                    if(ignoreChar == false)
                        {
                        ignoreChar = true;
                        }
                    else
                        {
                        ignoreChar = false;
                        }
                    }
                else
                    {
                    ignoreChar = false;
                    }

                if(!ignoreChar)
                    {
                    sb.Append(c);
                    }

                begin++;
                }
            return sb.ToString();
            }

        public static string UnQuoteString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }
            int begin = 0;
            int length = text.Length;

            //34 = Quote (")
            if((Int32)text[0] == 34)
                {
                ++begin;
                length = length - begin;
                }
            if((Int32)text[text.Length - 1] == 34)
                {
                --length;
                }
            return text.Substring(begin, length);
            }

        public static string UnQuotedString(string text)
            {
            return Syntax.UnescapeString(Syntax.UnQuoteString(text));
            }

        public static string UnescapeString(string text)
            {
            PropertyVerifier.ThrowOnNullArgument(text, "text");

            if(string.IsNullOrEmpty(text))
                {
                return string.Empty;
                }

            int begin = 0;
            int end = text.Length;

            StringBuilder sb = new StringBuilder(end + 2);
            bool ignoreChar = false;
            while(begin < end)
                {
                char c = text[begin];
                if((Int32)c == 92) //Backslash escape char
                    {
                    if(ignoreChar == false)
                        {
                        ignoreChar = true;
                        }
                    else
                        {
                        ignoreChar = false;
                        }
                    }
                else
                    {
                    ignoreChar = false;
                    }

                if(!ignoreChar)
                    {
                    sb.Append(c);
                    }
                begin++;
                }
            return sb.ToString();
            }

        private static string QuotedPairChar(char c)
            {
            return "\\" + c;
            //return "\\\\%" + String.Format("{0:X}", Convert.ToInt32(c));
            }

        internal static string ReplaceFolding(string text)
            {
            if((object)text == null)
                {
                return null;
                }
            if(text.Length == 0)
                {
                return string.Empty;
                }
            Regex LWS = new Regex(@"[ \t]*(\r\n[ \t]+)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return LWS.Replace(text, " ");
            }
        }
    }