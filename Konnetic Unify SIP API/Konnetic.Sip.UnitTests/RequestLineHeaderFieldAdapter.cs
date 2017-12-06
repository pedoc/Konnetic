using Konnetic.Sip;
using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RequestLineHeaderFieldAdapter and is intended
    ///to contain all RequestLineHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RequestLineHeaderFieldAdapter
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
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            RequestLineHeaderField expected = null; // TODO: Initialize to an appropriate value
            RequestLineHeaderField actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        public void MethodTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            SipMethod expected = new SipMethod(); // TODO: Initialize to an appropriate value
            SipMethod actual;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            string value = string.Empty; // TODO: Initialize to an appropriate value
            target.Parse(value);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RequestLineHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RequestLineHeaderFieldConstructorTest()
        {
            string value = string.Empty; // TODO: Initialize to an appropriate value
            RequestLineHeaderField target = new RequestLineHeaderField(value);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for RequestLineHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RequestLineHeaderFieldConstructorTest1()
        {
            RequestLineHeaderField target = new RequestLineHeaderField();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for RequestUri
        ///</summary>
        [TestMethod]
        public void RequestUriTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            SipUri expected = null; // TODO: Initialize to an appropriate value
            SipUri actual;
            target.RequestUri = expected;
            actual = target.RequestUri;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Scheme
        ///</summary>
        [TestMethod]
        public void SchemeTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Scheme = expected;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Version
        ///</summary>
        [TestMethod]
        public void VersionTest()
        {
            RequestLineHeaderField target = new RequestLineHeaderField();
            string expected = string.Empty;
            string actual;
            target.Version = expected;
            actual = target.Version;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            RequestLineHeaderField headerField = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = string.Empty; // TODO: Initialize to an appropriate value
            RequestLineHeaderField expected = null; // TODO: Initialize to an appropriate value
            RequestLineHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
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