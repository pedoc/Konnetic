using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ProxyAuthenticateHeaderFieldAdapter and is intended
    ///to contain all ProxyAuthenticateHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ProxyAuthenticateHeaderFieldAdapter
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
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField();
            HeaderFieldBase expected = new ProxyAuthenticateHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Algorithm = Common.TOKEN;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Algorithm = Common.TOKEN;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Algorithm = "";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Algorithm = "";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Domain = new System.Uri("http://www.google.com");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Domain = new System.Uri("http://www.google.com");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).MessageQop = "2345";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.MessageQop = "2345";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Nonce = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Nonce="abc";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Opaque = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Opaque = "abc";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Realm = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Realm = "abc";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)expected).Stale = true;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Stale = true;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ProxyAuthenticateHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)other).Algorithm = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Algorithm = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)other).Algorithm = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Algorithm = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)other).Domain = new SipUri("sip:bob@fanny.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Domain = new SipUri("sip:jim@fanny.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Domain = new SipUri("sip:bob@fanny.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)other).Realm = Common.ALPHANUM;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm = Common.ALPHANUM;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField)other).Stale = true;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Stale = true;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Stale = false;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            // ú\"\\
            value = @"Proxy-Authenticate: Digest realm=""ú\""\\""";
            target.Parse(value);
            expected = @"Digest realm=""ú\""\\""";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            value = "\tProxy-Authenticate\t:\tDigest realm=\"ú\\\"\",   domain=\"sip:bob@fred.com\"  , nonce=\"sdfasD9f69sa6f9asd6\", opaque=\"\", \r\n stale=FalSE, \r\n algorithm=mD5, qop=\"auth,auth-int\"";
            target.Parse(value);
            expected = "Digest";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual);
            expected = "ú\"";
            actual = target.Realm;
            Assert.AreEqual(expected, actual);
            expected = "sip:bob@fred.com";
            actual = target.Domain.ToString();
            Assert.AreEqual(expected, actual);
            expected = "sdfasd9f69sa6f9asd6";
            actual = target.Nonce;
            Assert.AreEqual(expected, actual);
            expected = "";
            actual = target.Opaque;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(false, target.Stale);
            expected = "MD5";
            actual = target.Algorithm;
            Assert.AreEqual(expected, actual);
            expected = "auth,auth-int";
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);
            expected = "False";
            actual = target.Stale.ToString();
            Assert.AreEqual(expected, actual);

            expected = "Digest domain=\"sip:bob@fred.com\", realm=\"ú\\\"\", nonce=\"sdfasd9f69sa6f9asd6\", algorithm=MD5, qop=\"auth,auth-int\", stale=FALSE";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tProxy-Authenticate\t:\tDigest realm=\"ú\\\"\",   domain=\"sip:bob@fred.com\"  , nonce=\"sdfasD9f69sa6f9asd6\", opaque=\"\", \r\n  algorithm=mD5, qop=\"auth,auth-int\"";
            target.Parse(value);
            expected = "Digest domain=\"sip:bob@fred.com\", realm=\"ú\\\"\", nonce=\"sdfasd9f69sa6f9asd6\", algorithm=MD5, qop=\"auth,auth-int\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            AuthHeaderFieldGroup<ProxyAuthenticateHeaderField> hfg = new AuthHeaderFieldGroup<ProxyAuthenticateHeaderField>();
            hfg.Parse("Proxy-Authenticate\t: Digest  domain=\"sip:bob@fred.com\", opaque=\"sdfasd9f69sa6f9asd6\" , algorithm=mD5 \r\n\tProxy-Authenticate: Digest  domain=\"sip:fanny@fred.com\", opaque=\"123\" , algorithm=MD7");

            expected = "sip:bob@fred.com";
            actual = hfg[0].Domain.ToString();
            Assert.AreEqual(expected, actual);

            expected = "sip:fanny@fred.com";
            actual = hfg[1].Domain.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProxyAuthenticateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ProxyAuthenticateHeaderFieldConstructorTest()
        {
            string scheme = string.Empty;
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField(scheme);
            Assert.IsTrue(target.Algorithm == string.Empty);
            Assert.IsTrue(target.Domain == new SipUri(SipUri.DEFAULTURI));
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.Nonce == "");
            Assert.IsTrue(target.Opaque == "");
            Assert.IsTrue(target.Realm == "");
            Assert.IsTrue(target.Scheme == "");
            Assert.IsTrue(target.Stale == false);

            scheme = "123";
            target = new ProxyAuthenticateHeaderField(scheme);
            Assert.IsTrue(target.Algorithm == string.Empty);
            Assert.IsTrue(target.Domain == new SipUri());
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.Nonce == "");
            Assert.IsTrue(target.Opaque == "");
            Assert.IsTrue(target.Realm == "");
            Assert.IsTrue(target.Scheme == "123");
            Assert.IsTrue(target.Stale == false);
        }

        /// <summary>
        ///A test for ProxyAuthenticateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ProxyAuthenticateHeaderFieldConstructorTest1()
        {
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField();
            Assert.IsTrue(target.Algorithm == string.Empty);
            Assert.IsTrue(target.Domain == new SipUri(SipUri.DEFAULTURI));
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.Nonce == "");
            Assert.IsTrue(target.Opaque == "");
            Assert.IsTrue(target.Realm == "");
            Assert.IsTrue(target.Scheme == "Digest");
            Assert.IsTrue(target.Stale == false);
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