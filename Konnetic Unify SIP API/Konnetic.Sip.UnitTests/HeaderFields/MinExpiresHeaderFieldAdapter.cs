using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for MinExpiresHeaderFieldAdapter and is intended
    ///to contain all MinExpiresHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class MinExpiresHeaderFieldAdapter
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
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            HeaderFieldBase expected = new MinExpiresHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((MinExpiresHeaderField)expected).Seconds = 1;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Seconds = 1;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            MinExpiresHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new MinExpiresHeaderField();
            other.Seconds = 0;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Seconds = 4294967295;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Seconds = 4294967295;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinExpiresHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MinExpiresHeaderFieldConstructorTest()
        {
            long seconds = 0;
            MinExpiresHeaderField target = new MinExpiresHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Min-Expires");
            Assert.IsTrue(target.CompactName == "Min-Expires");
            Assert.IsTrue(target.GetStringValue() == "0");

            seconds = 1;
            target = new MinExpiresHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Min-Expires");
            Assert.IsTrue(target.CompactName == "Min-Expires");
            Assert.IsTrue(target.GetStringValue() == "1");

            seconds = 4294967295;
            target = new MinExpiresHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Min-Expires");
            Assert.IsTrue(target.CompactName == "Min-Expires");
            Assert.IsTrue(target.GetStringValue() == "4294967295");
        }

        /// <summary>
        ///A test for MinExpiresHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MinExpiresHeaderFieldConstructorTest1()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Min-Expires");
            Assert.IsTrue(target.CompactName == "Min-Expires");
            Assert.IsTrue(target.GetStringValue() == "0");
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void MinExpiresHeaderFieldConstructorTest2()
        {
            long seconds = -1;
            MinExpiresHeaderField target = new MinExpiresHeaderField(seconds);
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void MinExpiresHeaderFieldConstructorTest3()
        {
            long seconds = 4294967296;
            MinExpiresHeaderField target = new MinExpiresHeaderField(seconds);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tMin-Expires\t:\t\r\n 0";
            target.Parse(value);
            expected = "0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Min-EXpires: \r\n 5346\t";
            target.Parse(value);
            expected = "5346";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "     Min-Expires: \r\n\t4294967295";
            target.Parse(value);
            expected = "4294967295";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "     Min-Expires: \r\n\t";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
		[ExpectedException(typeof(SipParseException))]
        public void ParseTest1()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            string value = "Min-Expires: \r\n 4294967296";
            target.Parse(value);
        }

        [TestMethod]
        public void ParseTest2()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            string value = "Min-Expires: \r\n -5346";
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest4()
        //    {
        //    MinExpiresHeaderField target = new MinExpiresHeaderField();
        //    string value = "MinExpires: \r\n 4294967296";
        //    target.Parse(value);
        //    }
        [TestMethod]
        public void ParseTest3()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            string value = null;
            target.Parse(value);
            string expected = "0";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
		[ExpectedException(typeof(SipParseException))]
        public void ParseTest5()
        {
            MinExpiresHeaderField target = new MinExpiresHeaderField();
            string value = "Min-Expires: \r\n 99999999994294967296";
            target.Parse(value);
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