using System.Collections.Generic;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for SipMessageAdapter and is intended
    ///to contain all SipMessageAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SipMessageAdapter
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
        public void AddHeaderTest()
        {
            SipMessage_Accessor target = CreateSipMessage_Accessor();
            HeaderFieldBase field = new FromHeaderField("sip:Fred@bob.com");
            field.ToString();
            bool expected = false;
            bool actual;
            actual = target.TryAddHeader(field);
            Assert.AreEqual(expected, actual,"From Constructor");

            target.Headers.Clear();
            expected = true;

            actual = target.TryAddHeader(field);
            Assert.AreEqual(expected, actual,"New Header");
        }

        /// <summary>
        ///A test for Body
        ///</summary>
        [TestMethod]
        public void BodyTest()
        {
            SipMessage target = CreateSipMessage();
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("bob");
            byte[] actual;
            target.Body = expected;
            actual = target.Body;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Find
        ///</summary>
        [TestMethod]
        public void FindTest()
        {
            SipMessage target = CreateSipMessage();
            string fieldName = string.Empty;
            HeaderFieldBase expected = null;
            HeaderFieldBase actual;
            actual = target.GetHeader(fieldName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FindValue
        ///</summary>
        [TestMethod]
        public void FindValueTest()
        {
            SipMessage_Accessor target = CreateSipMessage_Accessor();
            string fieldName = "From";
            string expected = "<sip:Fred>";
            string actual;
            actual = target.GetHeaderValue(fieldName);
            Assert.IsTrue(actual.StartsWith(expected),"From Constructor");

            target.Headers.Update(fieldName, "sip:Bob");
            expected = "<sip:Bob>";

            actual = target.GetHeaderValue(fieldName);
            Assert.IsTrue(actual.StartsWith(expected),"Assignment");
        }

        /// <summary>
        ///A test for Header
        ///</summary>
        [TestMethod]
        public void HeaderTest()
        {
            SipMessage target = CreateSipMessage();
            HeaderFieldCollection expected = new HeaderFieldCollection();
            expected.Add(new FromHeaderField());
            HeaderFieldCollection actual;
            target.Headers = expected;
            actual = target.Headers;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for UpdateHeader
        /// </summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void UpdateHeaderTest()
        {
            SipMessage_Accessor target = CreateSipMessage_Accessor();

            string name = "NoName";
            string newValue = "sip:Fred"; 
            target.Headers.Update(name, newValue);
            HeaderFieldBase s = target.Headers[name];
            Assert.IsTrue(s==null, "Try and update non-existant field.");

            name = "From";
            target.Headers.Update(name, newValue);
            s = target.Headers[name];
            Assert.IsTrue(target.Headers[name].GetStringValue() == "<"+newValue+">", "Try and update non-existant field.");

            HeaderFieldBase f = target.GetHeader(name);
            newValue = "<sip:Fred>";
            Assert.AreEqual(f.FieldName, name);
            Assert.IsTrue(f.GetStringValue().StartsWith(newValue), "Make sure From field updated");
        }

        internal virtual SipMessage CreateSipMessage()
        {
            SipMessage target = new Konnetic.Sip.Messages.Invite();
            return target;
        }

        internal virtual SipMessage_Accessor CreateSipMessage_Accessor()
        {
            Invite invite = new Invite("sip:Bob", "sip:Fred");
            PrivateObject param0 = new PrivateObject(invite);
            SipMessage_Accessor target = new SipMessage_Accessor(param0);
            return target;
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