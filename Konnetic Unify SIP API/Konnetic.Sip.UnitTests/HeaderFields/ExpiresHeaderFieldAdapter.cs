using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ExpiresHeaderFieldAdapter and is intended
    ///to contain all ExpiresHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ExpiresHeaderFieldAdapter
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
            ExpiresHeaderField target = new ExpiresHeaderField();
            HeaderFieldBase expected = new ExpiresHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ExpiresHeaderField)expected).Seconds = ExpiresHeaderField.MaxSeconds;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Seconds = ExpiresHeaderField.MaxSeconds;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ExpiresHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ExpiresHeaderField)other).Seconds=1;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Seconds = 1;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Seconds = 1;
            expected = true;
            actual = other.Equals(target);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ExpiresHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ExpiresHeaderFieldConstructorTest()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Expires");
            Assert.IsTrue(target.CompactName == "Expires");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for ExpiresHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ExpiresHeaderFieldConstructorTest1()
        {
            int seconds = 0;
            ExpiresHeaderField target = new ExpiresHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Expires");
            Assert.IsTrue(target.CompactName == "Expires");
            Assert.IsTrue(target.GetStringValue() == "0");

            target = new ExpiresHeaderField(4294967295);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Expires");
            Assert.IsTrue(target.CompactName == "Expires");
            Assert.IsTrue(target.GetStringValue() == "4294967295");
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void ExpiresHeaderFieldConstructorTest1a()
        {
            long seconds = 4294967296;
            ExpiresHeaderField target = new ExpiresHeaderField(seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void ExpiresHeaderFieldConstructorTest1b()
        {
            long seconds = -1;
            ExpiresHeaderField target = new ExpiresHeaderField(seconds);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " \r\n  4294967295";
            target.Parse(value);
            expected = "4294967295";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " Expires   : \r\n  0";
            target.Parse(value);
            expected = "0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " Expires   : \r\n ";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tExpires\t:\t1234567890\t";
            target.Parse(value);
            expected = "1234567890";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipParseException))]
        public void ParseTest1()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            string value = " 4294967296";
            target.Parse(value);
        }

        /// <summary>
        ///A test for Seconds
        ///</summary>
        [TestMethod]
        public void SecondsTest()
        {
            SecondsHeaderFieldBase target = new ExpiresHeaderField();
            long? expected = null;
            long? actual;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);

            target.Seconds = expected;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);

            expected = 4294967295;
            target.Seconds = expected;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void SecondsTest1()
        {
            SecondsHeaderFieldBase target = new ExpiresHeaderField();
            long expected = 4294967296;
             target.Seconds = expected;
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void SecondsTest2()
        {
            SecondsHeaderFieldBase target = new ExpiresHeaderField();
            long expected = -1;
            target.Seconds = expected;
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void SecondsTest3()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            long expected = 4294967296;
            target.Seconds = expected;
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void SecondsTest4()
        {
            ExpiresHeaderField target = new ExpiresHeaderField();
            long expected = -1;
            target.Seconds = expected;
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