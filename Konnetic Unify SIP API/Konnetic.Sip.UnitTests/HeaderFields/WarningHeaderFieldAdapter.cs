using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for WarningHeaderFieldAdapter and is intended
    ///to contain all WarningHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class WarningHeaderFieldAdapter
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

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            WarningHeaderField target = new WarningHeaderField();
            HeaderFieldBase expected = new WarningHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Code=100;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((WarningHeaderField)expected).Code = 100;
            Assert.AreEqual(expected, actual);

            target.Text = "bbb";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((WarningHeaderField)expected).Text = "bbb";
            Assert.AreEqual(expected, actual);

            target.Agent = "xyz.com:5558";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((WarningHeaderField)expected).Agent = "xyz.com:5558";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            WarningHeaderField target = new WarningHeaderField();
            WarningHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Text = Common.QUOTEDSTRING;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new WarningHeaderField();
            other.Text = Common.QUOTEDSTRING;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Agent = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Agent = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Code = 103;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Code = 103;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            WarningHeaderField target = new WarningHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Text = "xyz";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Agent = "xyz";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Code = 100;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Text = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Agent = "";
            target.Text = "xyz";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Agent = "";
            target.Text = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            WarningHeaderField target = new WarningHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "\"\"";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "345";
            target.Parse(value);
            expected = "345 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Code == 345);

            value = "\r\n 345\tgoogle.com:80\t ";
            target.Parse(value);
            expected = "345 google.com:80 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Code == 345);
            Assert.IsTrue(target.Agent == "google.com:80");

            value = "Warning\t           :\r\n 345\t123.145.168.199:5464\t";
            target.Parse(value);
            expected = "345 123.145.168.199:5464 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Agent == "123.145.168.199:5464");

            value = "Warning\t           :\r\n 345\t123.145.168.199:5464\t \"\"\t";
            target.Parse(value);
            expected = "345 123.145.168.199:5464 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Code == 345);
            Assert.IsTrue(target.Agent == "123.145.168.199:5464");
            Assert.IsTrue(target.Text == "");

            value = "\tWarning\t           :\r\n 345\t123.145.168.199:5464\t \"\\\t\"\t";
            target.Parse(value);
            expected = "345 123.145.168.199:5464 \"\\\t\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Code == 345);
            Assert.IsTrue(target.Agent == "123.145.168.199:5464");
            Assert.IsTrue(target.Text == "\t");

            value = "\tWarning\t           :\r\n 345\t123.145.168.199:5464\t \"" + Common.QUOTEDSTRINGESCAPED + "\"\t";
            target.Parse(value);
            expected = "345 123.145.168.199:5464 \"" + Common.QUOTEDSTRINGESCAPED + "\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Code == 345);
            Assert.IsTrue(target.Agent == "123.145.168.199:5464");
            Assert.IsTrue(target.Text == Common.QUOTEDSTRINGRESULT);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            WarningHeaderField target = new WarningHeaderField();
            string expected = "\"\"";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Text = "";
            expected = "\"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Code = (short)SipWarningCode.IncompatibleTransportProtocol;
            expected = "302 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Agent = "";
            expected = "302 \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Agent = "bbb.com";
            expected = "302 bbb.com \"\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Text = "The quick brown fox";
            expected = "302 bbb.com \"The quick brown fox\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WarningHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void WarningHeaderFieldConstructorTest()
        {
            SipWarningCode code = SipWarningCode.AttributeNotUnderstood;
            string agent = "abc";
            string text = "abc";
            WarningHeaderField target = new WarningHeaderField(code, agent, text);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Warning");
            Assert.IsTrue(target.CompactName == "Warning");
            Assert.IsTrue(target.GetStringValue() == "306 abc \"abc\"");
            Assert.IsTrue(target.Code == 306);
            Assert.IsTrue(target.Agent == "abc");
            Assert.IsTrue(target.Text == "abc");
        }

        /// <summary>
        ///A test for WarningHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void WarningHeaderFieldConstructorTest1()
        {
            WarningHeaderField target = new WarningHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Warning");
            Assert.IsTrue(target.CompactName == "Warning");
            Assert.IsTrue(target.GetStringValue() == "\"\"");
            Assert.IsTrue(target.Code == null);
            Assert.IsTrue(target.Agent == string.Empty);
            Assert.IsTrue(target.Text == string.Empty);
        }

        /// <summary>
        ///A test for WarningHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void WarningHeaderFieldConstructorTest2()
        {
            short code = 100;
            string agent = string.Empty;
            string text = string.Empty;
            WarningHeaderField target = new WarningHeaderField(code, agent, text);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Warning");
            Assert.IsTrue(target.CompactName == "Warning");
            Assert.IsTrue(target.GetStringValue() == "100 \"\"");
            Assert.IsTrue(target.Code == 100);
            Assert.IsTrue(target.Agent == string.Empty);
            Assert.IsTrue(target.Text == string.Empty);

            code = 100;
            agent = Common.TOKEN;
            text = Common.QUOTEDSTRING;
            target = new WarningHeaderField(code, agent, text);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Warning");
            Assert.IsTrue(target.CompactName == "Warning");

            Assert.IsTrue(target.GetStringValue() == "100 " + Common.TOKEN + " \"" + Common.QUOTEDSTRINGESCAPED + "\"");
            Assert.IsTrue(target.Code == 100);
            Assert.IsTrue(target.Agent == Common.TOKEN);
            Assert.IsTrue(target.Text == Common.QUOTEDSTRINGRESULT);

            code = 100;
            agent = Common.HOSTUNRESERVED;
            text = Common.QUOTEDSTRING;
            target = new WarningHeaderField(code, agent, text);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Warning");
            Assert.IsTrue(target.CompactName == "Warning");
            Assert.IsTrue(target.GetStringValue() == "100 " + Common.HOSTUNRESERVED + " \"" + Common.QUOTEDSTRINGESCAPED + "\"");
            Assert.IsTrue(target.Code == 100);
            Assert.IsTrue(target.Agent == Common.HOSTUNRESERVED);
            Assert.IsTrue(target.Text == Common.QUOTEDSTRINGRESULT);
        }

        #endregion Methods

        #region Other

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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