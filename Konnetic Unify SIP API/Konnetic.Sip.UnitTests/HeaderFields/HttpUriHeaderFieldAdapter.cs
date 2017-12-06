using System;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HttpUriHeaderFieldAdapter and is intended
    ///to contain all HttpUriHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HttpUriHeaderFieldAdapter
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
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            AbsoluteUriHeaderFieldBase other = new AlertInfoHeaderField("http://www.google.com/hhh.jpg");
            bool expected = true;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.AbsoluteUri = new Uri("http://www.GOOGLE.com/hhh.jpg");
             expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HttpUri
        ///</summary>
        [TestMethod]
        public void HttpUriTest()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            Uri expected = new Uri("http://www.konnetic.com/sounds/moo.wav");
            Uri actual;
            target.AbsoluteUri = expected;
            actual = target.AbsoluteUri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpUriTesta()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            Uri expected = null;
            target.AbsoluteUri = expected;
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            string expected = "<http://www.google.com/hhh.jpg>";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            string value = "http://www.MS.com/hhh.jpg\t";
            target.Parse(value);
            expected = "<http://www.ms.com/hhh.jpg>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = null;
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            AbsoluteUriHeaderFieldBase target = CreateHttpUriHeaderField();
            string expected = "<http://www.google.com/hhh.jpg>";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        internal virtual AbsoluteUriHeaderFieldBase CreateHttpUriHeaderField()
        {
            AbsoluteUriHeaderFieldBase target = new AlertInfoHeaderField("http://www.google.com/hhh.jpg");
            return target;
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