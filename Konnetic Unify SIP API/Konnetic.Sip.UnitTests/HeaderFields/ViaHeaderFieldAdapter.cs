using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ViaHeaderFieldAdapter and is intended
    ///to contain all ViaHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ViaHeaderFieldAdapter
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
        ///A test for Branch
        ///</summary>
        [TestMethod]
        public void BranchTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = string.Empty;
            string actual;
            target.Branch = expected;
            actual = target.Branch;
            Assert.AreEqual(expected, actual);

            expected = Common.TOKEN;
            target.Branch = expected;
            actual = target.Branch;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BranchTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(BranchThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            HeaderFieldBase expected = new ViaHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
			Assert.AreNotEqual(expected, actual);
			((ViaHeaderField)actual).Branch = ((ViaHeaderField)expected).Branch;
			Assert.AreEqual(expected, actual);

            target.Branch = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).Branch = "abc";
            Assert.AreEqual(expected, actual);

            target.MulticastAddress = "abc1";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).MulticastAddress = "abc1";
            Assert.AreEqual(expected, actual);

            target.AddParameter(new SipParameter("param"));
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).AddParameter(new SipParameter("param"));
            Assert.AreEqual(expected, actual);

            target.Received = "123.678.987.554";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).Received = "123.678.987.554";
            Assert.AreEqual(expected, actual);

            target.SentBy = Common.ALPHA;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).SentBy = Common.ALPHA;
            Assert.AreEqual(expected, actual);

            target.TimeToLive = 10;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).TimeToLive = 10;
            Assert.AreEqual(expected, actual);

            target.Transport = TransportType.Sctp;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            ((ViaHeaderField)expected).Transport = TransportType.Sctp;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ViaHeaderField();
            expected = false;
            actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			expected = true;
			((ViaHeaderField)other).Branch = target.Branch;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

            target.SentBy="123";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).SentBy = "123";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Transport =  TransportType.Tls;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).Transport = TransportType.Tls;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.TimeToLive = 100;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).TimeToLive = 100;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Received = "122";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).Received = "122";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.MulticastAddress = "122";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).MulticastAddress = "122";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Branch = "122";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).Branch = "122";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.AddParameter("1", "2");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ViaHeaderField)other).AddParameter("1", "2");
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
            ViaHeaderField target = new ViaHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

			target.Branch = "";
            target.TimeToLive = 100;
			expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.SentBy = "ff";
			expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Received = "123";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Branch="1";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Key
        ///</summary>
        [TestMethod]
        public void KeyTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            target.Branch = "123";
            target.SentBy = "456";
            string expected="123/456";
            string actual;
            actual = target.Key;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MulticastAddress
        ///</summary>
        [TestMethod]
        public void MulticastAddressTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = string.Empty;
            string actual;
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual);

            expected = Common.ALPHANUM + "-:.%";
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MulticastAddressTest1()
        {
            string s = "_!~*'()";
            for(int i = 0; i < s.Length; i++)
                {
                string val = new string(s[i], 1);
                Assert.IsTrue(MulticastAddressThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "SIP/2.0/UNKNOWN";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "SIP/2.0/TCP";
            target.Parse(value);
            expected = "SIP/2.0/TCP";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Via\t:\tSIP\t/\t2.0\t/\tTCP";
            target.Parse(value);
            expected = "SIP/2.0/TCP";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP /\t2.0 / TCP   \r\n \t google.com";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP/2.0/\r\n TCP   \r\n \t google.com ; branch=678";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com;branch=678";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP/2.0/TCP   \r\n \t google.com ; branch=678;maddr=123.456.7.8";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com;branch=678;maddr=123.456.7.8";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP/2.0/TCP   \r\n \t google.com ; branch=678;maddr=123.456.7.8;received=4564::4466";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com;branch=678;maddr=123.456.7.8;received=4564::4466";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP/2.0/TCP   \r\n \t google.com ; branch=678;maddr=123.456.7.8;received=4564::4466;ttl=200";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com;branch=678;ttl=200;maddr=123.456.7.8;received=4564::4466";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tv\t:\tSIP/2.0/TCP   \r\n \t google.com ; branch=678;\t maddr=123.456.7.8\t ;\r\n  received=4564::4466;ttl=200;param=1";
            target.Parse(value);
            expected = "SIP/2.0/TCP google.com;branch=678;ttl=200;maddr=123.456.7.8;received=4564::4466;param=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProtocolName
        ///</summary>
        [TestMethod]
        public void ProtocolNameTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = "SIP";
			string actual;
			target.ProtocolName = expected;
            actual = target.ProtocolName;
            Assert.AreEqual(expected, actual);

			target = new ViaHeaderField("123");
            expected = "SIP";
            actual = target.ProtocolName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProtocolVersion
        ///</summary>
        [TestMethod]
        public void ProtocolVersionTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = "2.0";
            string actual;
            actual = target.ProtocolVersion;
            Assert.AreEqual(expected, actual);

            target = new ViaHeaderField();
			expected = "2.0";
            actual = target.ProtocolVersion;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Received
        ///</summary>
        [TestMethod]
        public void ReceivedTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = string.Empty;
            string actual;
            target.Received = expected;
            actual = target.Received;
            Assert.AreEqual(expected, actual);

            expected = Common.NUMBER + ".:%";
            target.Received = expected;
            actual = target.Received;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReceivedTest2()
        {
            string s = Common.HOSTRESERVED;
            for(int i = 0; i < s.Length; i++)
                {
                string val = new string(s[i], 1);
				bool b = ReceivedThrowsError(val);
                Assert.IsTrue(b, "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for SentBy
        ///</summary>
        [TestMethod]
        public void SentByTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            string expected = string.Empty;
            string actual;
            target.SentBy = expected;
            actual = target.SentBy;
            Assert.AreEqual(expected, actual);

            expected = Common.ALPHANUM+"-:.%";
            target.SentBy = expected;
            actual = target.SentBy;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentByTest1()
        {
            string s =  "_!~*'()";
            for(int i = 0; i < s.Length; i++)
                {
                string val = new string(s[i], 1);
                Assert.IsTrue(SentByThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for TimeToLive
        ///</summary>
        [TestMethod]
        public void TimeToLiveTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            byte? expected = 0;
            byte? actual;
            target.TimeToLive = expected;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual);

            expected = 10;
            target.TimeToLive = expected;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual);

            expected = 255;
            target.TimeToLive = expected;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ViaHeaderField target = new ViaHeaderField("111");
            string expected = "SIP/2.0/TCP ;branch=111";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new ViaHeaderField();
            target.Branch = "123";
            expected = "SIP/2.0/TCP ;branch=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ProtocolName = "";
            expected = "/2.0/TCP ;branch=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ProtocolVersion = "";
            expected = "//TCP ;branch=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Transport = TransportType.Unknown;
            expected = ";branch=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new ViaHeaderField();
            target.Transport = TransportType.Udp;
            target.Branch = "123";
            expected = "SIP/2.0/UDP ;branch=123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.TimeToLive = 130;
            expected = "SIP/2.0/UDP ;branch=123;ttl=130";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.SentBy = "google.com";
            expected = "SIP/2.0/UDP google.com;branch=123;ttl=130";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Received = "123.456.7.8";
            expected = "SIP/2.0/UDP google.com;branch=123;ttl=130;received=123.456.7.8";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.MulticastAddress = "123.456.7.8";
            expected = "SIP/2.0/UDP google.com;branch=123;ttl=130;received=123.456.7.8;maddr=123.456.7.8";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Transport
        ///</summary>
        [TestMethod]
        public void TransportTest()
        {
            ViaHeaderField target = new ViaHeaderField();
            TransportType expected = TransportType.Udp;
            TransportType actual;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual);

            expected = TransportType.Unknown;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual);

            expected = TransportType.Sctp;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ViaHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ViaHeaderFieldConstructorTest()
        { 
            ViaHeaderField target = new ViaHeaderField("123");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Via");
            Assert.IsTrue(target.CompactName == "v");
			Assert.IsTrue(target.GetStringValue() == "SIP/2.0/TCP ;branch=123");
			Assert.IsTrue(target.HasParameters == true);
			 
            target = new ViaHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Via");
            Assert.IsTrue(target.CompactName == "v");
            Assert.IsTrue(target.GetStringValue().StartsWith("SIP/2.0/TCP ;branch="));
            Assert.IsTrue(target.HasParameters == true);
        }

        /// <summary>
        ///A test for ViaHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ViaHeaderFieldConstructorTest1()
        {
            string branch = string.Empty;
            ViaHeaderField target = new ViaHeaderField(branch);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Via");
            Assert.IsTrue(target.CompactName == "v");
            Assert.IsTrue(target.GetStringValue().StartsWith("SIP/2.0/TCP"));
            Assert.IsTrue(target.HasParameters == false);

            branch = "bbb";
            target = new ViaHeaderField(branch);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Via");
            Assert.IsTrue(target.CompactName == "v");
			Assert.IsTrue(target.GetStringValue() == "SIP/2.0/TCP ;branch=bbb");
            Assert.IsTrue(target.Branch == "bbb");
            Assert.IsTrue(target.HasParameters == true);
        }

        /// <summary>
        ///A test for ViaHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ViaHeaderFieldConstructorTest2()
        {
            ViaHeaderField target = new ViaHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Via");
            Assert.IsTrue(target.CompactName == "v");
            Assert.IsTrue(target.GetStringValue().StartsWith("SIP/2.0/TCP ;branch="));
            Assert.IsTrue(target.HasParameters == true);
        }

        private bool BranchThrowsError(string val)
        {
            try
                {
                ViaHeaderField target = new ViaHeaderField();
                target.Branch = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool MulticastAddressThrowsError(string val)
        {
            try
                {
                ViaHeaderField target = new ViaHeaderField();
                target.MulticastAddress = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool ReceivedThrowsError(string val)
        {
            try
                {
                ViaHeaderField target = new ViaHeaderField();
                target.Received = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool SentByThrowsError(string val)
        {
            try
                {
                ViaHeaderField target = new ViaHeaderField();
                target.SentBy = val;
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