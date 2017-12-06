using System;

using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
    {
    /// <summary>
    ///This is a test class for SipUriAdapter and is intended
    ///to contain all SipUriAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SipUriAdapter
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

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
            {
            if(!SipStyleUriParser.IsKnownScheme("sip"))
                {
                SipStyleUriParser p = new SipStyleUriParser();
                SipStyleUriParser.Register(p, "sip", 5060);
                SipStyleUriParser p1 = new SipStyleUriParser();
                SipStyleUriParser.Register(p1, "sips", 5060);
                }
            }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
            {
            object target = new SipUri("sip:alice:password@chicago.com");
            object obj = new SipUri("sip:alice:password@chicago.com");
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same");

            target = new SipUri("sip:alice:password@chicago.com");
            obj = new SipUri("sips:alice:password@chicago.com");
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "SIP AND SIPS");
            }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
            {
            SipUri target = new SipUri("sip:alice:password@chicago.com");
            object obj = new SipUri("sip:alice:password@chicago.com");
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same");

            target = new SipUri("sip:alice:password@chicago.com");
            obj = new SipUri("sips:alice:password@chicago.com");
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "SIP AND SIPS");
            }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
            {
            SipUri target = new SipUri("sip:alice:password@chicago.com");
            SipUri comparand = new SipUri("sip:alice:password@chicago.com");
            bool expected = true;
            bool actual;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Same");

            target = new SipUri("sip:alice:password@chicago.com");
            comparand = new SipUri("sips:alice:password@chicago.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "SIP AND SIPS");

            target = new SipUri("sip:alice:password@chicago.com");
            comparand = new SipUri("sip:Alice:password@chicago.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Case");

            target = new SipUri("sip:alice:password@chicago.com");
            comparand = new SipUri("sip:alice:Password@chicago.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Case2");

            target = new SipUri("sip:alice:password@chicago.com");
            comparand = new SipUri("sip:alice:password@Chicago.com");
            expected = true;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Case3");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alicea@bob.com&from=fred@bob.com");
            expected = true;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Ordering");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;method=INVITE;lr;user=fred;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = true;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Ordering2");

            target = new SipUri("sip::password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=sip:alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "No Name1");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "No Password");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com:9999;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Port");

            target = new SipUri("sip:alice:password@chicago.com:9560;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com:9561;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Explicit Port");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Explicit Transport");

            target = new SipUri("sip:alice:password@chicago.com;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Explicit TTL");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Explicit UserParam");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different Explicit Method");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=16;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different ttl");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=fred;lr;user=fred;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=bob;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Different MyOwn");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=fred;lr;user=fred;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=fred;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "No Maddr");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=fred;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            expected = true;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "Only one MyOwn");

            target = new SipUri("sip:alice:password@chicago.com;ttl=15;myown=fred;lr;user=fred;transport=tcp?from=fred@bob.com&to=alicea@bob.com");
            comparand = new SipUri("sip:alice:password@chicago.com;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alicea@bob.com");
            expected = false;
            actual = target.Equals(comparand);
            Assert.AreEqual(expected, actual, "No From Header");
            }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsTest2a()
            {
            SipUri target = new SipUri("sip::password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?from=fred@bob.com&to=alicea@bob.com&to=sip:alice1@bob.com");
            }

        ///// <summary>
        /////A test for New
        /////</summary>
        //[TestMethod()]
        //public void NewTest()
        //    {
        //    string uri = "sip:bob.com@bob.com";
        //    SipUri actual;
        //    actual = SipUri.New(uri);
        //    Assert.IsTrue(actual.ToString() == "sip:bob.com@bob.com");
        //    }
        /// <summary>
        ///A test for GetSipComponents
        ///</summary>
        [TestMethod]
        public void GetSipComponentsTest()
            {
            SipUri target = new SipUri("sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice%40bob.com&from=fred@bob.com");
            SipUriComponents components = SipUriComponents.Headers;
            string expected = "?to=alice@bob.com&from=fred@bob.com";
            string actual;
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Headers");

            components = SipUriComponents.Host;
            expected = "chicago.com";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "host");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.HostPort;
            expected = "chicago.com:1234";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "HostPort");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.LooseRouter;
            expected = "lr";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "lr");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Method;
            expected = "INVITE";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Method");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.MulticastAddress;
            expected = "239.255.255.1";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "maddr");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Parameters;
            expected = ";ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Parameters");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Password;
            expected = "password";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Password");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Port;
            expected = "1234";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Port");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Scheme;
            expected = "sip";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "Scheme");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.TimeToLive;
            expected = "15";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "TimeToLive");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.Transport;
            expected = "tcp";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "transport");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.None;
            expected = "";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "None");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            components = SipUriComponents.UserInfo;
            expected = "alice:password";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "UserInfo");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alicea@bob.com");
            components = SipUriComponents.UserName;
            expected = "alice";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "UserName");

            target = new SipUri("sip:alice:password@chicago.com:1234;ttl=15;lr;user=fred;method=INVITE;maddr=239.255.255.1;transport=tcp?to=alicea@bob.com");
            components = SipUriComponents.UserParameter;
            expected = "fred";
            actual = target.GetSipComponents(components);
            Assert.AreEqual(expected, actual, "UserParameter");
            }

        /// <summary>
        ///A test for Headers
        ///</summary>
        [TestMethod]
        public void HeadersTest()
            {
            Uri uri = new Uri("sip:bob.com?to=bob%40bob.com&jjj=jjj");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = "?to=bob@bob.com&jjj=jjj";
            actual = target.Headers;
            Assert.AreEqual(expected, actual, "Basic");
            }

        /// <summary>
        ///A test for HostPort
        ///</summary>
        [TestMethod]
        public void HostPortTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = "bob.com";
            actual = target.HostPort;
            Assert.AreEqual(expected, actual, "Just a host scheme");

            target = new SipUri(" sip:bob.com:1234");
            expected = "bob.com:1234";
            actual = target.HostPort;
            Assert.AreEqual(expected, actual, "Host + port");

            target = new SipUri(" sip:bob.com:0");
            expected = "bob.com:0";
            actual = target.HostPort;
            Assert.AreEqual(expected, actual, "Host + port2");
            }

        /// <summary>
        ///A test for IsSecureTransport
        ///</summary>
        [TestMethod]
        public void IsSecureTransportTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            bool actual;
            bool expected = false;
            actual = target.IsSecureTransport;
            Assert.AreEqual(expected, actual, "Default scheme");

            uri = new Uri("sips:bob.com");
            target = new SipUri(uri);
            expected = true;
            actual = target.IsSecureTransport;
            Assert.AreEqual(expected, actual, "Default scheme2");

            target = new SipUri("sips:bob.com");
            expected = true;
            actual = target.IsSecureTransport;
            Assert.AreEqual(expected, actual, "Default scheme3");

            target = new SipUri(" sip:bob.com");
            expected = false;
            actual = target.IsSecureTransport;
            Assert.AreEqual(expected, actual, "Default scheme4");
            }

        [TestMethod] 
        public void LrTest1()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            target = new SipUri("sip:bob.com;lr=");
            bool actual;
            bool expected = false;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "No Lr Parameter");
            }

        [TestMethod] 
        public void LrTest2()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            target = new SipUri("sip:bob.com;lr="); 
            bool actual;
            bool expected = false;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "No Lr Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void LrTest3()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            target = new SipUri("sip:bob.com;lr=aaa");
            }

        /// <summary>
        ///A test for Lr
        ///</summary>
        [TestMethod]
        public void LrTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            bool actual;
            bool expected = false;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "No Lr Parameter");

            target = new SipUri("sip:bob.com;lraaa;");
            expected = false;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "No Lr Parameter3");

            target = new SipUri("sip:bob.com;lR;");
            expected = true;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "Lr Parameter");

            target = new SipUri("sip:bob.com;LR;");
            expected = true;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "Lr Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void MaddrTest1()
            {
            Uri uri = new Uri("sip:bob.com;maddr");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "No Maddr Parameter");
            }
        /// <summary>
        ///A test for Maddr
        ///</summary>
        [TestMethod]
        public void MaddrTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "No Maddr Parameter");

            target = new SipUri("sip:bob.com;maddr=");
            expected = string.Empty;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "No Maddr Parameter2");             

            target = new SipUri("sip:bob.com;maddr=123.123.ggg.123");
            expected = "123.123.ggg.123";
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "123.123.ggg.123 Parameter");

            target = new SipUri("sip:bob.com;maddr=" + Common.HOSTUNRESERVED);
            expected = Common.HOSTUNRESERVED;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Common.HOSTUNRESERVED");
            }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        public void MethodTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "No Method Parameter");

            target = new SipUri("sip:bob.com;METHOD=566");
            expected = "566";
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Standard Method");

            target = new SipUri("sip:bob.com;METHOD=INvITE?method=BOB");
            expected = "INvITE";
            actual = target.Method;
            Assert.AreEqual(expected, actual, "INvITE Method");

            uri = new Uri("sip:bob.com;method=");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Empty Method");

            target = new SipUri("sip:bob.com;MethoD=null");
            expected = "null";
            actual = target.Method;
            Assert.AreEqual(expected, actual, "'null' Method");

            target = new SipUri("sip:bob.com;MethoD=" + Common.TOKEN);
            expected = Common.TOKEN;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "TOKEN Method");

            target = new SipUri("sip:bob.com;lr;method=" + Common.TOKEN);
            expected = Common.TOKEN;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "TOKEN Method2");
            }

        /// <summary>
        ///A test for Parameters
        ///</summary>
        [TestMethod]
        public void ParametersTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            actual = target.Parameters;
            string expected = string.Empty;
            Assert.AreEqual(expected, actual, "No Parameters");
            }

        /// <summary>
        ///A test for Password
        ///</summary>
        [TestMethod]
        public void PasswordTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "No Password");

            uri = new Uri("sips:bob.com@bob.com");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "No Password");

            uri = new Uri("sips::@bob.com");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "No Password");

            uri = new Uri("sips:;:@bob.com");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "No Password");

            uri = new Uri("sips:;:@bob.com");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "No Password");

            uri = new Uri("sips:a:@bob.com");
            target = new SipUri(uri);
            expected = "";
            actual = target.Password;
            Assert.AreEqual(expected, actual, "null Password");

            uri = new Uri("sips:a:123@bob.com");
            target = new SipUri(uri);
            expected = "123";
            actual = target.Password;
            Assert.AreEqual(expected, actual, "123 Password");

            uri = new Uri("sips:a:" + Common.ALPHANUM + "%&=+$," + "@bob.com");
            target = new SipUri(uri);
            expected = Common.ALPHANUM + "%&=+$,";
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Common.ALPHANUM+%&=+$ Password");
            }

        /// <summary>
        ///A test for SipRequestUrl
        ///</summary>
        //[TestMethod()]
        //public void SipRequestUrlTest()
        //    {
        //    Uri uri = null; // TODO: Initialize to an appropriate value
        //    SipUri target = new SipUri(uri); // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = target.SipRequestUrl;
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //    }
        /// <summary>
        ///A test for Scheme
        ///</summary>
        [TestMethod]
        public void SchemeTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = "sip";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Default scheme");

            uri = new Uri("sips:bob.com");
            target = new SipUri(uri);
            expected = "sips";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Default scheme2");

            target = new SipUri("sips:bob.com");
            expected = "sips";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Default scheme3");

            target = new SipUri(" sip:bob.com");
            expected = "sip";
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Default scheme4");
            }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void SchemeTest1()
            {
            Uri uri = new Uri("sip1:bob.com");
            SipUri target = new SipUri(uri);
            }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void SchemeTest2()
            {
            Uri uri = new Uri(":bob.com");
            SipUri target = new SipUri(uri);
            }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void SchemeTest3()
            {
            Uri uri = new Uri(" ssip:bob.com");
            SipUri target = new SipUri(uri);
            }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void SchemeTest4()
            {
            Uri uri = new Uri(":ssip:bob.com");
            SipUri target = new SipUri(uri);
            }

        /// <summary>
        ///A test for TimeToLive
        ///</summary>
        [TestMethod]
        public void TimeToLiveTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            byte actual;
            byte expected = 0;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "No TimeToLive Parameter");

            target = new SipUri("sip:bob.com;ttl=2;transport=tcp");
            expected = 2;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "No Transport Parameter");

            target = new SipUri("sip:bob.com;TTL=2");
            expected = 2;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "Standard Transport Parameter");

            target = new SipUri("sip:bob.com;TimeToLive=2");
            expected = 0;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "No Transport Parameter");
            }

        //[TestMethod]
        //[ExpectedException(typeof(SipUriFormatException))]
        //public void TimeToLiveTest5()
        //    {
        //    SipUri target = new SipUri("sip:bob.com;ttl=;transport=tcp");
        //    }
        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TimeToLiveTest1()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            byte actual;
            byte expected = 0;
            target = new SipUri("sip:bob.com;TTL=257");
            expected = 0;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "Too high TimeToLive Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TimeToLiveTest2()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            byte actual;
            byte expected = 0;
            target = new SipUri("sip:bob.com;TTL=-1");
            expected = 0;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "Too low TimeToLive Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TimeToLiveTest3()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            byte actual;
            byte expected = 0;
            target = new SipUri("sip:bob.com;TTL=ttl");
            expected = 0;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "Too low TimeToLive Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TimeToLiveTest4()
            {
            SipUri target = new SipUri("sip:bob.com;ttl;transport=tcp");
            }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = "sip:bob.com";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Basic");

            target = new SipUri("sip:bob.com:0");
            expected = "sip:bob.com:0";
            byte a = target.TimeToLive;
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Basic2");

            uri = new Uri(" sip:bob.com@bob.com");
            target = new SipUri(" sip:bob.com@bob.com");
            expected = "sip:bob.com@bob.com";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Basic3");
            }

        [TestMethod] 
        public void TransportTest2()
            {
            SipUri target = new SipUri("sip:bob.com;transport="); 
            string actual;
            string expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");
            }

        [TestMethod] 
        public void TransportTest1()
            {
            SipUri target = new SipUri("sip:bob.com;transport "); 
            string actual;
            string expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TransportTest3()
            {
            SipUri target = new SipUri("sip:bob.com;transport;lr");
            }


        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TransportTest4()
            {
            SipUri target = new SipUri("sip:bob.com;transport?transport=tcp");
            }


        [TestMethod]
        public void TransportTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");

            target = new SipUri("sip:bob.com");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");

            target = new SipUri("sip:user:transport=tcp@bob.com");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");

            target = new SipUri("sip:user;transport=tcp:transport=tcp@bob.com");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "No Transport Parameter");

            target = new SipUri("sip:user;transport=tcp:transport=tcp@bob.com;transport=udp");
            expected = "udp";
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "UDP Transport Parameter");

            target = new SipUri("sip:bob.com;transport=fff");
            expected = "fff";
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "fff Transport Parameter");

            }

        [TestMethod]
        [ExpectedException(typeof(Konnetic.Sip.SipUriFormatException))]
        public void TransportTest1a()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            target = new SipUri("sip:bob.com;transport=udp;transport=tcp");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "Duplicate Transport Parameter");
            }

        /// <summary>
        ///A test for UserName
        ///</summary>
        [TestMethod]
        public void UserNameTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "No user");

            uri = new Uri("sips:bob.com;user=566");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "No user");

            uri = new Uri(" sip::@bob.com;user=");
            target = new SipUri(uri);
            expected = string.Empty;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "Empty user");

            target = new SipUri(" sips:null@bob.com;user=null");
            expected = "null";
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "null user1");

            target = new SipUri(" sips:null:@bob.com;user=null");
            expected = "null";
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "null user2");

            target = new SipUri("sip:alice@atlanta.com");
            target = new SipUri("sip:alice:secretworld@atlanta.com");
            target = new SipUri("sips:alice@atlanta.com?subject=project%20x&priority=urgent");
            target = new SipUri("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            target = new SipUri("sips:1212@gateway.com");
            target = new SipUri("sip:atlanta.com;method=REGISTER?to=alice%40atlanta.com");
            target = new SipUri("sip:alice;day=tuesday@atlanta.com");

            uri = new Uri("sip:" + Common.USERUNRESERVED + "@bob.com");
            target = new SipUri(uri);
            expected = Common.USERUNRESERVED;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "USERUNRESERVED user");

            target = new SipUri("sip:" + Common.USERUNRESERVED + "@bob.com");
            expected = Common.USERUNRESERVED;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "USERUNRESERVED user2");
            }

        //[TestMethod]
        //[ExpectedException(typeof(UriFormatException))]
        //public void UserNameTest1()
        //{
        //    Uri uri = new Uri("sip:bob.com");
        //    SipUri target = new SipUri(uri);
        //    string actual;
        //    string expected = string.Empty;
        //    uri = new Uri(" sip:::@bob.com;user=");
        //    target = new SipUri(uri);
        //    target = new SipUri(" sip:::@bob.com;user=");
        //    expected = string.Empty;
        //    actual = target.UserName;
        //    Assert.AreEqual(expected, actual, "Invalid user");
        //}
        /// <summary>
        ///A test for UserParameter
        ///</summary>
        [TestMethod]
        public void UserParameterTest()
            {
            Uri uri = new Uri("sip:bob.com");
            SipUri target = new SipUri(uri);
            string actual;
            string expected = string.Empty;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "No user Parameter");

            uri = new Uri("sip:bob.com;user=566");
            target = new SipUri(uri);
            expected = "566";
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "Standard user");

            uri = new Uri("sip:bob.com;user=");
            target = new SipUri(uri);
            expected = string.Empty; 
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "Empty user");

            uri = new Uri("sip:bob.com;user=null");
            target = new SipUri(uri);
            expected = "null";
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "'null' user");

            target = new SipUri("sip:bob.com;user=" + Common.TOKEN);
            expected = Common.TOKEN;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "TOKEN user");

            target = new SipUri("sip:bob.com;lr;user=" + Common.TOKEN);
            expected = Common.TOKEN;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "TOKEN user2");
            }

        #endregion Methods

        #region Other

        ///// <summary>
        /////A test for CompareParameters
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("Konnetic.Sip.dll")]
        //public void CompareParametersTest()
        //    {
        //    SipParameter sp = null; // TODO: Initialize to an appropriate value
        //    SipParameter other = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = SipUri_Accessor.CompareParameters(sp, other);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //    }
        ///// <summary>
        /////A test for CompareHeaderFields
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("Konnetic.Sip.dll")]
        //public void CompareHeaderFieldsTest()
        //    {
        //    HeaderField hf = null; // TODO: Initialize to an appropriate value
        //    HeaderField other = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = SipUri_Accessor.CompareHeaderFields(hf, other);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //    }
        ///// <summary>
        /////A test for SipUri Constructor
        /////</summary>
        //[TestMethod()]
        //public void SipUriConstructorTest1()
        //    {
        //    string uri = string.Empty; // TODO: Initialize to an appropriate value
        //    SipUri target = new SipUri(uri);
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //    }
        ///// <summary>
        /////A test for SipUri Constructor
        /////</summary>
        //[TestMethod()]
        //public void SipUriConstructorTest()
        //    {
        //    Uri uri = null; // TODO: Initialize to an appropriate value
        //    SipUri target = new SipUri(uri);
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //    }
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