using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ErrorHeaderFieldAdapter and is intended
    ///to contain all ErrorHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ErrorHeaderFieldAdapter
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
            ErrorInfoHeaderField target = new ErrorInfoHeaderField();
            HeaderFieldBase expected = new ErrorInfoHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sips:sips:sips@5060");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ErrorInfoHeaderField)expected).Uri = new SipUri("sips:sips:sips@5060");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddParameter("Name", "123");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ErrorInfoHeaderField)expected).AddParameter("name", "123");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<ErrorInfoHeaderField> hg = new HeaderFieldGroup<ErrorInfoHeaderField>();
            hg.Add(target);
            actual = hg.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ErrorInfoHeaderField target = new ErrorInfoHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ErrorInfoHeaderField();
            ((ErrorInfoHeaderField)other).Uri= new SipUri("sip:bob@123.44.2.222:554");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:bob@123.44.2.222:554");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:bob@123.44.2.222:5545");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<ErrorInfoHeaderField> hg = new HeaderFieldGroup<ErrorInfoHeaderField>();
            hg.Add(target);
            expected = false;
            actual = hg.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = true;
            hg[0].Uri = new SipUri("sip:bob@123.44.2.222:554");
            actual = other.Equals(hg);
            Assert.AreEqual(expected, actual);

            actual = hg.Equals(hg);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ErrorHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ErrorHeaderFieldConstructorTest()
        {
            SipUri uri = new SipUri("sip:bob@fanny.com");
            ErrorInfoHeaderField target = new ErrorInfoHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Error-Info");
            Assert.IsTrue(target.CompactName == "Error-Info");
            Assert.IsTrue(target.GetStringValue() == "<sip:bob@fanny.com>");
        }

        /// <summary>
        ///A test for ErrorHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ErrorHeaderFieldConstructorTest1()
        {
            string uri = "sips:sip:56142";
            ErrorInfoHeaderField target = new ErrorInfoHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Error-Info");
            Assert.IsTrue(target.CompactName == "Error-Info");
            Assert.IsTrue(target.GetStringValue() == "<sips:sip:56142>");

            uri = "sip:bob@fanny.com";
            target = new ErrorInfoHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Error-Info");
            Assert.IsTrue(target.CompactName == "Error-Info");
            Assert.IsTrue(target.GetStringValue() == "<sip:bob@fanny.com>");
        }

        /// <summary>
        ///A test for ErrorHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ErrorHeaderFieldConstructorTest2()
        {
            ErrorInfoHeaderField target = new ErrorInfoHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Error-Info");
            Assert.IsTrue(target.CompactName == "Error-Info");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ErrorInfoHeaderField target = new ErrorInfoHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>\t";
            target.Parse(value);
            expected = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>;param=value";
            target.Parse(value);
            expected = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>;param=value";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>;param=value;singleparam";
            target.Parse(value);
            expected = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>;param=value;singleparam";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "  Error-Info   \t:\t\r\n <sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>";
            target.Parse(value);
            expected = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tError-Info\t:\t<sip:alice:password@chicago.com>\t";
            target.Parse(value);
            expected = "<sip:alice:password@chicago.com>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<ErrorInfoHeaderField> hg = new HeaderFieldGroup<ErrorInfoHeaderField>();
            hg.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "  Error-Info   : \r\n <sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>, <sip:fred:bananas@chicago.com>";

            expected = "<sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=sip:alice>, <sip:fred:bananas@chicago.com>";
            hg.Parse(value);
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [TestMethod]
        public void UriTest()
        {
            SipUriHeaderFieldBase target = new ErrorInfoHeaderField();
            SipUri expected = new SipUri("sip:localhost:5060");
            SipUri actual;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);

            expected = new SipUri("sip:fred@localhost");
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
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