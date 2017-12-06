using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ChallengeHeaderFieldAdapter and is intended
    ///to contain all ChallengeHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ChallengeHeaderFieldAdapter
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

        [TestMethod]
        public void AlgorithmTest1()
        {
            for(int i = 0; i < 147; i++)
                {
                char c = (char)i;
                bool match = false;
                foreach(char c1 in Common.TOKEN.ToCharArray())
                    {
                    if(c == c1)
                    { match = true; }
                    }
                if(!match)
                    {
                    string val = new string(c, 1);
					bool t = AlgorithmThrowsError("a"+val+"a");
                    Assert.IsTrue(t, "Exception Not thrown on: " + val);
                    }
                }
        }

        /// <summary>
        ///A test for MessageQop
        ///</summary>
        [TestMethod]
        public void MessageQopTest()
        {
            ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
            string expected = string.Empty;
            string actual;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);

            expected = "1122, 32ahah";
            target.MessageQop = expected;
            actual = target.MessageQop;
            expected = "1122, 32ahah";
            Assert.AreEqual(expected, actual);

            expected = string.Empty;
            target.MessageQop = expected;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageQopTest1()
        {
            for(int i = 0; i < 126; i++)
                {
                char c = (char)i;
                bool match = false;
                foreach(char c1 in "-.!%*_+`'~,\\ \"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \f\t\n\r\v".ToCharArray())
                    {
                    if(c == c1)
                    { match = true; }
                    }
                if(!match)
                    {
                    string val = new string(c, 1);
                    Assert.IsTrue(MessageQopThrowsError(val), "Exception Not thrown on: " + val);
                    }
                }
        }

        public void NonceTest1()
        {
            for(int i = 0; i < 500; i++)
                {
                char c = (char)i;

                    string val = new string(c, 1);
                    Assert.IsFalse(NonceThrowsError(val), "Exception thrown on: " + val);
                }
        }

        public void OpaqueTest1()
        {
            for(int i = 0; i < 500; i++)
                {
                char c = (char)i;

                string val = new string(c, 1);
                Assert.IsFalse(OpaqueThrowsError(val), "Exception thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ChallengeHeaderFieldBase target = CreateChallengeHeaderField();
            string expected = "Digest domain=\"sip:bob@atlanta.com\", qop=\"auth\", nonce=\"afafabbaba454644646afbaabffbaf\", realm=\"566fafabbaba454644646afbaabffbaf\", stale=FALSE";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "";
            string value = string.Empty;
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Digest";
            target.Parse(value);
            expected = "Digest";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Digest qop=\"auth\"";
            target.Parse(value);
            expected = "Digest qop=\"auth\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Digest qop=\"auth\", domain=\"sip:bob@atlanta.com\"";
            target.Parse(value);
            expected = "Digest domain=\"sip:bob@atlanta.com\", qop=\"auth\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Proxy-Authenticate \t  :       Digest domain=\"sip:bob@atlanta.com\", stale=true";
            target.Parse(value);
            expected = "Digest domain=\"sip:bob@atlanta.com\", stale=TRUE";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " Proxy-aUthenticate  \r\n :\tDigest\tdomain=\"sip:bob@atlanta.com\"\t,\tstale=true, realm=\"566fafabbaba454644646afbaabffbaf\"";
            target.Parse(value);
            expected = "Digest domain=\"sip:bob@atlanta.com\", realm=\"566fafabbaba454644646afbaabffbaf\", stale=TRUE";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        public void RealmTest1()
        {
            for(int i = 0; i < 500; i++)
                {
                char c = (char)i;

                string val = new string(c, 1);
                Assert.IsFalse(RealmThrowsError(val), "Exception thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Stale
        ///</summary>
        [TestMethod]
        public void StaleTest()
        {
            ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
            bool expected = false;
            bool actual;
            actual = target.Stale;
            Assert.AreEqual(expected, actual);

            expected = true;
            target.Stale = expected;
            actual = target.Stale;
            Assert.AreEqual(expected, actual);

            expected = false;
            target.Stale = expected;
            actual = target.Stale;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            SchemeAuthHeaderFieldBase target = CreateChallengeHeaderField();
            string expected = "Digest domain=\"sip:bob@atlanta.com\", qop=\"auth\", nonce=\"afafabbaba454644646afbaabffbaf\", realm=\"566fafabbaba454644646afbaabffbaf\", stale=FALSE";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Algorithm = "MD5";
            expected = "Digest domain=\"sip:bob@atlanta.com\", qop=\"auth\", nonce=\"afafabbaba454644646afbaabffbaf\", realm=\"566fafabbaba454644646afbaabffbaf\", stale=FALSE, algorithm=MD5";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Nonce = "";
            expected = "Digest domain=\"sip:bob@atlanta.com\", qop=\"auth\", realm=\"566fafabbaba454644646afbaabffbaf\", stale=FALSE, algorithm=MD5";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        internal virtual ChallengeHeaderFieldBase CreateChallengeHeaderField()
        {
            ChallengeHeaderFieldBase target = new ProxyAuthenticateHeaderField("Digest");
            target.MessageQop = "auth";
            target.Nonce = "afafabbaba454644646afbaabffbaf";
            target.Opaque = "";
            target.Realm = "566fafabbaba454644646afbaabffbaf";
            target.Stale = false;
            target.Domain = new SipUri("sip:bob@atlanta.com");
            return target;
        }

        internal virtual ChallengeHeaderFieldBase CreateChallengeHeaderField1()
        {
            ChallengeHeaderFieldBase target = new ProxyAuthenticateHeaderField();
            return target;
        }

        private bool AlgorithmThrowsError(string val)
        {
            try
                {
                ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
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
                ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
                target.MessageQop = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool NonceThrowsError(string val)
        {
            try
                {
                ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
                target.Nonce = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool OpaqueThrowsError(string val)
        {
            try
                {
                ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
                target.Opaque = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool RealmThrowsError(string val)
        {
            try
                {
                ChallengeHeaderFieldBase target = CreateChallengeHeaderField1();
                target.Opaque = val;
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