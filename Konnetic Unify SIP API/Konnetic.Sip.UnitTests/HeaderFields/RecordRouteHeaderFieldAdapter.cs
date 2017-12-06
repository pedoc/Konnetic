using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RecordRouteHeaderFieldAdapter and is intended
    ///to contain all RecordRouteHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RecordRouteHeaderFieldAdapter
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
            RecordRouteHeaderField target = new RecordRouteHeaderField();
            HeaderFieldBase expected = new RecordRouteHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            expected = new RecordRouteHeaderField("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target = new RecordRouteHeaderField("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            RecordRouteHeaderField target = new RecordRouteHeaderField(new SipUri("sips:alice@atlanta.com?subject=project%20x&priority=urgent"));
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new RecordRouteHeaderField(new SipUri("sips:alice@atlanta.com?subject=project%20x&priority=urgent"));
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((RecordRouteHeaderField)other).DisplayName = "bob";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DisplayName = "bob";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((RecordRouteHeaderField)other).AddParameter("bob", "gary");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.AddParameter("bob", "gary");
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
            RecordRouteHeaderField target = new RecordRouteHeaderField();
            string value = string.Empty;
            target.Parse(value);

            target = new RecordRouteHeaderField();
            value = "\tRecord-Route \t: \t\"DisplayName\" <sips:bbbb@fannly.com> ";
            target.Parse(value);
            string expected = "\"DisplayName\" <sips:bbbb@fannly.com>";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new RecordRouteHeaderField();
            value = "Record-Route : DisplayName\r\n \t<sips:bbbb@fannly.com> \t ";
            target.Parse(value);
            expected = "\"DisplayName\" <sips:bbbb@fannly.com>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new RecordRouteHeaderField();
            value = "Record-Route : <sips:bbbb@fannly.com> ";
            target.Parse(value);
            expected = "<sips:bbbb@fannly.com>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RecordRouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RecordRouteHeaderFieldConstructorTest()
        {
            RecordRouteHeaderField target = new RecordRouteHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Record-Route");
            Assert.IsTrue(target.CompactName == "Record-Route");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for RecordRouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RecordRouteHeaderFieldConstructorTest1()
        {
            SipUri uri = new SipUri("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            RecordRouteHeaderField target = new RecordRouteHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Record-Route");
            Assert.IsTrue(target.CompactName == "Record-Route");
            Assert.IsTrue(target.GetStringValue() == "<sip:+1-212-555-1212:1234@gateway.com;user=phone>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for RecordRouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RecordRouteHeaderFieldConstructorTest2()
        {
            string uri = "sip:localhost";
            RecordRouteHeaderField target = new RecordRouteHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Record-Route");
            Assert.IsTrue(target.CompactName == "Record-Route");
            Assert.IsTrue(target.GetStringValue() == "<sip:localhost>");
            Assert.IsTrue(target.HasParameters == false);

            uri = "sip:+1-212-555-1212:1234@gateway.com;user=phone";
            target = new RecordRouteHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Record-Route");
            Assert.IsTrue(target.CompactName == "Record-Route");
            Assert.IsTrue(target.GetStringValue() == "<sip:+1-212-555-1212:1234@gateway.com;user=phone>");
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