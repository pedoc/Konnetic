using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContactHeaderFieldAdapter and is intended
    ///to contain all ContactHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContactHeaderFieldAdapter
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
            ContactHeaderField target = new ContactHeaderField();
            HeaderFieldBase expected = new ContactHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ContactHeaderField)expected).Uri = new SipUri("sip:nnn@nnn.com");
            target.Uri = new SipUri("sip:nnn@nnn.com");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ContactHeaderField)expected).Expires = 100000;
            target.Expires = 100000;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContactHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContactHeaderFieldConstructorTest()
        {
            ContactHeaderField target = new ContactHeaderField();
            Assert.AreEqual(target.ToString(), "Contact: ");
        }

        /// <summary>
        ///A test for ContactHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContactHeaderFieldConstructorTest2()
        {
            SipUri uri = new SipUri("sip:fred@bob.com");
            string displayName = "Fred\"";
            ContactHeaderField target = new ContactHeaderField(uri, displayName);
            Assert.AreEqual("Contact: \"Fred\\\"\" <sip:fred@bob.com>", target.ToString());

            displayName = "\"Fred\t";
            target = new ContactHeaderField(uri, displayName);
            Assert.AreEqual("Contact: \"\\\"Fred\\\t\" <sip:fred@bob.com>", target.ToString());
        }

        /// <summary>
        ///A test for ContactHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContactHeaderFieldConstructorTest3()
        {
            string uri1 = "sip:localhost";
            ContactHeaderField target = new ContactHeaderField(uri1);

            Assert.AreEqual(target.Uri, new SipUri("sip:localhost"));

            uri1 = "sips:yahoo.com";
            target = new ContactHeaderField(uri1);

            Assert.AreEqual(target.Uri.ToString(), "sips:yahoo.com");
        }

        /// <summary>
        ///A test for ContactHeaderField Constructor
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ContactHeaderFieldConstructorTest4()
        {
            SipUri uri = null;
            ContactHeaderField target = new ContactHeaderField(uri);
        }

        [TestMethod]
        public void ContactHeaderFieldConstructorTest5()
        {
            SipUri uri = new SipUri();
            ContactHeaderField target = new ContactHeaderField(uri);
            Assert.AreEqual(target.Uri.Host,"localhost");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ContactHeaderField target = new ContactHeaderField(new SipUri("sips:bob:pwd7!@123.com"));
            ContactHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ContactHeaderField();
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Uri = new SipUri("sips:bob:pwd7!@123.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DisplayName="James";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Expires = 9;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Expires
        ///</summary>
        [TestMethod]
        public void ExpiresTest1()
        {
            ContactHeaderField target = new ContactHeaderField(SipUriHeaderFieldBase.DEFAULTURI);
            int? expected = null;
            int? actual;
            actual = target.Expires;
            Assert.AreEqual(expected, actual);

            expected = 8;
            target.Expires = expected;
            actual = target.Expires;
            Assert.AreEqual(expected, actual);

            string expectedStr = "<sip:localhost:5060>;expires=8";
            string actualStr;
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            target.QValue = 0.333f;

            expectedStr = "<sip:localhost:5060>;q=0.333;expires=8";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            expected = null;
            target.Expires = expected;
            actual = target.Expires;

            expectedStr = "<sip:localhost:5060>;q=0.333";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            SipUriHeaderFieldBase target = new ContactHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:123:1231@123.123.123.123");
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ContactHeaderField target = new ContactHeaderField();
            string value = string.Empty;
            target.Parse(value);

            string expectedStr = "";
            string actualStr;
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = "   Contact \t :  \r\n \"Mr. \r\n  \r\n  Watson <sip:watson@bbbb.com> ;q=0.7 ;expires=6";
            target.Parse(value);

            expectedStr = "\"Mr. Watson\" <sip:watson@bbbb.com>;q=0.7;expires=6";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = "  \r\n \"Mr. Watson\" <sip:watson@bbbb.com> ;q=0.7 ; expires=6";
            target.Parse(value);
            expectedStr = "\"Mr. Watson\" <sip:watson@bbbb.com>;q=0.7;expires=6";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = "  \r\n Mr. Watson <sip:watson@bbbb.com> ;q=0.7 ; expires=6";
            target.Parse(value);
            expectedStr = "\"Mr. Watson\" <sip:watson@bbbb.com>;q=0.7;expires=6";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = "  \r\n Mr. Watson<sip:watson@bbbb.com> ;q=0.7 ; expires=6";
            target.Parse(value);
            expectedStr = "\"Mr. Watson\" <sip:watson@bbbb.com>;q=0.7;expires=6";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = "  \r\n sip:watson@bbbb.com;q=0.7;expires=6";
            target.Parse(value);
            expectedStr = "<sip:watson@bbbb.com;q=0.7;expires=6>";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = " m: \r\n \"Mrs. Watson\" <sip:watson2@bbbb.com> ;q=0.8 ; expires=1666";
            target.Parse(value);
            expectedStr = "\"Mrs. Watson\" <sip:watson2@bbbb.com>;q=0.8;expires=1666";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = " m: \r\n \"Mrs. \"Betty\" Watson\" <sip:watson2@bbbb.com> ;q=0.8 ; expires=1666";
            target.Parse(value);
            expectedStr = "\"Mrs. \\\"Betty\\\" Watson\" <sip:watson2@bbbb.com>;q=0.8;expires=1666";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = " m: \r\n \"Mrs.\\\\ Watson\" <sip:watson2@bbbb.com> ;q=0.8 ; expires=1666";
            target.Parse(value);
            expectedStr = "\"Mrs.\\\\ Watson\" <sip:watson2@bbbb.com>;q=0.8;expires=1666";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            value = " m  \r\n\t:\t\r\n \"Mrs.\\\\  \r\n		\\\tWatson\"\t<sip:watson2@bbbb.com> ;q=0.8 ; expires=1666";
            target.Parse(value);
            expectedStr = "\"Mrs.\\\\ \\\tWatson\" <sip:watson2@bbbb.com>;q=0.8;expires=1666";
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            HeaderFieldGroup<ContactHeaderField> hfg = new HeaderFieldGroup<ContactHeaderField>();
            value = " m  \r\n\t:\t\r\n \"Mrs.\\\\ \\\" ,  \r\n		\\\tWatson\"\t<sip:watson,2@bbbb.com> ;q=0.8 ; expires=1666 , <sip:bob,@google.com> , *";
            hfg.Parse(value);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest1()
        {
            SipUriHeaderFieldBase target = new ContactHeaderField();
            string value = string.Empty;
            string expected = "";
            string actual;
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "    <sip:watson@worcester.bell-telephone.com> ;q=0.7; expires=3600";
            expected = "<sip:watson@worcester.bell-telephone.com>;q=0.7;expires=3600";
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ContactHeaderField target1 = (ContactHeaderField)target;
            HeaderFieldGroup<ContactHeaderField> hg = new HeaderFieldGroup<ContactHeaderField>();

            hg.Parse(value);
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);

            hg.Parse(value + ", " + "<sip:localhost>");
            expected = "<sip:watson@worcester.bell-telephone.com>;q=0.7;expires=3600, <sip:localhost>";
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for QValue
        ///</summary>
        [TestMethod]
        public void QValueTest()
        {
            ContactHeaderField target = new ContactHeaderField();
            float? expected = null;
            float? actual;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);

            expected = 0.1F;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);

            string expectedStr = ";q=0.1";
            string actualStr;
            actualStr = target.GetStringValue();
            Assert.AreEqual(expectedStr, actualStr);

            expected = 0F;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);

            expectedStr = "Contact: ;q=0";
            actualStr = target.ToString();
            Assert.AreEqual(expectedStr, actualStr);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            SipUriHeaderFieldBase target = new ContactHeaderField();
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Uri = new SipUri("sip:123:1231@123.123.123.123");
            expected = "<sip:123:1231@123.123.123.123>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ContactHeaderField target1 = (ContactHeaderField)target;
            HeaderFieldGroup<ContactHeaderField> hg = new HeaderFieldGroup<ContactHeaderField>();
            hg.Add(target1);

            expected = "<sip:123:1231@123.123.123.123>";
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);

            hg.Add(new ContactHeaderField(new SipUri("sip:yahoo!"),"bob"));

            expected = "<sip:123:1231@123.123.123.123>, \"bob\" <sip:yahoo>";
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [TestMethod]
        public void UriTest()
        {
            SipUriHeaderFieldBase target = new ContactHeaderField();
            SipUri expected = new SipUri("sip:localhost:5060");
            SipUri actual;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);

            expected = new SipUri("sip:bob@tlanata.com");
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void UriTest1()
        {
            SipUriHeaderFieldBase target = new ContactHeaderField();
            SipUri expected = null;
            SipUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            ContactHeaderField headerField = new ContactHeaderField("sips:fanny@dickins.com");
            string expected = "Contact: <sips:fanny@dickins.com>";
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
            string value = "\"Fanny Jones\" <sips:fanny@dickins.com>";
            ContactHeaderField expected = new ContactHeaderField(new SipUri("sips:fanny@dickins.com"),"Fanny Jones");
            ContactHeaderField actual;
            actual = value;
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