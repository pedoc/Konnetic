using Konnetic.Sip;
using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HeaderFieldAdapter and is intended
    ///to contain all HeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HeaderFieldBaseAdapter
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
        ///A test for CompareTo
        ///</summary>
        [TestMethod]
        public void CompareToTest()
        {
            HeaderFieldBase target = CreateHeaderField();
            HeaderFieldBase other = new ToHeaderField(new SipUri("sip:Bob@bob.com"));
            int expected = 0;
            int actual;
            actual = target.CompareTo(other);
            Assert.AreEqual(expected, actual);

            other = new FromHeaderField(new SipUri("sip:Bob@bob.com"));
            expected = 14;
            actual = target.CompareTo(other);
            Assert.AreEqual(expected, actual);

            other = new ViaHeaderField("bbb");
            expected = -2;
            actual = target.CompareTo(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            HeaderFieldBase_Accessor target = CreateHeaderFieldBase_Accessor();
            HeaderFieldBase headerField = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Test the Constructor");

            expected = true;
            headerField = new ToHeaderField(new SipUri("sip:Bob@bob.com"));
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Test equality with an equivalent HeaderField");

            expected = false;
            headerField = new FromHeaderField(new SipUri("sip:Bob@bob.com"));
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Test inequality with a different HeaderField type");

            headerField = new ToHeaderField(new SipUri("sip:Fred@bob.com"));
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Test inequality with a different value");
        }

        /// <summary>
        ///A test for FieldName
        ///</summary>
        [TestMethod]
        public void FieldNameTest()
        {
            HeaderFieldBase_Accessor target = CreateHeaderFieldBase_Accessor();
            string expected = "To";
            string actual;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the Constructor");
            expected = "Name";
            target.FieldName = expected;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "After an assignement");
        }

        /// <summary>
        ///A test for FieldValue
        ///</summary>
        [TestMethod]
        public void FieldValueTest()
        {
            HeaderFieldBase target = CreateHeaderField();
            string expected = "<sip:Bob@bob.com>";
            string actual;
            actual = target.GetStringValue();
            Assert.IsTrue(actual.StartsWith(expected), "Test the Constructor");
            expected = "<sip:Bob@bob.com>";
            target.Parse(expected);
            actual = target.GetStringValue();
            Assert.IsTrue(actual.StartsWith(expected), "After an assignement");
        }

        /// <summary>
        ///A test for MultipleAllowed
        ///</summary>
        [TestMethod]
        public void MultipleAllowedTest()
        {
            HeaderFieldBase_Accessor target = CreateHeaderFieldBase_Accessor();
            bool expected = false;
            bool actual;
            actual = target.AllowMultiple;
            Assert.AreEqual(expected, actual, "Test the Constructor");
            expected = true;
            target.AllowMultiple = expected;
            actual = target.AllowMultiple;
            Assert.AreEqual(expected, actual, "After an assignement");
        }

        /// <summary>
        ///A test for ToBytes
        ///</summary>
        [TestMethod]
        public void ToBytesTest()
        {
            HeaderFieldBase target = CreateHeaderField();

            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("To: <sip:Bob@bob.com>".ToCharArray());
            byte[] actual;
            actual = target.GetBytes();
            Assert.AreEqual(new string(System.Text.UTF8Encoding.UTF8.GetChars(expected)), new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)));
        }

        /// <summary>
        ///A test for ToChars
        ///</summary>
        [TestMethod]
        public void ToCharsTest()
        {
            HeaderFieldBase target = CreateHeaderField();
            char[] expected = "To: <sip:Bob@bob.com>".ToCharArray();
            char[] actual;
            actual = target.GetChars();
            Assert.AreEqual(new string(expected), new string(actual));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            HeaderFieldBase_Accessor target = CreateHeaderFieldBase_Accessor();
            string expected = "To: <sip:Bob@bob.com>";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Test the Constructor");

            target.FieldName = "Name";
            expected = "Name: <sip:Bob@bob.com>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Just a name");

            target.Parse("sip:Value@bob.com");
            expected = "Name: <sip:Value@bob.com>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Both name and value");

            target.Parse("sip:Value");
            target.AllowMultiple = true;
            expected = "Name: <sip:Value>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Check no Change to Cache 1");

            actual = target.GetString(true);
            expected = "t: <sip:Value>";
            Assert.AreEqual(expected, actual, "Check no Change to Cache 3");

            target.FieldName = "Name";
            expected = "Name: <sip:Value>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Check Change to Cache 1a");

			actual = target.GetString(false);
            expected = "Name: <sip:Value>";
            Assert.AreEqual(expected, actual, "Check Change to Cache 1b");

            target.FieldName = "Name1";
            actual = target.ToString();
            expected = "Name1: <sip:Value>";
            Assert.AreEqual(expected, actual, "Check Change to Cache 2");

            target.Parse("sip:Value1");
            actual = target.ToString();
            expected = "Name1: <sip:Value1>";
            Assert.AreEqual(expected, actual, "Check Change to Cache 3");

            target.FieldName = "Name";
            expected = "Name: <sip:Value1>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Check Final OK");
        }

        [ExpectedException(typeof(System.FormatException))]
        public void ToStringTest1a()
        {
            HeaderFieldBase_Accessor target = CreateHeaderFieldBase_Accessor();
            string actual;
            string expected = string.Empty;
            target.FieldName = string.Empty;
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Check Change to Cache 4");
        }

        internal virtual HeaderFieldBase CreateHeaderField()
        {
            HeaderFieldBase target = new ToHeaderField(new SipUri("sip:Bob@bob.com"));
            return target;
        }

        internal virtual HeaderFieldBase_Accessor CreateHeaderFieldBase_Accessor()
        {
            PrivateObject p = new PrivateObject(new ToHeaderField(new SipUri("sip:Bob@bob.com")));
            HeaderFieldBase_Accessor target = new HeaderFieldBase_Accessor(p);
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