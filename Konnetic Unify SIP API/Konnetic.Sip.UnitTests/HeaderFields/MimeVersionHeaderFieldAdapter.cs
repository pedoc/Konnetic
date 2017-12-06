using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for MimeVersionHeaderFieldAdapter and is intended
    ///to contain all MimeVersionHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class MimeVersionHeaderFieldAdapter
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
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            HeaderFieldBase expected = new MimeVersionHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.MajorVersion = 2;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((MimeVersionHeaderField)expected).MajorVersion = 2;
            Assert.AreEqual(expected, actual);

            target.MinorVersion = 1;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((MimeVersionHeaderField)expected).MinorVersion = 1;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new MimeVersionHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((MimeVersionHeaderField)other).MinorVersion = 255;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.MinorVersion = 255;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((MimeVersionHeaderField)other).MajorVersion = 255;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.MajorVersion = 255;
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
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MajorVersion
        ///</summary>
        [TestMethod]
        public void MajorVersionTest()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            byte? expected = 1;
            byte? actual;
            actual = target.MajorVersion;
            Assert.AreEqual(expected, actual);

            target.MajorVersion = expected;
            actual = target.MajorVersion;
            Assert.AreEqual(expected, actual);

            expected = 1;
            target.MajorVersion = expected;
            actual = target.MajorVersion;
            Assert.AreEqual(expected, actual);

            expected = 255;
            target.MajorVersion = expected;
            actual = target.MajorVersion;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MimeVersionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MimeVersionHeaderFieldConstructorTest()
        {
            byte majorVersion = 0;
            byte minorVersion = 0;
            MimeVersionHeaderField target = new MimeVersionHeaderField(majorVersion, minorVersion);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "MIME-Version");
            Assert.IsTrue(target.CompactName == "MIME-Version");
            Assert.IsTrue(target.GetStringValue() == "0.0");

            majorVersion = 1;
            minorVersion = 255;
            target = new MimeVersionHeaderField(majorVersion, minorVersion);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "MIME-Version");
            Assert.IsTrue(target.CompactName == "MIME-Version");
            Assert.IsTrue(target.GetStringValue() == "1.255");
        }

        /// <summary>
        ///A test for MimeVersionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MimeVersionHeaderFieldConstructorTest1()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "MIME-Version");
            Assert.IsTrue(target.CompactName == "MIME-Version");
            Assert.IsTrue(target.GetStringValue() == "1.0");

			target = new MimeVersionHeaderField(0,0);
			Assert.IsTrue(target.AllowMultiple == false);

			Assert.IsTrue(target.FieldName == "MIME-Version");
			Assert.IsTrue(target.CompactName == "MIME-Version");
			Assert.IsTrue(target.GetStringValue() == "0.0");
        }

        /// <summary>
        ///A test for MinorVersion
        ///</summary>
        [TestMethod]
        public void MinorVersionTest()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            byte? expected = 0;
            byte? actual;
            actual = target.MinorVersion;
            Assert.AreEqual(expected, actual);

            target.MinorVersion = expected;
            actual = target.MinorVersion;
            Assert.AreEqual(expected, actual);

            expected = 1;
            target.MinorVersion = expected;
            actual = target.MinorVersion;
            Assert.AreEqual(expected, actual);

            expected = 255;
            target.MinorVersion = expected;
            actual = target.MinorVersion;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tMiME-Version\t:\t255.9";
            target.Parse(value);
            expected = "255.9";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "   MiME-Version: \r\n 1.99\t";
            target.Parse(value);
            expected = "1.99";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " \t  MiME-Version: \r\n 1.i";
            target.Parse(value);
            expected = "1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipParseException))]
        public void ParseTest1()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            string value = "256.0";
            target.Parse(value);
        }

        [TestMethod]
        [ExpectedException(typeof(SipParseException))]
        public void ParseTest2()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            string value = "1.256";
            target.Parse(value);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            MimeVersionHeaderField target = new MimeVersionHeaderField();
            string expected = "1.0";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new MimeVersionHeaderField(2,0);
            expected = "2.0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new MimeVersionHeaderField();
            expected = "1.0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.MajorVersion = 255;
            target.MinorVersion = 255;
            expected = "255.255";
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