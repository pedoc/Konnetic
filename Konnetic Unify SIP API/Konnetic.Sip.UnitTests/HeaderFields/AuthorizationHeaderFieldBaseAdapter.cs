using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AuthorizationHeaderFieldBaseAdapter and is intended
    ///to contain all AuthorizationHeaderFieldBaseAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AuthorizationHeaderFieldBaseAdapter
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
        ///A test for CNonce
        ///</summary>
        [TestMethod]
        public void CNonceTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected = string.Empty;
            string actual;
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.CNonce = expected;
            actual = target.CNonce;
            expected = Common.QUOTEDSTRINGRESULT.ToLowerInvariant();
            Assert.AreEqual(expected, actual);

            expected = "";
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = CreateAuthorizationHeaderFieldBase();
            ((AuthorizationHeaderField)obj).Response = "ababa";
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.Response = "ababa";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            AuthorizationHeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = CreateAuthorizationHeaderFieldBase();
            other.Realm = "abcdef1233";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm = "abcdef1233";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = CreateAuthorizationHeaderFieldBase();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)other).Uri = new SipUri("sip:bob@henry.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:bob@henry.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)other).AddParameter("bob", "fred");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.AddParameter("bob", "fred");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)other).Realm = "abcdef1233";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm = "abcdef1233";
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
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Realm = "aaa";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NonceCount
        ///</summary>
        [TestMethod]
        public void NonceCountTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected = "";
            string actual;
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);

            expected = "";
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string value = "Digest";
            target.Parse(value);
            string expected = "Digest";
            string actual = target.Scheme;
            Assert.AreEqual(expected, actual);

            value = "Digest1 ";
            target.Parse(value);
            expected = "Digest1";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual);

            value = "Digest1 username=\"ú\\\" \r\n\\\\\r\n \\\"\"";
            target.Parse(value);
            expected = "ú\" \\ \"";
            actual = target.Username;
            Assert.AreEqual(expected, actual);

            value = "Digest1 username=\"ú\\\"\r\n\\\\\r\n \\\"\",\tqop=auth\t";
            target.Parse(value);
            expected = "auth";
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);

            value = "\r\nDigest1\tusername=\"ú\\\"\r\n\\\\\r\n \\\"\", qop=auth,\tnonce=\"1212abcdef\"";
            target.Parse(value);
            expected = "1212abcdef";
            actual = target.Nonce;
            Assert.AreEqual(expected, actual);

            value = "Digest1 username=\"ú\\\"\r\n\\\\\r\n \\\"\", qop=auth,\r\n nonce=\"1212abcdef\", nc=00000001, response=\"%ab%cd%ef123456\", realm=\"aaaa\", uri=\"sip:bob@johns.com\"";
            target.Parse(value);
            expected = "sip:bob@johns.com";
            actual = target.Uri.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Response
        ///</summary>
        [TestMethod]
        public void ResponseTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected = string.Empty;
            string actual;
            target.Response = expected;
            actual = target.Response;
            Assert.AreEqual(expected, actual);

            expected = Common.HEX;
            target.Response = expected;
            actual = target.Response;
            expected = "0123456789abcdefabcdef";
            Assert.AreEqual(expected, actual);

            expected = "";
            target.Response = expected;
            actual = target.Response;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Konnetic.Sip.SipFormatException))]
        public void ResponseTest1()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected ="s";
            target.Response = expected;
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected = "Digest";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Algorithm="MD5";
            expected = "Digest algorithm=MD5";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.CNonce = "abcd2332423";
            expected = "Digest algorithm=MD5, cnonce=\"abcd2332423\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.MessageQop = "token";
            expected = "Digest algorithm=MD5, cnonce=\"abcd2332423\", qop=token";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Nonce = "fffeedd124";
            expected = "Digest algorithm=MD5, cnonce=\"abcd2332423\", qop=token, nonce=\"fffeedd124\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Opaque = "aaaa";
            expected = "Digest algorithm=MD5, cnonce=\"abcd2332423\", qop=token, nonce=\"fffeedd124\", opaque=\"aaaa\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [TestMethod]
        public void UriTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            SipUri expected = new SipUri();
            SipUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);

            expected = new SipUri("sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice");
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Username
        ///</summary>
        [TestMethod]
        public void UsernameTest()
        {
            AuthorizationHeaderFieldBase target = CreateAuthorizationHeaderFieldBase();
            string expected = string.Empty;
            string actual;
            target.Username = expected;
            actual = target.Username;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.Username = expected;
            actual = target.Username;
            expected = Common.QUOTEDSTRINGRESULT;
            Assert.AreEqual(expected, actual); 

            expected = "";
            target.Username = expected;
            actual = target.Username;
            Assert.AreEqual(expected, actual);
        }

        internal virtual AuthorizationHeaderFieldBase CreateAuthorizationHeaderFieldBase()
        {
            AuthorizationHeaderFieldBase target = new AuthorizationHeaderField("Digest");
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