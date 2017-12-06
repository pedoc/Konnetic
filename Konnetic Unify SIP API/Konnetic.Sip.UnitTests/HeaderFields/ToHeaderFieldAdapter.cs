using Konnetic.Sip;
using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ToHeaderFieldAdapter and is intended
    ///to contain all ToHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ToHeaderFieldAdapter
    {
        #region Fields

        private TestContext testContextInstance;

        #endregion Fields

        #region Properties

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
                {
                return testContextInstance;
                }
            set
                {
                testContextInstance = value;
                }
        }

        #endregion Properties

        #region Methods

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            if(!SipStyleUriParser.IsKnownScheme("sip"))
                {
                SipStyleUriParser p = new SipStyleUriParser();
                SipStyleUriParser.Register(p, "sip", 5060);
                SipStyleUriParser p1 = new SipStyleUriParser();
                SipStyleUriParser.Register(p1, "sips", 5060);
                }
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            ToHeaderField target = new ToHeaderField();
            HeaderFieldBase expected = new ToHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.DisplayName="abc";
            actual = target.Clone();
            Assert.IsTrue(((ToHeaderField)actual).DisplayName == "abc");
            ((ToHeaderField)expected).DisplayName = "abc";
            Assert.AreEqual(expected, actual);

            target.Tag = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ToHeaderField)expected).Tag = "abc";
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:abc");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ToHeaderField)expected).Uri = new SipUri("sip:abc");
            Assert.AreEqual(expected, actual);

            target.AddParameter("aa", "bb");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ToHeaderField)expected).AddParameter("aa", "bb");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            HeaderFieldBase target = new ToHeaderField();
            HeaderFieldBase headerField = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "headerField is null");

            target.Parse("tony <sip:123@bob.com>");
            headerField = new ToHeaderField();
            expected = false;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Different Values");

            headerField.Parse("sip:123@bob.com");
            expected = true;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Same at HeaderField level");
            ((ToHeaderField)target).RecreateTag();
            ((ToHeaderField)headerField).Tag = ((ToHeaderField)target).Tag;
            expected = true;
            actual = headerField.Equals(target);
            Assert.AreEqual(expected, actual, "Same at HeaderField level2");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            ToHeaderField target = new ToHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "obj is null");

            obj = new ToHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Equivalent Objects");

            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same Reference");

            target = new ToHeaderField(new SipUri("sip:bob@bob.com"));
            obj = new ToHeaderField(new SipUri("sip:Fred@bob.com"));
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Different Addresses");

            obj = new ToHeaderField(new SipUri("sip:bob@bob.com"));
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same Addresses");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            ToHeaderField target = new ToHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new ToHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.DisplayName = "abc";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.Tag = "abc";
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            ((ToHeaderField)obj).Tag = "abc";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest3()
        {
            ToHeaderField target = new ToHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ToHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Tag = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Tag = "1";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ToHeaderField)other).Tag = "1";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DisplayName = "1";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ToHeaderField)other).DisplayName = "2";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("SIP:BOB@ALtanTA.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ToHeaderField)other).Uri = new SipUri("SIP:bob@altanta.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ToHeaderField)other).Uri = new SipUri("SIP:BOB@altanta.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FieldName
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void FieldNameTest()
        {
            HeaderFieldBase_Accessor target = new ToHeaderField_Accessor();
            string expected = "To";
            string actual;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the constructor");
            expected = "To";
            target.FieldName = expected;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the assignement");
        }

        /// <summary>
        ///A test for New
        ///</summary>
        [TestMethod]
        public void NewTest()
        {
            ToHeaderField actual;
            actual = new ToHeaderField();
            Assert.AreEqual("To", actual.FieldName);
            Assert.IsTrue(actual.GetStringValue().Length == 0);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ToHeaderField target = new ToHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tt:";
            target.Parse(value);
            expected = string.Empty;
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tt\t:\t";
            target.Parse(value);
            expected = string.Empty;
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tt\t:\t\"\"";
            target.Parse(value);
            expected = "\"\"";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tto\t:\t\"dis\"";
            target.Parse(value);
            expected = "\"dis\"";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tTO\r\n \t:\t\"dis\"<sips:!!!!!@123>";
            target.Parse(value);
            expected = "\"dis\" <sips:!!!!!@123>";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tTO\r\n \t:\t\"dis\r\n \"\t<sips:!!!!!@123>\r\n ";
            target.Parse(value);
            expected = "\"dis \" <sips:!!!!!@123>";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tTO\r\n \t:\t\"dis\r\n \"\t<sips:!!!!!@123>\r\n ; tag = abc";
            target.Parse(value);
            expected = "\"dis \" <sips:!!!!!@123>;tag=abc";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tTO\r\n \t:\t\"dis\r\n \"\t<sips:!!!!!@123>\r\n ; tag = abc \t\t; Pram";
            target.Parse(value);
            expected = "\"dis \" <sips:!!!!!@123>;tag=abc;Pram";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);

            value = "\tTO\r\n \t:\t\\t<sips:!!!!!@123>\r\n ; tag = abc \t\t; Pram; Param=123";
            target.Parse(value);
            expected = "<sips:!!!!!@123>;tag=abc;Pram;Param=123";
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToBytes
        ///</summary>
        [TestMethod]
        public void ToBytesTest()
        {
            HeaderFieldBase target = new ToHeaderField();
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("".ToCharArray());
            byte[] actual;
            actual = target.GetBytes();
            Assert.IsTrue(new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)).StartsWith("To: "), "Test the constructor");

            target.Parse("sip:123@bob.com");
            expected = System.Text.UTF8Encoding.UTF8.GetBytes("To: <sip:123@bob.com>".ToCharArray());
            actual = target.GetBytes();
            Assert.IsTrue(new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)).StartsWith("To: <sip:123@bob.com>"), "Test after assignment");
        }

        /// <summary>
        ///A test for ToChars
        ///</summary>
        [TestMethod]
        public void ToCharsTest()
        {
            HeaderFieldBase target = new ToHeaderField();
            char[] expected = "To: ".ToCharArray();
            char[] actual;
            actual = target.GetChars();
            Assert.IsTrue(new string(actual).StartsWith(new string(expected)), "Test the constructor");
            target.Parse("sip:&1b@bob.com");
            expected = "To: <sip:&1b@bob.com>".ToCharArray();
            actual = target.GetChars();
            Assert.IsTrue(new string(actual).StartsWith(new string(expected)), "Test after assignment");
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest()
        {
            SipUri To = new SipUri("sip:123@bob.com");
            ToHeaderField target = new ToHeaderField(To);
            Assert.AreEqual("<" + To.ToString() + ">", target.GetStringValue());
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest1()
        {
            ToHeaderField target = new ToHeaderField();
            string expected = string.Empty;
            Assert.AreEqual("To", target.FieldName);
            Assert.IsTrue(target.GetStringValue().Length == 0);
            Assert.AreEqual(false, target.HasParameters);

            Assert.AreEqual(false, target.AllowMultiple);
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest2()
        {
            SipUri uri = new SipUri();
            ToHeaderField target = new ToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri("sip:abc");
            target = new ToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "<sip:abc>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest3()
        {
            string uri = "sip:localhost";
            ToHeaderField target = new ToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "<sip:localhost>");
            Assert.IsTrue(target.HasParameters == false);

            uri = "sip:anny@123.89.4.564";
            target = new ToHeaderField("sip:anny@123.89.4.564");
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "<sip:anny@123.89.4.564>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest4()
        {
            SipUri uri = new SipUri();
            string displayName = string.Empty;
            ToHeaderField target = new ToHeaderField(uri, displayName);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri();
            displayName = "!!!";
            target = new ToHeaderField(uri, displayName);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "\"!!!\"");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri("sip:bob");
            displayName = "!!!";
            target = new ToHeaderField(uri, displayName);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "\"!!!\" <sip:bob>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest5()
        {
            ToHeaderField target = new ToHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ToHeaderFieldConstructorTest6()
        {
            SipUri uri = new SipUri();
            string displayName = string.Empty;
            string tag = string.Empty;
            ToHeaderField target = new ToHeaderField(uri, displayName, tag);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri();
            displayName = "abc";
            tag = string.Empty;
            target = new ToHeaderField(uri, displayName, tag);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "\"abc\"");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri();
            displayName = "abc";
            tag = "123";
            target = new ToHeaderField(uri, displayName, tag);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "\"abc\";tag=123");
            Assert.IsTrue(target.HasParameters == true);

            uri = new SipUri("sip:bo;b@fanny.com");
            displayName = "abc";
            tag = "123";
            target = new ToHeaderField(uri, displayName, tag);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "To");
            Assert.IsTrue(target.CompactName == "t");
            Assert.IsTrue(target.GetStringValue() == "\"abc\" <sip:bo;b@fanny.com>;tag=123");
            Assert.IsTrue(target.HasParameters == true);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            HeaderFieldBase target = new ToHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.ToString();
            Assert.AreNotEqual(expected, actual, "Test the constructor");
            target.Parse("sip:123");
            expected = "To: <sip:123>";
            actual = target.ToString();
            Assert.IsTrue(actual.StartsWith(expected), "Test after assignment");
        }

        /// <summary>
        ///A test for Valid
        ///</summary>
        [TestMethod]
        public void ValidTest()
        {
            HeaderFieldBase_Accessor target = new ToHeaderField_Accessor();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test the constructor");
            expected = true;
            target.Parse("sip:123");
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test after assignment");
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            ToHeaderField headerField = new ToHeaderField(new SipUri("sip:bob@gtel.com"),"display","tag");
            string expected = "To: \"display\" <sip:bob@gtel.com>;tag=tag";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "\"display\" <sip:bob@gtel.com>;tag=tag";
            ToHeaderField expected = new ToHeaderField(new SipUri("sip:bob@gtel.com"), "display", "tag");
            ToHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        #endregion Methods

        #region Other

        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion Other
    }
}