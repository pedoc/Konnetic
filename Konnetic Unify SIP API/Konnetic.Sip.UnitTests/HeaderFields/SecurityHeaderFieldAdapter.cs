using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for SecurityHeaderFieldAdapter and is intended
    ///to contain all SecurityHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SecurityHeaderFieldAdapter
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
        ///A test for Algorithm
        ///</summary>
        [TestMethod]
        public void AlgorithmTest()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = string.Empty;
            string actual;
            target.Algorithm = expected;
            actual = target.Algorithm;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AlgorithmTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(AlgorithmThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
        SchemeAuthHeaderFieldBase_Accessor target = CreateSecurityHeaderField_Accessor();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj=CreateSecurityHeaderField_Accessor();
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
        SchemeAuthHeaderFieldBase_Accessor target = CreateSecurityHeaderField_Accessor();
        SchemeAuthHeaderFieldBase_Accessor other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = CreateSecurityHeaderField_Accessor();
            other.Scheme = "abc";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Scheme = "abc";
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
        SchemeAuthHeaderFieldBase_Accessor target = CreateSecurityHeaderField_Accessor();
            HeaderFieldBase_Accessor other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = CreateSecurityHeaderField_Accessor();
            ((SchemeAuthHeaderFieldBase_Accessor)other).Algorithm = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Algorithm = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField_Accessor)other).MessageQop = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField_Accessor)target).MessageQop = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((SchemeAuthHeaderFieldBase_Accessor)other).Nonce = Common.QUOTEDSTRING;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Nonce = Common.QUOTEDSTRING;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((SchemeAuthHeaderFieldBase_Accessor)other).Opaque = Common.QUOTEDSTRING;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Opaque = Common.QUOTEDSTRING;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((SchemeAuthHeaderFieldBase_Accessor)other).Realm = Common.QUOTEDSTRING;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm = Common.QUOTEDSTRING;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((SchemeAuthHeaderFieldBase_Accessor)other).Scheme = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Scheme = Common.TOKEN;
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
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            string value = "Digest algorithm=123, qop=\"" + Common.QUOTEDSTRING + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\",param=value";
            target.Parse(value);
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MessageQop
        ///</summary>
        [TestMethod]
        public void MessageQopTest()
        {
            ProxyAuthenticateHeaderField target = new ProxyAuthenticateHeaderField(Common.TOKEN);
            string expected = string.Empty;
            string actual;
            target.MessageQop = expected;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);

            expected = "\"auth,auth1\"";
            target.MessageQop = expected;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);

            expected = "auth,"+Common.TOKEN;
            target.MessageQop = expected;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageQopTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(MessageQopThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Nonce
        ///</summary>
        [TestMethod]
        public void NonceTest()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = string.Empty;
            string actual;
            target.Nonce = expected;
            actual = target.Nonce;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.Nonce = expected;
            actual = target.Nonce;
            expected = Common.QUOTEDSTRINGRESULT.ToLower();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NonceTest1()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\"";
            string actual;
            target.Nonce = expected;
            actual = target.Nonce;
            Assert.IsTrue(actual == "\"");
        }

        [TestMethod]
        public void NonceTest2()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\\";
            string actual;
            target.Nonce = expected;
            actual = target.Nonce;
            Assert.IsTrue(actual == "\\");
        }

        /// <summary>
        ///A test for Opaque
        ///</summary>
        [TestMethod]
        public void OpaqueTest()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = string.Empty;
            string actual;
            target.Opaque = expected;
            actual = target.Opaque;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.Opaque = expected;
            actual = target.Opaque;
            expected = Common.QUOTEDSTRINGRESULT.ToLowerInvariant();
            Assert.AreEqual(expected, actual); 
        }

        [TestMethod]
        public void OpaqueTest1()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\"";
            string actual;
            target.Opaque = expected;
            actual = target.Opaque;
            Assert.IsTrue(actual == "\"");
        }

        [TestMethod]
        public void OpaqueTest2()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\\";
            string actual;
            target.Opaque = expected;
            actual = target.Opaque;
            Assert.IsTrue(actual == "\\");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tDigest\t algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\", param=value";
            target.Parse(value);
            expected = "Digest realm=\"123456789abcdef\", nonce=\"123456789abcdef\", algorithm=123, opaque=\"123456789abcdef\", qop=\"" + Common.TOKEN + "\", param=value";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Realm
        ///</summary>
        [TestMethod]
        public void RealmTest()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = string.Empty;
            string actual;
            target.Realm = expected;
            actual = target.Realm;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.Realm = expected;
            actual = target.Realm;
            expected = Common.QUOTEDSTRINGRESULT;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RealmTest1()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\"";
            string actual;
            target.Realm = expected;
            actual = target.Realm;
            Assert.IsTrue(actual == "\"");
        }

        [TestMethod]
        public void RealmTest2()
        {
            SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
            string expected = "\\";
            string actual;
            target.Realm = expected;
            actual = target.Realm;
            Assert.IsTrue(actual == "\\");
        }

        /// <summary>
        ///A test for Scheme
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void SchemeTest()
        {
        SchemeAuthHeaderFieldBase_Accessor target = new ProxyAuthenticateHeaderField_Accessor();
            string expected = "Digest2";
            string actual;
            target.Scheme = expected;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        [ExpectedException(typeof(SipFormatException))]
        public void SchemeTest2()
        {
            SchemeAuthHeaderFieldBase target = new ProxyAuthenticateHeaderField(Common.TOKENRESERVED);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
        SchemeAuthHeaderFieldBase_Accessor target = CreateSecurityHeaderField_Accessor();
            string expected = "Digest2";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Algorithm="123";
            expected = "Digest2 algorithm=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField_Accessor)target).MessageQop = Common.TOKEN;
            expected = "Digest2 algorithm=123, qop=\"" + Common.TOKEN + "\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Nonce = "123456789abcdef";
            expected = "Digest2 algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Scheme = "Digest";
            expected = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Scheme = "";
            expected = "algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Scheme = "Digest";
            expected = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Realm = "123456789abcdef";
            expected = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Opaque = "123456789abcdef";
            expected = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((ProxyAuthenticateHeaderField_Accessor)target).AddParameter("param", "value");
            expected = "Digest algorithm=123, qop=\"" + Common.TOKEN + "\", nonce=\"123456789abcdef\", realm=\"123456789abcdef\", opaque=\"123456789abcdef\", param=value";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        internal virtual SchemeAuthHeaderFieldBase CreateSecurityHeaderField()
        {
            SchemeAuthHeaderFieldBase target = new ProxyAuthenticateHeaderField("Digest2");
            return target;
        }

        internal virtual SchemeAuthHeaderFieldBase_Accessor CreateSecurityHeaderField_Accessor()
        {
        SchemeAuthHeaderFieldBase_Accessor target = new ProxyAuthenticateHeaderField_Accessor("Digest2");
            return target;
        }

        private bool AlgorithmThrowsError(string val)
        {
            try
                {
                SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
                target.Algorithm = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool MessageQopThrowsError(string val)
        {
            try
                {
                SchemeAuthHeaderFieldBase target = CreateSecurityHeaderField();
                target.Algorithm = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
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