using System;

using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
    {
    /// <summary>
    ///This is a test class for SipUriBuilderAdapter and is intended
    ///to contain all SipUriBuilderAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SipUriBuilderAdapter
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
        /// A test for Headers
        /// </summary>
        [TestMethod]
        public void HeadersTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=sip:alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "<sip:alice1>";
            HeaderFieldCollection actual = new HeaderFieldCollection();
            System.Collections.ObjectModel.ReadOnlyCollection<HeaderFieldBase> readOnlyActual = new System.Collections.ObjectModel.ReadOnlyCollection<HeaderFieldBase>(actual);
            readOnlyActual = target.Headers;
            Assert.AreEqual("To", readOnlyActual[0].FieldName, "Constructor");
            Assert.AreEqual(expected, readOnlyActual[0].GetStringValue(), "Constructor1");

            uri = new SipUri("SIPS:ALiCE:@Bob.com:1564;maddr=239.255.255.1");
            target = new SipUriBuilder(uri);
            expected = "Bob.com";
            readOnlyActual = target.Headers;
            Assert.IsTrue(readOnlyActual.Count == 0, "Constructor3");

            uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=sip:alice1&from=sip:aaa");
            target = new SipUriBuilder(uri);
            readOnlyActual = target.Headers;
            Assert.AreEqual("To", readOnlyActual[0].FieldName, "Constructor4");
            Assert.AreEqual("<sip:alice1>", readOnlyActual[0].GetStringValue(), "Constructor5");
            Assert.AreEqual("From", readOnlyActual[1].FieldName, "Constructor6");
            Assert.IsTrue(readOnlyActual[1].GetStringValue().StartsWith("<sip:aaa"), "Constructor7");

            uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=sip:alice1");
            target = new SipUriBuilder(uri);
            FromHeaderField fhf = new FromHeaderField("sip:fred");
            target.AddHeader(fhf);
            readOnlyActual = target.Headers;
            Assert.IsTrue(readOnlyActual.Count == 2, "Constructor11");
            Assert.AreEqual("From", readOnlyActual[1].FieldName, "Constructor9");
            Assert.IsTrue(readOnlyActual[1].GetStringValue().StartsWith("<sip:fred>"), "Constructor10");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void HeadersTest1()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=alice1&to=aaa");

            SipUriBuilder target = new SipUriBuilder(uri);
            }

        /// <summary>
        ///A test for Host
        ///</summary>
        [TestMethod]
        public void HostTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "bob.com";
            string actual;
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:ALiCE:@Bob.com:1564;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "Bob.com";
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor1");

            uri = new SipUri("sip::@123.156.128.125:1564;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "123.156.128.125";
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor2");

            uri = new SipUri("SIP:bob.com;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "bob.com";
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor3");

            uri = new SipUri("SiPS:bob.com?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "bob.com";
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor4");

            uri = new SipUri("SIPS:bob.com ");
            target = new SipUriBuilder(uri);
            expected = "bob.com";
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Constructor5");

            expected = String.Empty;
            target.Host = expected;
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = "Pass";
            target.Host = expected;
            actual = target.Host;
            Assert.AreEqual(expected, actual, "Assignment");

            expected = Common.ALPHANUM + ":%.-[]";
            target.Host = expected;
            actual = target.Host;
            Assert.AreEqual("[" + expected + "]", actual, "TOKEN");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void HostTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual = string.Empty;

            expected = string.Empty;
            target.Host = null;
            actual = target.Host;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        /// <summary>
        ///A test for Lr
        ///</summary>
        [TestMethod]
        public void LrTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;lr;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            bool expected = true;
            bool actual;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = false;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "Constructor1");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;maddr=239.255.255.1;lr");
            target = new SipUriBuilder(uri);
            expected = true;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "Constructor2");

            expected = true;
            target.LooseRouter = expected;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "True Assignment");

            expected = false;
            target.LooseRouter = expected;
            actual = target.LooseRouter;
            Assert.AreEqual(expected, actual, "false Assignment");
            }

        [TestMethod]
        public void MaddrParameterTest()
            {
            for(int i = 0; i < Common.HOSTRESERVED.Length; i++)
                {
                string val = new string(Common.HOSTRESERVED[i], 1);
                Assert.IsTrue(MaddrThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void MaddrTest2()
            {
            SipUri uri = new SipUri("sIp:ALICE:@bob.com:1564;maddr?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            }
        [TestMethod] 
        public void MaddrTest1()
            {
            SipUri uri = new SipUri("sip:ALICE:@bob.com:1564;maddr=?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "";
            string actual;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual);
            }


        /// <summary>
        ///A test for Maddr
        ///</summary>
        [TestMethod]
        public void MaddrTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "239.255.255.1";
            string actual;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "239.255.255.1";
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Constructor1");             

            uri = new SipUri(" SIPS:bob.com;maddr=239.255.255.1?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "239.255.255.1";
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Constructor4");

            uri = new SipUri("sips:bob.com;maddr=bob.com");
            target = new SipUriBuilder(uri);
            expected = "bob.com";
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Constructor5");

            uri = new SipUri(" Sips:bob.com;maddr=bob.com:1562");
            target = new SipUriBuilder(uri);
            expected = "bob.com:1562";
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Constructor6");

            expected = String.Empty;
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = "Pass";
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "Assignment");

            expected = Common.HOSTUNRESERVED;
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "TOKEN");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void MaddrTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual = string.Empty;
            expected = null;
            target.MulticastAddress = expected;
            actual = target.MulticastAddress;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        [TestMethod]
        public void MethodParameterTest()
            {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(MethodThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void MethodTest2()
            {
            SipUri uri = new SipUri("SIPS:ALICE:@bob.com:1564;method;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            }

        [TestMethod] 
        public void MethodTest1()
            {
            SipUri uri = new SipUri("SIPS:ALICE:@bob.com:1564;method=;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "";
            string actual;
            actual = target.Method;
            Assert.AreEqual(expected, actual);
            }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        public void MethodTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "INVITE";
            string actual;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;method=INViTE1;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            target = new SipUriBuilder(uri);
            expected = "INVITE1";
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Constructor1");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            target = new SipUriBuilder(uri);
            expected = string.Empty;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Constructor2");

            expected = String.Empty;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = "Pass";
            target.Method = expected;
            actual = target.Method;
            expected = "PASS";
            Assert.AreEqual(expected, actual, "Assignment");

            expected = Common.TOKEN;
            target.Method = expected;
            actual = target.Method;
            expected = Common.TOKEN.ToUpperInvariant();
            Assert.AreEqual(expected, actual, "TOKEN");
            }

        [TestMethod]
        public void MethodTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual = string.Empty;

            expected = null;
            target.Method = expected;
            actual = target.Method;
            expected = string.Empty;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        [TestMethod]
        public void PasswordParameterTest()
            {
            for(int i = 0; i < Common.PASSWORDRESERVED.Length; i++)
                {
                string val = new string(Common.PASSWORDRESERVED[i], 1);
                Assert.IsTrue(PasswordThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        /// <summary>
        ///A test for Password
        ///</summary>
        [TestMethod]
        public void PasswordTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:PASSWORD@bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "PASSWORD";
            string actual;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:ALICE:@bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor1");

            uri = new SipUri("sip:ALICE@bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor2");

            uri = new SipUri("Sip:ALICE@bob.com");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor3");

            uri = new SipUri("sip:ALICE:@bob.com");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor4");

            uri = new SipUri("SiPS:ALICE:1232");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor5");

            uri = new SipUri("SIPS:ALICE@bob.com:1232");
            target = new SipUriBuilder(uri);
            expected = String.Empty;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor6");

            uri = new SipUri("SIPS:ALICE:1232@bob.com ");
            target = new SipUriBuilder(uri);
            expected = "1232";
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Constructor7");

            expected = String.Empty;
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = "Pass";
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "Assignment");

            expected = Common.ALPHANUM + Common.MARK;
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "TOKEN");

            expected = "&=+$,";
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "PASSWORDUNRESERVED");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void PasswordTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual = string.Empty;

            expected = null;
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        /// <summary>
        ///A test for Port
        ///</summary>
        [TestMethod]
        public void PortTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:PASSWORD@bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            int? expected = 1564;
            int? actual;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor");

            uri = new SipUri("SIPS:bob.com:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            target = new SipUriBuilder(uri);
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor2");

            uri = new SipUri("SIPS:bob.com:1564");
            target = new SipUriBuilder(uri);
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor3");
            /*
            uri = new SipUri("SIP:[2001:0db8:85a3:0000:0000:8a2e:0370:7334]:1564");
            target = new SipUriBuilder(uri);
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor4");

            uri = new SipUri("SIP:[2001:db8:85a3:0:0:8a2e:370:7334]:15644");
            target = new SipUriBuilder(uri);
            expected = 15644;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor5");

            uri = new SipUri("SIP:[2001:db8:85a3::8a2e:370:7334]:156");
            target = new SipUriBuilder(uri);
            expected = 156;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor5");

            uri = new SipUri("SIP:[2001-db8-85a3-8d3-1319-8a2e-370-7348.ipv6-literal.net]:156");
            target = new SipUriBuilder(uri);
            expected = 156;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor6");

            uri = new SipUri("SIP:[2001:db8:85a3::8a2e:370:7334]");
            target = new SipUriBuilder(uri);
            expected = 0;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor7");
            */
            uri = new SipUri("SIP:server1");
            target = new SipUriBuilder(uri);
            expected = 0;
            target.Port = expected;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor8");

            uri = new SipUri("SIP:server1:0123");
            target = new SipUriBuilder(uri);
            expected = 0;
            target.Port = expected;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Constructor9");

            expected = 0;
            target.Port = expected;
            actual = target.Port;
            Assert.AreEqual(expected, actual, "Zero Assignment");


            }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void PortTest3()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            target.Port = 99999;
            }
        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void PortTest2()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            target.Port = -1;
            }
        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void PortTest1()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            target.Port = 655536;
            }
        /// <summary>
        ///A test for Scheme
        ///</summary>
        [TestMethod]
        public void SchemeTest()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            SipScheme expected = SipScheme.Sips;
            SipScheme actual;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Constructor");

            target = new SipUriBuilder("sIp:bob.com");
            expected = SipScheme.Sip;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Constructor2 with case changed");

            target = new SipUriBuilder();
            expected = SipScheme.Sip;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Constructor3");

            expected = SipScheme.Sips;
            target.Scheme = SipScheme.Sips;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "assignment");
            }

        [TestMethod]
        [ExpectedException(typeof(Konnetic.Sip.SipUriFormatException))]
        public void SchemeTest3()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            SipScheme expected = SipScheme.Sips;
            SipScheme actual;
            target = new SipUriBuilder("sI1p:");
            expected = SipScheme.Unknown;
            actual = target.Scheme;
            Assert.AreEqual(expected, actual, "Unknown");
            }

        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TTLTest4()
            {
            SipUriBuilder target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl;maddr=239.255.255.1;transport=tcp?to=alice1");
            }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
            {
            SipUri uri = new SipUri("SIPS:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=sip:alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "sips:ALICE:Password@bob.com:1564;ttl=15;maddr=239.255.255.1;method=INVITE;transport=tcp?to=%3Csip:alice1%3E";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            }

        [TestMethod]
        public void TransportParameterTest()
            {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(TransportThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        /// <summary>
        ///A test for Transport
        ///</summary>
        [TestMethod]
        public void TransportTest()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "tcp";
            string actual;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "From SipUri constructor (transport=tcp)");

            target = new SipUriBuilder("sip:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=t@p?to=alice1");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "Reserved letter in constructor");

            target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport?to=alice1");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "no value in constructor");

            target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=?to=alice1");
            expected = string.Empty;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "no value in constructor2");

            expected = String.Empty;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = "UdP";
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "Assignment");

            expected = Common.TOKEN;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "TOKEN");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void TransportTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual = string.Empty;

            expected = null;
            target.Transport = expected;
            actual = target.Transport;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        /// <summary>
        ///A test for Ttl
        ///</summary>
        [TestMethod]
        public void TtlTest()
            {
            SipUri uri = new SipUri("sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            byte? expected = 15;
            byte? actual;
            target.TimeToLive = expected;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "From SipUri constructor (ttl=15)");

            target = new SipUriBuilder("sip:alice:1256password@123.456.789.464:1564;ttl=0;maddr=239.255.255.1;transport=tcp?to=alice1");
            actual = target.TimeToLive;
            expected = 0;
            Assert.AreEqual(expected, actual, "From string constructor (ttl=0)");

            target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl=255;maddr=239.255.255.1;transport=tcp?to=alice1");
            actual = target.TimeToLive;
            expected = 255;
            Assert.AreEqual(expected, actual, "ttl at 255");

            target = new SipUriBuilder();
            expected = 255;
            target.TimeToLive = 255;
            actual = target.TimeToLive;
            Assert.AreEqual(expected, actual, "ttl assign 255");
            }

        /// <summary>
        ///A test for Ttl
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(SipUriFormatException))]
        public void TtlTest1()
            {
            SipUriBuilder target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl=ALPHA;maddr=239.255.255.1;transport=tcp?to=alice1");
            }

        //[TestMethod]
        //[ExpectedException(typeof(SipUriFormatException))]
        //public void TTLTest5()
        //    {
        //    SipUriBuilder target = new SipUriBuilder("sips:alice:1256password@123.456.789.464:1564;ttl=;maddr=239.255.255.1;transport=tcp?to=alice1");
        //    }
        [TestMethod]
        public void UserNameParameterTest()
            {
            for(int i = 0; i < Common.USERRESERVED.Length; i++)
                {
                string val = new string(Common.USERRESERVED[i], 1);
                Assert.IsTrue(UserNameThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        /// <summary>
        ///A test for UserName
        ///</summary>
        [TestMethod]
        public void UserNameTest()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = "alice";
            string actual;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual);

            target = new SipUriBuilder(@"sip::1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            expected = String.Empty;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "Null From Constructor");

            target = new SipUriBuilder();
            expected = "Bob";
            target.UserName = "Bob";
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "Assignment");

            expected = "sip:Bob@localhost";
            Assert.AreEqual(expected, target.ToString());

            target = new SipUriBuilder(@"sips:@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            expected = String.Empty;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "Empty Constructor");

            expected = String.Empty;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "Empty Assignment");

            expected = Common.ALPHANUM;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "ALPHANUM");

            expected = Common.USERUNRESERVED;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "USERUNRESERVED");
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void UserNameTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice1");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = null;
            string actual = string.Empty;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual, "null Assignment");
            }

        /// <summary>
        ///A test for UserParameter
        ///</summary>
        [TestMethod]
        public void UserParameterTest()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual;
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "No User-Paramater");

            target = new SipUriBuilder(@"sip:alice;1256password@123.456.789.464:1564;user=bill;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            actual = target.UserParameter;
            expected = "bill";
            Assert.AreEqual(expected, actual, "Expect bill");

            target = new SipUriBuilder(@"sips:alice;1256password@123.456.789.464:1564;user;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            actual = target.UserParameter;
            expected = string.Empty;
            Assert.AreEqual(expected, actual, "Expect no value");

            target = new SipUriBuilder();
            expected = "Franky";
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "Expect Franky");

            //Assert.AreEqual("sip:localhost;user=Franky", target.ToString(), "Expect Default Output");

            target.Host = "konnetic.com";
            Assert.AreEqual("sip:konnetic.com;user=Franky", target.ToString(), "Expect Sip Address");

            expected = String.Empty;
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "Empty");

            expected = Common.ALPHANUM;
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "ALPHANUM");

            expected = Common.TOKEN;
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "TOKEN");
            }

        [TestMethod]
        public void UserParameterTest2()
            {
            for(int i = 0; i < Common.USERRESERVED.Length; i++)
                {
                string val = new string(Common.USERRESERVED[i], 1);
                Assert.IsTrue(UserParameterThrowsError(val), "Exception Not thrown on: " + val);
                }
            }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void UserParameterTest3()
            {
            SipUri uri = new SipUri(@"sips:alice:1256password@123.456.789.464:1564;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            SipUriBuilder target = new SipUriBuilder(uri);
            string expected = string.Empty;
            string actual;
            expected = null;
            target.UserParameter = expected;
            actual = target.UserParameter;
            Assert.AreEqual(expected, actual, "null");
            }

        /// <summary>
        ///A test for UserParameters
        ///</summary>
        [TestMethod]
        public void UserParametersTest()
            {
            SipUri uri = new SipUri(@"sips:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            SipUriBuilder target = new SipUriBuilder(uri);

            SipUriParameterCollection expected = new SipUriParameterCollection();
            SipUriParameterCollection actual = new SipUriParameterCollection();
            System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> actualReadOnly = new System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>(actual);
            actualReadOnly = target.Parameters;
            Assert.AreEqual(expected.Count, actual.Count, "No User parameters defined");

            uri = new SipUri(@"sip:alice:password@chicago.com;Howdee=DooDee;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice@bob.com");
            target = new SipUriBuilder(uri);
            actualReadOnly = target.Parameters;
            Assert.AreEqual(4, actualReadOnly.Count, "Three user parameters define");
            Assert.AreEqual("howdee", actualReadOnly[0].Name, "Correct User parameter name");
            Assert.AreEqual("DooDee", actualReadOnly[0].Value, "Correct User parameter value");

            target = new SipUriBuilder();
            target.AddParameter(new SipParameter("Howdee1", "DooDee1"));
            actualReadOnly = target.Parameters;
            Assert.AreEqual(1, actualReadOnly.Count, "One user parameter define1");
            Assert.AreEqual("howdee1", actualReadOnly[0].Name, "Correct User parameter name1");
            Assert.AreEqual("DooDee1", actualReadOnly[0].Value, "Correct User parameter value1");
            }

        private bool MaddrThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.MulticastAddress = val;
                }
            catch(SipUriFormatException)
                {
                return true;
                }
            return false;
            }

        private bool MethodThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.Method = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
            }

        private bool PasswordThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.Password = val;
                }
            catch(SipUriFormatException)
                {
                return true;
                }
            return false;
            }

        private bool TransportThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.Transport = val;
                }
            catch(SipUriFormatException)
                {
                return true;
                }
            return false;
            }

        private bool UserNameThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.UserName = val;
                }
            catch(SipUriFormatException)
                {
                return true;
                }
            return false;
            }

        private bool UserParameterThrowsError(string val)
            {
            try
                {
                SipUriBuilder target = new SipUriBuilder();
                target.UserParameter = val;
                }
            catch(SipUriFormatException)
                {
                return true;
                }
            return false;
            }

        #endregion Methods

        #region Other

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