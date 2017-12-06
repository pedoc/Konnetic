using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContentLengthHeaderFieldAdapter and is intended
    ///to contain all ContentLengthHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContentLengthHeaderFieldAdapter
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
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            HeaderFieldBase expected = new ContentLengthHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Length = 9;
            ((ContentLengthHeaderField)expected).Length = 9;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentLengthHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentLengthHeaderFieldConstructorTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Length");
            Assert.IsTrue(target.CompactName == "l");
            Assert.IsTrue(target.GetStringValue() == "0");
        }

        /// <summary>
        ///A test for ContentLengthHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentLengthHeaderFieldConstructorTest1()
        {
            int length = 1;
            ContentLengthHeaderField target = new ContentLengthHeaderField(length);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Length");
            Assert.IsTrue(target.CompactName == "l");
            Assert.IsTrue(target.GetStringValue() == "1");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            ContentLengthHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Length = 9;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ContentLengthHeaderField();
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Length = 8;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Length = 8;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Length = 0;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Length = 8;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Length = 0;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void LengthTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            int? expected = -2;
            int? actual;
            target.Length = expected;
            actual = target.Length;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Length
        ///</summary>
        [TestMethod]
        public void LengthTest1()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            int? expected = 0;
            int? actual;
            target.Length = expected;
            actual = target.Length;
            Assert.AreEqual(expected, actual);

            expected = 11111110;
            target.Length = expected;
            actual = target.Length;
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest1()
        //    {
        //    ContentLengthHeaderField target = new ContentLengthHeaderField();
        //    string value = "   h123123h123 " ;
        //    target.Parse(value);
        //    }
        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest2()
        //    {
        //    ContentLengthHeaderField target = new ContentLengthHeaderField();
        //    string value = " Content-LenGTH:  -66 ";
        //    target.Parse(value);
        //    }
        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest3()
        //    {
        //    ContentLengthHeaderField target = new ContentLengthHeaderField();
        //    string value = " Content-LenGTH:  h123123h123 ";
        //    target.Parse(value);
        //    }
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            string value = string.Empty;
            target.Parse(value);

            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.ToString() == "Content-Length: ");

            value = "  \tContent-LenGTH  \t:  \r\n  8";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "8");
            Assert.IsTrue(target.ToString() == "Content-Length: 8");

            value = "  L\t:\t  \r\n 6";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "6");
            Assert.IsTrue(target.ToString() == "Content-Length: 6");

            value = "  \r\n  564768456 \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "564768456");
            Assert.IsTrue(target.ToString() == "Content-Length: 564768456");

            value = "  \r\n  0123456789 \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "123456789");
            Assert.IsTrue(target.ToString() == "Content-Length: 123456789");
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ContentLengthHeaderField target = new ContentLengthHeaderField();
            string expected = "0";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Length = 45467;
            expected = "45467";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Length = 0;
            expected = "0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
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