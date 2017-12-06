using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AuthenticationInfoHeaderFieldAdapter and is intended
    ///to contain all AuthenticationInfoHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AuthenticationInfoHeaderFieldAdapter
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
        ///A test for AuthenticationInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AuthenticationInfoHeaderFieldConstructorTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.CNonce == "");
            Assert.IsTrue(target.FieldName == "Authentication-Info");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.NextNonce == "");
            Assert.IsTrue(target.ResponseAuth == "");
            Assert.IsTrue(target.NonceCount == "");
            Assert.IsTrue(target.CompactName == "Authentication-Info");
        }

        /// <summary>
        ///A test for CNonce
        ///</summary>
        [TestMethod]
        public void CNonceTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = string.Empty;
            string actual;
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);

            expected = "nasdfadsh3333hhhh33hh3nasdfadsh3333hhhh33hh3";
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);

            expected = "Nasdfadsh3333hhhh33hh3nasdfadsh3333hhhh33hh3";
            target.CNonce = expected;
            actual = target.CNonce;
            expected = "nasdfadsh3333hhhh33hh3nasdfadsh3333hhhh33hh3";
            Assert.AreEqual(expected, actual);

            expected = "\"nasdfadsh3333hhhh33hh3nasdfadsh3333hhhh33hh3\"";
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);

            expected = "\"nasdfadsh3333hhhh33--%\"hh\"3nasdfadsh3333hhhh33hh3\"";
            target.CNonce = expected;
            actual = target.CNonce;
            Assert.AreEqual(expected, actual);

            expected = "ú3&&jkasdhfasdfhHHKJ792342";
            target.CNonce = expected;
            actual = target.CNonce;
            expected = "ú3&&jkasdhfasdfhhhkj792342";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            HeaderFieldBase expected = new AuthenticationInfoHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.NextNonce = "afdasfdasf";
            expected = new AuthenticationInfoHeaderField();
            ((AuthenticationInfoHeaderField)expected).NextNonce = "afdasfdasf";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual.GetStringValue() == "nextnonce=\"afdasfdasf\"");
             
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.MessageQop = "hhh";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = false;
            target.MessageQop = "";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.CNonce = "123";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
             

        }

        /// <summary>
        ///A test for MessageQop
        ///</summary>
        [TestMethod]
        public void MessageQopTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = string.Empty;
            string actual;
            target.MessageQop = expected;
            actual = target.MessageQop;
            Assert.AreEqual(expected, actual);

            expected = "jjjjjjjjjjjjjjjjjasdfasdf454352345jjjjjjjjjjjjjjjj";
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

        [TestMethod]
        public void NextCountAllTest1()
        {
            for(int i = 0; i < Common.HEXRESERVED.Length; i++)
                {
                string val = new string(Common.HEXRESERVED[i], 1);
                Assert.IsTrue(NextCountThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for NextNonce
        ///</summary>
        [TestMethod]
        public void NextNonceTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = string.Empty;
            string actual;
            target.NextNonce = expected;
            actual = target.NextNonce;
            Assert.AreEqual(expected, actual);

            expected = "111111111111111__11111111\"11111111111768768766760";
            target.NextNonce = expected;
            actual = target.NextNonce;
            Assert.AreEqual(expected, actual);

            expected = "ú3&&jkasdhfasdfhHHKJ792342";
            target.NextNonce = expected;
            actual = target.NextNonce;
            expected = "ú3&&jkasdhfasdfhhhkj792342";
            Assert.AreEqual(expected, actual);

            expected = "ú3&&JkasdhfasdfhHHKJ792342";
            target.NextNonce = expected;
            actual = target.NextNonce;
            expected = "ú3&&JkasdhfasdfhHHKJ792342";
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for NonceCount
        ///</summary>
        [TestMethod]
        public void NonceCountTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = "Adfa45";
            string actual;
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual("adfa45", actual);

            expected = "111110";
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);

            expected = "";
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void NonceCountTest1()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = "asdfasd88aa9s";
            string actual;
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void NonceCountTest2()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = "g";
            string actual;
            target.NonceCount = expected;
            actual = target.NonceCount;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
       // [ExpectedException(typeof(SipException))] //TODO Should we throw here (parsing q parameter)?
        public void GenericParametersTest()
            {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string value = "Authentication-Info  \t : nextnonce=\"4asdASDhwoipjwnacasdc46abcdef0123456789d\"\t;q=0.1";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "nextnonce=\"4asdasdhwoipjwnacasdc46abcdef0123456789d\"");
                        
            }
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.ToString() == "Authentication-Info: ");
            Assert.IsTrue(target.GetStringValue() == "");

            value = "Authentication-Info  \t : nextnonce=\"4asdASDhwoipjwnacasdc46abcdef0123456789d\"\t";
            target.Parse(value);
            Assert.IsTrue(target.ToString() == "Authentication-Info: nextnonce=\"4asdasdhwoipjwnacasdc46abcdef0123456789d\"");
            Assert.IsTrue(target.GetStringValue() == "nextnonce=\"4asdasdhwoipjwnacasdc46abcdef0123456789d\"");
            Assert.IsTrue(target.NextNonce == "4asdasdhwoipjwnacasdc46abcdef0123456789d");

            value = "    Authentication-Info     : \r\n nextnonce=\"0123456789abcdefgHGSYUGAGJhhg\",\r\n nc=0%ab34%f6756";
            target.Parse(value);

            string expected = "Authentication-Info: nextnonce=\"0123456789abcdefghgsyugagjhhg\", nc=0%ab34%f6756";
            Assert.IsTrue(target.ToString() == expected);
            expected = "nextnonce=\"0123456789abcdefghgsyugagjhhg\", nc=0%ab34%f6756";
            Assert.IsTrue(target.GetStringValue() == expected);
            Assert.IsTrue(target.NonceCount == "0%ab34%f6756");

            value = " cnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\",  nc=00000001,   \r\n    rspauth=\"42%ce3%ce%f44%b22%f50%c6%a6071%bc8\"\r\n, genericparam=genericparamname";

            target.Parse(value);
            Assert.IsTrue(target.NonceCount == "00000001");
            Assert.IsTrue(target.ResponseAuth == "42%ce3%ce%f44%b22%f50%c6%a6071%bc8");
            Assert.IsTrue(target.CNonce == "wf84f1ceczx41ae6cbe5aea9c8e88d359");
            Assert.IsTrue(target.HasParameters == true);
 
        }

        [TestMethod]
        public void ResponseAuthAllTest1()
        {
            for(int i = 0; i < Common.HEXRESERVED.Length; i++)
                {
                string val = new string(Common.HEXRESERVED[i], 1);
                Assert.IsTrue(ResponseAuthThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for ResponseAuth
        ///</summary>
        [TestMethod]
        public void ResponseAuthTest()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = string.Empty;
            string actual;
            target.ResponseAuth = expected;
            actual = target.ResponseAuth;
            Assert.AreEqual(expected, actual);

            expected = "\"\"";
            target.ResponseAuth = expected;
            actual = target.ResponseAuth;
            Assert.AreEqual(expected, actual);

            expected = "\"\"\"\"";
            target.ResponseAuth = expected;
            actual = target.ResponseAuth;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void ResponseAuthTest1()
        {
            AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
            string expected = "\"\"\r\n\"\"";
            string actual;
            target.ResponseAuth = expected;
            actual = target.ResponseAuth;
            Assert.AreEqual("\"\\\"\\\"\"", actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ParamatizedHeaderFieldBase target = new AuthenticationInfoHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

             
            string value = "Authentication-Info: nextnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\", qop=Auth, nc=00000001, response=\"42%ce3%ce%f44%b22%f50%c6%a6071%bc8\"";
            target.Parse(value);
            expected = "nextnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\", qop=Auth, nc=00000001" ;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            AuthenticationInfoHeaderField headerField = new AuthenticationInfoHeaderField();

            string value = " cnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\",  nc=00000001,   \r\n    response=\"42%ce3%ce%f44%b22%f50%c6%a6071%bc8\"\r\n";

            headerField.Parse(value);
            string expected = "Authentication-Info: nc=00000001, cnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\"";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "Authentication-Info: nextnonce=\"wf84f1ceczx41ae6cbe5aea9c8e88d359\", qop=Auth, nc=00000001, rspauth=\"42%ce3%ce%f44%b22%f50%c6%a6071%bc88\"";
            AuthenticationInfoHeaderField expected = new AuthenticationInfoHeaderField();
            expected.NextNonce = "wf84f1ceczx41ae6cbe5aea9c8e88d359";
            expected.MessageQop = "Auth";
            expected.NonceCount = "00000001";
            expected.ResponseAuth = "42%ce3%ce%f44%b22%f50%c6%a6071%bc88";
            AuthenticationInfoHeaderField actual = new AuthenticationInfoHeaderField();
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        private bool MessageQopThrowsError(string val)
        {
            try
                {
                AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
                target.MessageQop = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool NextCountThrowsError(string val)
        {
            try
                {
                AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
                target.NonceCount = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool ResponseAuthThrowsError(string val)
        {
            try
                {
                AuthenticationInfoHeaderField target = new AuthenticationInfoHeaderField();
                target.ResponseAuth = val;
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