/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace Konnetic.Sip
	{
	internal static class UrlSyntax
		{
		public static string UrlDecode(string str)
			{
			return UrlDecode(str, Encoding.UTF8);
			}

		private static char[] GetChars(MemoryStream b, Encoding e)
			{
			return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
			}

		public static string UrlDecode(string s, Encoding e)
			{
			if(null == s)
				return null;

			if(s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
				return s;

			if(e == null)
				e = Encoding.UTF8;

            int len = s.Length;
            StringBuilder output = new StringBuilder(len);
			MemoryStream bytes = new MemoryStream();
			int xchar;

			for(int i = 0; i < len; i++)
				{
				if(s[i] == '%' && i + 2 < len && s[i + 1] != '%')
					{
					if(s[i + 1] == 'u' && i + 5 < len)
						{
						if(bytes.Length > 0)
							{
							output.Append(GetChars(bytes, e));
							bytes.SetLength(0);
							}

						xchar = GetChar(s, i + 2, 4);
						if(xchar != -1)
							{
							output.Append((char)xchar);
							i += 5;
							}
						else
							{
							output.Append('%');
							}
						}
					else if((xchar = GetChar(s, i + 1, 2)) != -1)
						{
						bytes.WriteByte((byte)xchar);
						i += 2;
						}
					else
						{
						output.Append('%');
						}
					continue;
					}

				if(bytes.Length > 0)
					{
					output.Append(GetChars(bytes, e));
					bytes.SetLength(0);
					}

				if(s[i] == '+')
					{
					output.Append(' ');
					}
				else
					{
					output.Append(s[i]);
					}
				}

			if(bytes.Length > 0)
				{
				output.Append(GetChars(bytes, e));
				}

			bytes = null;
			return output.ToString();
			}

		public static string UrlDecode(byte[] bytes, Encoding e)
			{
			if(bytes == null)
				return null;

			return UrlDecode(bytes, 0, bytes.Length, e);
			}

		private static int GetInt(byte b)
			{
			char c = (char)b;
			if(c >= '0' && c <= '9')
				return c - '0';

			if(c >= 'a' && c <= 'f')
				return c - 'a' + 10;

			if(c >= 'A' && c <= 'F')
				return c - 'A' + 10;

			return -1;
			}

		private static int GetChar(byte[] bytes, int offset, int length)
			{
			int value = 0;
			int end = (length + offset);
			for(int i = offset; i < end; i++)
				{
				int current = GetInt(bytes[i]);
				if(current == -1)
					return -1;
				value = (value << 4) + current;
				}

			return value;
			}

		private static int GetChar(string str, int offset, int length)
			{
            int val = 0;
            int end = (length + offset);
			for(int i = offset; i < end; i++)
				{
				char c = str[i];
				if(c > 127)
					return -1;

				int current = GetInt((byte)c);
				if(current == -1)
					return -1;
				val = (val << 4) + current;
				}

			return val;
			}

		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
			{
			if(bytes == null)
				return null;
			if(count == 0)
				return String.Empty;

			if(bytes == null)
				throw new ArgumentNullException("bytes");

			if(offset < 0 || offset > bytes.Length)
				throw new ArgumentOutOfRangeException("offset");

			if(count < 0 || offset + count > bytes.Length)
				throw new ArgumentOutOfRangeException("count");

			StringBuilder output = new StringBuilder();
			MemoryStream acc = new MemoryStream();

            int end = (count + offset); 
			int xchar;
			for(int i = offset; i < end; i++)
				{
				if(bytes[i] == '%' && i + 2 < count && bytes[i + 1] != '%')
					{
					if(bytes[i + 1] == (byte)'u' && i + 5 < end)
						{
						if(acc.Length > 0)
							{
							output.Append(GetChars(acc, e));
							acc.SetLength(0);
							}
						xchar = GetChar(bytes, i + 2, 4);
						if(xchar != -1)
							{
							output.Append((char)xchar);
							i += 5;
							continue;
							}
						}
					else if((xchar = GetChar(bytes, i + 1, 2)) != -1)
						{
						acc.WriteByte((byte)xchar);
						i += 2;
						continue;
						}
					}

				if(acc.Length > 0)
					{
					output.Append(GetChars(acc, e));
					acc.SetLength(0);
					}

				if(bytes[i] == '+')
					{
					output.Append(' ');
					}
				else
					{
					output.Append((char)bytes[i]);
					}
				}

			if(acc.Length > 0)
				{
				output.Append(GetChars(acc, e));
				}

			acc = null;
			return output.ToString();
			}

		public static byte[] UrlDecodeToBytes(byte[] bytes)
			{
			if(bytes == null)
				return null;

			return UrlDecodeToBytes(bytes, 0, bytes.Length);
			}

		public static byte[] UrlDecodeToBytes(string str)
			{
			return UrlDecodeToBytes(str, Encoding.UTF8);
			}

		public static byte[] UrlDecodeToBytes(string str, Encoding e)
			{
			if(str == null)
				return null;

			if(e == null)
				throw new ArgumentNullException("e");

			return UrlDecodeToBytes(e.GetBytes(str));
			}

		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
			{
			if(bytes == null)
				return null;
			if(count == 0)
				return new byte[0];

			int len = bytes.Length;
			if(offset < 0 || offset >= len)
				throw new ArgumentOutOfRangeException("offset");

			if(count < 0 || offset > len - count)
				throw new ArgumentOutOfRangeException("count");

			MemoryStream result = new MemoryStream();
			int end = offset + count;
			for(int i = offset; i < end; i++)
				{
				char c = (char)bytes[i];
				if(c == '+')
					{
					c = ' ';
					}
				else if(c == '%' && i < end - 2)
					{
					int xchar = GetChar(bytes, i + 1, 2);
					if(xchar != -1)
						{
						c = (char)xchar;
						i += 2;
						}
					}
				result.WriteByte((byte)c);
				}

			return result.ToArray();
			}

		static char[] hexChars = "0123456789abcdef".ToCharArray();
		const string notEncoded = "!'()*-._";

		static void UrlEncodeChar(char c, Stream result, bool isUnicode)
			{
			if(c > 255)
				{
				//FIXME: what happens when there is an internal error?
				//if (!isUnicode)
				//	throw new ArgumentOutOfRangeException ("c", c, "c must be less than 256");
				int idx;
				int i = (int)c;

				result.WriteByte((byte)'%');
				result.WriteByte((byte)'u');
				idx = i >> 12;
				result.WriteByte((byte)hexChars[idx]);
				idx = (i >> 8) & 0x0F;
				result.WriteByte((byte)hexChars[idx]);
				idx = (i >> 4) & 0x0F;
				result.WriteByte((byte)hexChars[idx]);
				idx = i & 0x0F;
				result.WriteByte((byte)hexChars[idx]);
				return;
				}

			if(c > ' ' && notEncoded.IndexOf(c) != -1)
				{
				result.WriteByte((byte)c);
				return;
				}
			if(c == ' ')
				{
				result.WriteByte((byte)'+');
				return;
				}
			if((c < '0') ||
				(c < 'A' && c > '9') ||
				(c > 'Z' && c < 'a') ||
				(c > 'z'))
				{
				if(isUnicode && c > 127)
					{
					result.WriteByte((byte)'%');
					result.WriteByte((byte)'u');
					result.WriteByte((byte)'0');
					result.WriteByte((byte)'0');
					}
				else
					result.WriteByte((byte)'%');

				int idx = ((int)c) >> 4;
				result.WriteByte((byte)hexChars[idx]);
				idx = ((int)c) & 0x0F;
				result.WriteByte((byte)hexChars[idx]);
				}
			else
				result.WriteByte((byte)c);
			}

		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
			{
			if(bytes == null)
				return null;

			int len = bytes.Length;
			if(len == 0)
				return new byte[0];

			if(offset < 0 || offset >= len)
				throw new ArgumentOutOfRangeException("offset");

			if(count < 0 || count > len - offset)
				throw new ArgumentOutOfRangeException("count");

            MemoryStream result = new MemoryStream(count);
            int end = (count + offset);  
			for(int i = offset; i < end; i++)
				UrlEncodeChar((char)bytes[i], result, false);

			return result.ToArray();
			}


		public static byte[] UrlEncodeUnicodeToBytes(string str)
			{
			if(str == null)
				return null;

			if(str == "")
				return new byte[0];

			MemoryStream result = new MemoryStream(str.Length);
			foreach(char c in str)
				{
				UrlEncodeChar(c, result, true);
				}
			return result.ToArray();
			}

 
		public static NameValueCollection ParseQueryString (string query)
		{
			return ParseQueryString (query, Encoding.UTF8);
		}

		public static NameValueCollection ParseQueryString (string query, Encoding encoding)
		{
			if (query == null)
				throw new ArgumentNullException ("query");
			if (encoding == null)
				throw new ArgumentNullException ("encoding");
			if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
				return new NameValueCollection ();
			if (query[0] == '?')
				query = query.Substring (1);
				
			NameValueCollection result = new NameValueCollection ();
			ParseQueryString (query, encoding, result);
			return result;
		} 				
 
		internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
			{
			if(query.Length == 0)
				return;

			int namePos = 0;
			while(namePos <= query.Length)
				{
				int valuePos = -1, valueEnd = -1;
				for(int q = namePos; q < query.Length; q++)
					{
					if(valuePos == -1 && query[q] == '=')
						{
						valuePos = q + 1;
						}
					else if(query[q] == '&')
						{
						valueEnd = q;
						break;
						}
					}

				string name, value;
				if(valuePos == -1)
					{
					name = null;
					valuePos = namePos;
					}
				else
					{
					name = UrlDecode(query.Substring(namePos, valuePos - namePos - 1), encoding);
					}
				if(valueEnd < 0)
					{
					namePos = -1;
					valueEnd = query.Length;
					}
				else
					{
					namePos = valueEnd + 1;
					}
				value = UrlDecode(query.Substring(valuePos, valueEnd - valuePos), encoding);

				result.Add(name, value);
				if(namePos == -1)
					break;
				}
			}
		}
	}
