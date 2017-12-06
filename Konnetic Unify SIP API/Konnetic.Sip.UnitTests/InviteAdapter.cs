using System;
using System.Collections.Generic;
using System.IO;

using Konnetic.Sip;
using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for InviteAdapter and is intended
    ///to contain all InviteAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class InviteAdapter
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
        ///A test for AddHeader
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void AddHeaderTest1()
        {
            //PrivateObject param0 = new PrivateObject(new FromHeaderField("bob"));
            //SipMessage_Accessor target = new SipMessage_Accessor(param0);
            //HeaderField expected = new FromHeaderField("bob");
            //bool b = target.AddHeader(expected);
            //HeaderField actual = target.Header[0];
            //Assert.AreEqual(actual, expected);
        }

        /// <summary>
        ///A test for Body
        ///</summary>
        [TestMethod]
        public void BodyTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            SipMessage target = new Invite(to, from);
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("Bob");
            byte[] actual;
            target.Body = expected;
            actual = target.Body;
            Assert.AreEqual(expected, actual,"Body not being set correctly");
        }

        /// <summary>
        ///A test for CallID
        ///</summary>
        [TestMethod]
        public void CallIDTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            Invite target = new Invite(to, from);
            string expected = "123";
            string actual;
            target.CallId = expected;
            actual = target.CallId.GetStringValue();
            Assert.AreEqual(expected, actual, "CallID not being set correctly");
        }

        /// <summary>
        ///A test for Find
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void FindTest()
        {
            Invite_Accessor i = new Invite_Accessor(new SipUri("sip:Bob"));
            string name = string.Empty;

            HeaderFieldBase actual;
            actual = i.GetHeader(name);
            Assert.AreEqual(null, actual);

            actual = i.GetHeader("To");

            Assert.AreEqual("To", actual.FieldName);
        }

        /// <summary>
        ///A test for FindValue
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void FindValueTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            Invite_Accessor target = new Invite_Accessor(to, from);

            string expected = "<sip:Bob@bob.com>";
            string actual;
            actual = target.GetHeaderValue("To");
            Assert.AreNotEqual(null, actual, "The Find method did not find an instance.");
            Assert.AreEqual(expected, actual, "The Find method returned an incorrect value");
        }

        /// <summary>
        ///A test for From
        ///</summary>
        [TestMethod]
        public void FromTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            Invite target = new Invite(to, from);
            FromHeaderField expected = "From: <sip:Fred@bob.com>";
            FromHeaderField actual;
            target.From = expected;
            actual = target.From;
            Assert.AreEqual(expected, actual, "Invite does not initialise the 'From' Uri or assigned and retrieve its value correctly");
        }

        /// <summary>
        ///A test for Header
        ///</summary>
        [TestMethod]
        public void HeaderTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            SipMessage target = new Invite(to, from);

            Assert.AreNotEqual(target.Headers, null, "Constructor not setting headers to List<>");
            Assert.AreNotEqual(target.Headers.Count, 0, "Constructor setting up default headers!");

            ToHeaderField hf = new ToHeaderField(); //Set to some header field.

            HeaderFieldCollection expected = new HeaderFieldCollection();
            expected.Add(hf);
            HeaderFieldCollection actual;
            target.Headers = expected;
            actual = target.Headers;
            Assert.AreNotEqual(actual[0], null, "HeaderField not being added");
            Assert.AreEqual(actual[0].GetType(), hf.GetType(), "HeaderField types are not the same");
        }

        /// <summary>
        ///A test for Invite Constructor
        ///</summary>
        [TestMethod]
        public void InviteConstructorTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sip:Fred@bob.com");
            Invite target = new Invite(to, from);
            Assert.AreEqual(target.To, "To: <sip:Bob@bob.com>", "Constructor not assigning 'To' correctly");
            Assert.AreEqual(target.From.Uri.ToString(), "sip:Fred@bob.com", "Constructor not assigning 'From' correctly");
        }

        /// <summary>
        ///A test for Invite Constructor
        ///</summary>
        [TestMethod]
        public void InviteConstructorTest1()
        {
            string to = "sip:Bob@bob.com";
            string from = "sip:Fred@bob.com";
            Invite target = new Invite(to, from);
            Assert.AreEqual(target.To, "To: <sip:Bob@bob.com>", "Constructor not assigning 'To' or 'From' correctly");
            Assert.AreEqual(target.From.Uri.ToString(), "sip:Fred@bob.com", "Constructor not assigning 'To' or 'From' correctly");
        }

        /// <summary>
        ///A test for Invite Constructor
        ///</summary>
        [TestMethod]
        public void InviteConstructorTest2()
        {
            Invite target = new Invite(new SipUri("sip:Bob@bob.com"));
            Assert.AreEqual("To: <sip:Bob@bob.com>", target.To, "Constructor not assigning 'To' correctly");
        }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void MethodTest()
        {
            Invite_Accessor target = new Invite_Accessor();
            SipMethod expected = SipMethod.Invite;
            SipMethod actual;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected = SipMethod.Invite;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for To
        ///</summary>
        [TestMethod]
        public void ToTest()
        {
            SipUri to = new SipUri("sip:Bob@bob.com");
            SipUri from = new SipUri("sips:Fred@bob.com");
            Invite target = new Invite(to, from);
            ToHeaderField expected = "To: <sip:Bob@bob.com>";
            ToHeaderField actual;
            target.To = expected;
            actual = target.To;
            Assert.AreEqual(expected, actual, "Invite does not initialise the 'To' Uri or assigned and retrieve its value correctly");
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