using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for SecurityHeaderFieldBaseAdapter and is intended
    ///to contain all SecurityHeaderFieldBaseAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SecurityHeaderFieldBaseAdapter
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
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            SecurityHeaderFieldBase target = CreateSecurityHeaderFieldBase();
            bool expected = false;
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
            SecurityHeaderFieldBase target = CreateSecurityHeaderFieldBase();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Digest algorithm=123, qop=" + Common.TOKEN + ", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\",param=value";
            target.Parse(value);
            expected = "Digest realm=\"123456789abcdef\", nonce=\"123456789abcdef\", algorithm=123, opaque=\"123456789abcdef\", qop=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~, param=value";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            SecurityHeaderFieldBase target = CreateSecurityHeaderFieldBase();
            string expected = "Scheme1";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)target).AddParameter("bob", "fanny\t");
            expected = "Scheme1 bob=fanny";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)target).Uri = new SipUri("sip:bob@billygoat.com");
            expected = "Scheme1 uri=\"sip:bob@billygoat.com\", bob=fanny";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        internal virtual SecurityHeaderFieldBase CreateSecurityHeaderFieldBase()
        {
            SecurityHeaderFieldBase target = new AuthorizationHeaderField("Scheme1");
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