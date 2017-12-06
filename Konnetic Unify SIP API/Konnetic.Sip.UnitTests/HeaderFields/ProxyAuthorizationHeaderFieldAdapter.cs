using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ProxyAuthorizationHeaderFieldAdapter and is intended
    ///to contain all ProxyAuthorizationHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ProxyAuthorizationHeaderFieldAdapter
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
            ProxyAuthorizationHeaderField target = new ProxyAuthorizationHeaderField();
            HeaderFieldBase expected = new ProxyAuthorizationHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthorizationHeaderField)expected).NonceCount = "345";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.NonceCount = "345";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ProxyAuthorizationHeaderField target = new ProxyAuthorizationHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ProxyAuthorizationHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthorizationHeaderField)other).Username = "abcdefg123";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Username = "abcdefg123";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ProxyAuthorizationHeaderField target = new ProxyAuthorizationHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual = string.Empty;

            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tProxy-Authorization\t: \tDigest \r\n realm=\"abcdef\"";
            target.Parse(value);
            expected = "Digest realm=\"abcdef\"";
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