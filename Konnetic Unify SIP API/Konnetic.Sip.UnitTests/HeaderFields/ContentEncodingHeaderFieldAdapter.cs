using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContentEncodingHeaderFieldAdapter and is intended
    ///to contain all ContentEncodingHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContentEncodingHeaderFieldAdapter
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
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            HeaderFieldBase expected = new ContentEncodingHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target = new ContentEncodingHeaderField(Common.TOKEN);
            ((ContentEncodingHeaderField)expected).ContentEncoding = Common.TOKEN;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentEncodingHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentEncodingHeaderFieldConstructorTest()
        {
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();

            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Content-Encoding");
            Assert.IsTrue(target.CompactName == "e");
            Assert.IsTrue(target.GetStringValue() == "");
            string expected = "";
            string actual;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentEncodingHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentEncodingHeaderFieldConstructorTest1()
        {
            string contentEncoding = string.Empty;
            ContentEncodingHeaderField target = new ContentEncodingHeaderField(contentEncoding);

            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Content-Encoding");
            Assert.IsTrue(target.CompactName == "e");
            Assert.IsTrue(target.GetStringValue() == "");
            string expected = "";
            string actual;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            contentEncoding = Common.TOKEN;
            target = new ContentEncodingHeaderField(contentEncoding);
            expected = Common.TOKEN;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentEncoding
        ///</summary>
        [TestMethod]
        public void ContentEncodingTest()
        {
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            expected = string.Empty;
            target.ContentEncoding = expected;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            expected = "1";
            target.ContentEncoding = expected;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            expected = " ";
            target.ContentEncoding = expected;
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            expected = "   \r\n    ";
            target.ContentEncoding = expected;
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void ContentEncodingTest1()
        {
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            string actual;
            string expected = "   \r\n  abcdefghijklmnopqrstuvwxyzABCDEFGHIJK\r\nLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~  ";
            target.ContentEncoding = expected;
            expected = Common.TOKEN;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncodingTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(EncodingThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            ContentEncodingHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ContentEncodingHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.ContentEncoding = "hhh";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "HHH";
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
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "null";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "  ";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "  \r\n  ";
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
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = "   ";
            target.Parse(value);
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = "  \r\n ";
            target.Parse(value);
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = " e: \r\n ";
            target.Parse(value);
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = " e \t       : \r\n ! ";
            target.Parse(value);
            expected = "!";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = " ConTent-ENCODing: \r\n \r\n\t!\t";
            target.Parse(value);
            expected = "!";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);

            value = " ConTent-ENCODing  \r\n : \r\n  ! \r\n";
            target.Parse(value);
            expected = "!";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
            value = " ConTent-ENCODing: \r\n\t";
            target.Parse(value);
            expected = "";
            actual = target.ContentEncoding;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ContentEncodingHeaderField target = new ContentEncodingHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = "  ";
            expected = string.Empty;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = " !  ";
            expected = "!";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ContentEncoding = " !  ";
            expected = "Content-Encoding: !";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        private bool EncodingThrowsError(string val)
        {
            try
                {
                ContentEncodingHeaderField target = new ContentEncodingHeaderField();
                target.ContentEncoding = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
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