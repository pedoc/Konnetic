using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ReplyToHeaderFieldAdapter and is intended
    ///to contain all ReplyToHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ReplyToHeaderFieldAdapter
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
            ReplyToHeaderField target = new ReplyToHeaderField();
            HeaderFieldBase expected = new ReplyToHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            expected = new ReplyToHeaderField("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target = new ReplyToHeaderField("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddParameter("bob", "Fred");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ReplyToHeaderField)expected).AddParameter("bob", "Fred");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ReplyToHeaderField target = new ReplyToHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ReplyToHeaderField("sip:alice;day=tuesday@atlanta.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new ReplyToHeaderField("sip:alice;day=tuesday@atlanta.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DisplayName="1232";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ReplyToHeaderField target = new ReplyToHeaderField();
            string value = string.Empty;
            target.Parse(value);

            target = new ReplyToHeaderField();
            value = "sip:bob@fanny.com";
            target.Parse(value);

            string expected = "<sip:bob@fanny.com>";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new ReplyToHeaderField();
            value = "\t\"displayname\"\t<sip:bob@fanny.com>";
            target.Parse(value);

            expected = "\"displayname\" <sip:bob@fanny.com>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new ReplyToHeaderField();
            value = "\tReply-To\t: \"displayname\" <sip:bob@fanny.com>";
            target.Parse(value);

            expected = "\"displayname\" <sip:bob@fanny.com>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ReplyToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ReplyToHeaderFieldConstructorTest()
        {
            ReplyToHeaderField target = new ReplyToHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Reply-To");
            Assert.IsTrue(target.CompactName == "Reply-To");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ReplyToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ReplyToHeaderFieldConstructorTest1()
        {
            SipUri uri = new SipUri();
            ReplyToHeaderField target = new ReplyToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Reply-To");
            Assert.IsTrue(target.CompactName == "Reply-To");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);

            uri = new SipUri("sips:1212@gateway.com");
            target = new ReplyToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Reply-To");
            Assert.IsTrue(target.CompactName == "Reply-To");
            Assert.IsTrue(target.GetStringValue() == "<sips:1212@gateway.com>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for ReplyToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ReplyToHeaderFieldConstructorTest2()
        {
            string uri = "sip:localhost";
            ReplyToHeaderField target = new ReplyToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Reply-To");
            Assert.IsTrue(target.CompactName == "Reply-To");
            Assert.IsTrue(target.GetStringValue() == "<sip:localhost>");
            Assert.IsTrue(target.HasParameters == false);

            uri = "sip:atlanta.com;method=REGISTER?to=alice%40atlanta.com";
            target = new ReplyToHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Reply-To");
            Assert.IsTrue(target.CompactName == "Reply-To");
            Assert.IsTrue(target.GetStringValue() == "<sip:atlanta.com;method=REGISTER?to=alice@atlanta.com>");
            Assert.IsTrue(target.HasParameters == false);
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