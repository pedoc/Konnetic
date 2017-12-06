using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RouteHeaderFieldAdapter and is intended
    ///to contain all RouteHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RouteHeaderFieldAdapter
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
            RouteHeaderField target = new RouteHeaderField();
            HeaderFieldBase expected = new RouteHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.DisplayName = "Fred";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(((RouteHeaderField)actual).DisplayName == "Fred");

            ((RouteHeaderField)expected).DisplayName = "Fred";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddParameter("bob", "Fanny");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(((RouteHeaderField)actual).GenericParameters[0].Name == "bob");

            ((RouteHeaderField)expected).AddParameter("bob", "Fanny");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(((RouteHeaderField)actual).GenericParameters[0].Name == "bob");

            target.Uri = new SipUri("sip:bob@fanny.com:5080");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(((RouteHeaderField)actual).Uri == new SipUri("sip:bob@fanny.com:5080"));

            ((RouteHeaderField)expected).Uri = new SipUri("sip:bob@fanny.com:5080");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(((RouteHeaderField)actual).Uri == new SipUri("sip:bob@fanny.com:5080"));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            RouteHeaderField target = new RouteHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new RouteHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new RouteHeaderField("sip:bob@gmail.com");
            expected = false;
            actual = target.Equals(other);
            string strexpected = "<sip:bob@gmail.com>";
            string stractual = other.GetStringValue();
            Assert.AreEqual(strexpected,stractual);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:bob1@gmail.com");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:bob@gmail.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.AddParameter("Param", "value");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((RouteHeaderField)other).AddParameter("Param", "value");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DisplayName="Gary";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((RouteHeaderField)other).DisplayName = "Bob";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((RouteHeaderField)other).DisplayName = "Gary";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest1()
        //    {
        //    RouteHeaderField target = new RouteHeaderField();
        //    string value = "Route1     :  \r\n  ";
        //    target.Parse(value);
        //    }
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            RouteHeaderField target = new RouteHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Route     :  \r\n  ";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tRoute    \t :\t  \r  ";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Route     :  \n  ";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\r\n Route\r\n   \r\n \r\n    :\r\n   \r\n \"Bob\"\r\n<sips:bob@google.com:5062> \t";
            target.Parse(value);
            expected = "\"Bob\" <sips:bob@google.com:5062>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tRoute     :\t  \r\n \"Bob\"\r\n<sips:bob@google.com:5062;lr>;param=1";
            target.Parse(value);
            expected = "\"Bob\" <sips:bob@google.com:5062;lr>;param=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new RouteHeaderField();
            value = "  Route     :  \r\n \r\n<sips:bob@google.com:5062;lr>;param=1";
            target.Parse(value);
            expected = "<sips:bob@google.com:5062;lr>;param=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new RouteHeaderField();
            value = "Route  \r\n   :  \"Bob\"<sips:bob@google.com:5062;username=james>;param=1";
            target.Parse(value);
            expected = "\"Bob\" <sips:bob@google.com:5062;username=james>;param=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\"Bob\" \r\n<sips:bob@google.com:5062>;param=1";
            target.Parse(value);
            expected = "\"Bob\" <sips:bob@google.com:5062>;param=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RouteHeaderFieldConstructorTest()
        {
            RouteHeaderField target = new RouteHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Route");
            Assert.IsTrue(target.CompactName == "Route");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for RouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RouteHeaderFieldConstructorTest1()
        {
            SipUri uri = new SipUri("sip:1_unusual.URI(to-be!sure)&isn't+it$/crazy?,/;;*@example.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice&from=sip:bob");
            RouteHeaderField target = new RouteHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Route");
            Assert.IsTrue(target.CompactName == "Route");
            Assert.IsTrue(target.GetStringValue() == "<sip:1_unusual.URI(to-be!sure)&isn't+it$/crazy?,/;;*@example.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice&from=sip:bob>");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for RouteHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RouteHeaderFieldConstructorTest2()
        {
            string uri = "sip:1_unusual.URI(to-be!sure)&isn't+it$/crazy?,/;;*:&it+has=1,weird!*pas$wo~d_too.(doesn't-it)@example.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice&from=sip:bob";
            RouteHeaderField target = new RouteHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Route");
            Assert.IsTrue(target.CompactName == "Route");
            Assert.IsTrue(target.GetStringValue() == "<sip:1_unusual.URI(to-be!sure)&isn't+it$/crazy?,/;;*:&it+has=1,weird!*pas$wo~d_too.(doesn't-it)@example.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice&from=sip:bob>");
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