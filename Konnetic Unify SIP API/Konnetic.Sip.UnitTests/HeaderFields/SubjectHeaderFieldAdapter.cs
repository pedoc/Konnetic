using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for SubjectHeaderFieldAdapter and is intended
    ///to contain all SubjectHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class SubjectHeaderFieldAdapter
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
            SubjectHeaderField target = new SubjectHeaderField();
            HeaderFieldBase expected = null;
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            expected = new SubjectHeaderField();
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Subject = Common.TEXTUTF8TRIM;
            expected = new SubjectHeaderField();
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(((SubjectHeaderField)actual).Subject == Common.TEXTUTF8TRIMRESULT);
            ((SubjectHeaderField)expected).Subject = Common.TEXTUTF8TRIM;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            SubjectHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new SubjectHeaderField();
            other.Subject = Common.TEXTUTF8TRIM;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Subject = Common.TEXTUTF8TRIM;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Subject = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Subject = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.Subject = Common.TEXTUTF8TRIM;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = Common.TEXTUTF8TRIM;
            target.Parse(value);
            expected = Common.TEXTUTF8TRIMRESULT;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\ts\t:\t"+Common.TEXTUTF8TRIM;
            target.Parse(value);
            expected = Common.TEXTUTF8TRIMRESULT;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tsuBJect\r\n \t:\t " + Common.TEXTUTF8TRIM+ "\r\n ";
            target.Parse(value);
            expected = Common.TEXTUTF8TRIMRESULT;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubjectHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void SubjectHeaderFieldConstructorTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Subject");
            Assert.IsTrue(target.CompactName == "s");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for SubjectHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void SubjectHeaderFieldConstructorTest1()
        {
            string subject = string.Empty;
            SubjectHeaderField target = new SubjectHeaderField(subject);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Subject");
            Assert.IsTrue(target.CompactName == "s");
            Assert.IsTrue(target.GetStringValue() == "");

            subject = Common.TEXTUTF8TRIM;
            target = new SubjectHeaderField(subject);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Subject");
            Assert.IsTrue(target.CompactName == "s");
            Assert.IsTrue(target.GetStringValue() == Common.TEXTUTF8TRIMRESULT);
        }

        /// <summary>
        ///A test for Subject
        ///</summary>
        [TestMethod]
        public void SubjectTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            string expected = string.Empty;
            string actual;
            target.Subject = expected;
            actual = target.Subject;
            Assert.AreEqual(expected, actual);

            expected = Common.TEXTUTF8TRIM;
            target.Subject = expected;
            actual = target.Subject;
            expected = Common.TEXTUTF8TRIMRESULT;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            SubjectHeaderField target = new SubjectHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Subject = Common.TEXTUTF8TRIM;
            expected = Common.TEXTUTF8TRIMRESULT;
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