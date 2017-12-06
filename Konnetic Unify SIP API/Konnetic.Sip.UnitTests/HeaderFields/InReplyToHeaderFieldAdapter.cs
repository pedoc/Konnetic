using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for InReplyToHeaderFieldAdapter and is intended
    ///to contain all InReplyToHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class InReplyToHeaderFieldAdapter
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
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            InReplyToHeaderField target = new InReplyToHeaderField();
            InReplyToHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new InReplyToHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new InReplyToHeaderField("ggg");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new InReplyToHeaderField("ggg");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new InReplyToHeaderField("");
            target = new InReplyToHeaderField("");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InReplyToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void InReplyToHeaderFieldConstructorTest()
        {
            string callId = string.Empty;
            InReplyToHeaderField target = new InReplyToHeaderField(callId);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "In-Reply-To");
            Assert.IsTrue(target.CompactName == "In-Reply-To");
            Assert.IsTrue(target.GetStringValue() == "");

            callId = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~()<>:\\\"/[]?{}@";
            target = new InReplyToHeaderField(callId);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "In-Reply-To");
            Assert.IsTrue(target.CompactName == "In-Reply-To");
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~()<>:\\\"/[]?{}@");
        }

        /// <summary>
        ///A test for InReplyToHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void InReplyToHeaderFieldConstructorTest1()
        {
            InReplyToHeaderField target = new InReplyToHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "In-Reply-To");
            Assert.IsTrue(target.CompactName == "In-Reply-To");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void InReplyToHeaderFieldConstructorTesta()
        {
            string callId = null;
            InReplyToHeaderField target = new InReplyToHeaderField(callId);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            InReplyToHeaderField target = new InReplyToHeaderField();
            string value = string.Empty;
            target.Parse(value);

            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual, "Test the constructor");

            value = "\t" + Common.WORD+"@"+ Common.WORD;
            target.Parse(value);
            expected = Common.WORD + "@" + Common.WORD;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tIn-Reply-To\t:" + "\t" + Common.WORD + "@" + Common.WORD;
            ;
            target.Parse(value);
            expected = Common.WORD + "@" + Common.WORD;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "  In-RePly-To: \r\n " + Common.WORD + "@" + Common.WORD + " \r\n ";
            target.Parse(value);
            expected = Common.WORD + "@" + Common.WORD;
            actual = target.GetStringValue();
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