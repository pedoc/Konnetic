using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for WwwAuthenticateHeaderFieldAdapter and is intended
    ///to contain all WwwAuthenticateHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class WwwAuthenticateHeaderFieldAdapter
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
            WwwAuthenticateHeaderField target = new WwwAuthenticateHeaderField();
            HeaderFieldBase expected = new WwwAuthenticateHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Nonce="123";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((WwwAuthenticateHeaderField)expected).Nonce = "123";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            WwwAuthenticateHeaderField target = new WwwAuthenticateHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm = "abc";
            other = new WwwAuthenticateHeaderField();
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            ((WwwAuthenticateHeaderField)target).Realm = "abc";
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            WwwAuthenticateHeaderField target = new WwwAuthenticateHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\"";
            target.Parse(value);
            expected = "Digest realm=\"123456789abcdef\", nonce=\"123456789abcdef\", algorithm=123, opaque=\"123456789abcdef\", qop=\"" + Common.TOKEN + "\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tWWW-AUTHEnticate   :\tDigest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\"";
            target.Parse(value);
            expected = "Digest realm=\"123456789abcdef\", nonce=\"123456789abcdef\", algorithm=123, opaque=\"123456789abcdef\", qop=\"" + Common.TOKEN + "\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WwwAuthenticateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void WwwAuthenticateHeaderFieldConstructorTest()
        {
            WwwAuthenticateHeaderField target = new WwwAuthenticateHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "WWW-Authenticate");
            Assert.IsTrue(target.CompactName == "WWW-Authenticate");
            Assert.IsTrue(target.GetStringValue() == "Digest");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for WwwAuthenticateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void WwwAuthenticateHeaderFieldConstructorTest1()
        {
            string scheme = string.Empty;
            WwwAuthenticateHeaderField target = new WwwAuthenticateHeaderField(scheme);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "WWW-Authenticate");
            Assert.IsTrue(target.CompactName == "WWW-Authenticate");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);

            scheme = "Digest1";
             target = new WwwAuthenticateHeaderField(scheme);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "WWW-Authenticate");
            Assert.IsTrue(target.CompactName == "WWW-Authenticate");
            Assert.IsTrue(target.GetStringValue() == "Digest1");
            Assert.IsTrue(target.HasParameters == false);
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