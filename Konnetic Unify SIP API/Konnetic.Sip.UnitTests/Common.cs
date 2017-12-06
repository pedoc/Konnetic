using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    [TestClass]
    public static class InitTestEnv
    {
        #region Methods

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            if(!SipStyleUriParser.IsKnownScheme("sip"))
                {
                SipStyleUriParser p = new SipStyleUriParser();
                SipStyleUriParser.Register(p, "sip", 5060);
                SipStyleUriParser p1 = new SipStyleUriParser();
                SipStyleUriParser.Register(p1, "sips", 5060);
                }
        }

        #endregion Methods
    }

    internal static class Common
    {
        #region Fields

	private static readonly char c0 = Convert.ToChar(0);
	private static readonly char c1 = Convert.ToChar(1);
	private static readonly char c10 = Convert.ToChar(10);
	private static readonly char c11 = Convert.ToChar(11);
	private static readonly char c12 = Convert.ToChar(12);
	private static readonly char c13 = Convert.ToChar(13);
	private static readonly char c14 = Convert.ToChar(14);
	private static readonly char c15 = Convert.ToChar(15);
	private static readonly char c16 = Convert.ToChar(16);
	private static readonly char c17 = Convert.ToChar(17);
	private static readonly char c18 = Convert.ToChar(18);
	private static readonly char c19 = Convert.ToChar(19);
	private static readonly char c2 = Convert.ToChar(2);
	private static readonly char c20 = Convert.ToChar(20);
	private static readonly char c21 = Convert.ToChar(21);
	private static readonly char c22 = Convert.ToChar(22);
	private static readonly char c23 = Convert.ToChar(23);
	private static readonly char c24 = Convert.ToChar(24);
	private static readonly char c25 = Convert.ToChar(25);
	private static readonly char c26 = Convert.ToChar(26);
	private static readonly char c27 = Convert.ToChar(27);
	private static readonly char c28 = Convert.ToChar(28);
	private static readonly char c29 = Convert.ToChar(29);
	private static readonly char c3 = Convert.ToChar(3);
	private static readonly char c30 = Convert.ToChar(30);
	private static readonly char c31 = Convert.ToChar(31);
	private static readonly char c4 = Convert.ToChar(4);
	private static readonly char c5 = Convert.ToChar(5);
	private static readonly char c6 = Convert.ToChar(6);
	private static readonly char c7 = Convert.ToChar(7);
	private static readonly char c8 = Convert.ToChar(8);
	private static readonly char c9 = Convert.ToChar(9);
        internal static readonly char[] NONPRINTABLE = new char[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31 };
        internal static readonly char[] NONPRINTABLEESCAPED = new char[] { '\\', c1, '\\', c2, '\\', c3, '\\', c4, '\\', c5, '\\', c6, '\\', c7, '\\', c8, '\\', c9, '\\', c11, '\\', c12, '\\', c14, '\\', c15, '\\', c16, '\\', c17, '\\', c18, '\\', c19, '\\', c20, '\\', c21, '\\', c22, '\\', c23, '\\', c24, '\\', c25, '\\', c26, '\\', c27, '\\', c28, '\\', c29, '\\', c30, '\\', c31 };
        internal static readonly char[] NONPRINTABLELESSCRLF = new char[] { c1, c2, c3, c4, c5, c6, c7, c8, c9,  c11, c12,  c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31 };
        internal static readonly string ALPHA = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        internal static readonly string ALPHANUM = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        internal static readonly string HOSTRESERVED = ";/?@&=+$,";
        internal static readonly string MARK = "-_.!~*'()";
        internal static readonly string NUMBER = "0123456789";
        internal static readonly string PASSWORDRESERVED = ";/?:@";
        internal static readonly string QUOTEDSTRING;
        internal static readonly string QUOTEDSTRINGESCAPED;
        internal static readonly string QUOTEDSTRINGESCAPEDRESULT;
        internal static readonly string QUOTEDSTRINGRESULT;
        internal static readonly string RESERVED = ";/?:@&=+$,";
        internal static readonly string TEXTUTF8TRIM = "  ü \tü \"<(\"";
        internal static readonly string TEXTUTF8TRIMRESULT = "ü ü \"<(\"";
        internal static readonly string TOKENRESERVED = ";/?:@&=$,";
        internal static readonly string USERRESERVED = ":@";
		internal static readonly string USERUNRESERVED = "&=+$,;?/";
		internal static readonly string HOSTUNRESERVED = ALPHANUM + ":[].-";
		internal static readonly string COMMENTSTRING = ALPHANUM + "ú\"\\\r\n %45()";
		internal static readonly string HEX = NUMBER + "abcdefABCDEF";
		internal static readonly string HEXRESERVED = MARK + "GHIJKLMNOPQRSTUVWXYZghijklmnopqrstuvwxyz";
		internal static readonly string TOKEN = ALPHANUM + "-.!%*_+`'~";
        internal static readonly string WORD = TOKEN + "()<>:\\\"/[]?{}";


        #endregion Fields

        #region Constructors

        static Common()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(NONPRINTABLE);
            sb.Append(ALPHANUM);
            sb.Append("ú\"\\\r\n \t\r\n\t%45");
            QUOTEDSTRING = sb.ToString();

            sb = new StringBuilder();
            sb.Append(NONPRINTABLELESSCRLF);
            sb.Append(ALPHANUM);
            sb.Append("ú\"\\ %45");
            QUOTEDSTRINGRESULT = sb.ToString();

            sb = new StringBuilder();
            sb.Append(NONPRINTABLEESCAPED);
            sb.Append(ALPHANUM);
            sb.Append("ú\\\"\\\\ %45");
            QUOTEDSTRINGESCAPED = sb.ToString();

            QUOTEDSTRINGESCAPEDRESULT = "\"" + QUOTEDSTRINGESCAPED + "\"";
        }

        #endregion Constructors
    }
}