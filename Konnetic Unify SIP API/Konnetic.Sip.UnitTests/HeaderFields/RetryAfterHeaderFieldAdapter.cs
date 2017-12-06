using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RetryAfterHeaderFieldAdapter and is intended
    ///to contain all RetryAfterHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RetryAfterHeaderFieldAdapter
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
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            HeaderFieldBase expected = new RetryAfterHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Comment = Common.QUOTEDSTRING;
            actual = target.Clone();
            Assert.IsTrue(((RetryAfterHeaderField)actual).Comment == Common.QUOTEDSTRINGRESULT);
            Assert.AreNotEqual(expected, actual);

            ((RetryAfterHeaderField)expected).Comment = Common.QUOTEDSTRING;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Duration = 1;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(((RetryAfterHeaderField)actual).Duration == 1);

            ((RetryAfterHeaderField)expected).Duration = 1;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Seconds = 1;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(((RetryAfterHeaderField)actual).Seconds == 1);

            ((RetryAfterHeaderField)expected).Seconds = 1;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Comment
        ///</summary>
        [TestMethod]
        public void CommentTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            string expected = string.Empty;
            string actual;
            target.Comment = expected;
            actual = target.Comment;
            Assert.AreEqual(expected, actual);

            expected = Common.COMMENTSTRING;
            target.Comment = expected;
            actual = target.Comment;
            expected = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ú\"\\ %45()";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Duration
        ///</summary>
        [TestMethod]
        public void DurationTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            int? expected = 0;
            int? actual;
            target.Duration = expected;
            actual = target.Duration;
            Assert.AreEqual(expected, actual);

            expected = int.MaxValue;
            target.Duration = expected;
            actual = target.Duration;
            Assert.AreEqual(expected, actual);

            target = new RetryAfterHeaderField();
            expected = null;
            target.Duration = expected;
            actual = target.Duration;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            RetryAfterHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new RetryAfterHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Duration = 1;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Comment = "\\\"%Z%45";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Comment = "\\\"%Z%45";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Seconds=0;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Duration = 0;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Duration = 1;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Seconds = 1;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Duration = 0;
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
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new RetryAfterHeaderField();
            value = "Retry-After     :    ";
            target.Parse(value);
            Assert.AreEqual(expected, actual);

            value = "Retry-After    \t :    ;\tduration=99\t  ";
            target.Parse(value);
            expected = "99";
            actual = target.Duration.ToString();
            Assert.AreEqual(expected, actual);
            expected = "";
            actual = target.Seconds.ToString();
            Assert.AreEqual(expected, actual);

            value = "\tRetry-After     : \t  99  ";
            target.Parse(value);
            expected = "99";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
            expected = "99";
            actual = target.Seconds.ToString();
            Assert.AreEqual(expected, actual);

            value = "Retry-After     :   99 (comment) ";
            target.Parse(value);
            expected = "comment";
            actual = target.Comment;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RetryAfterHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RetryAfterHeaderFieldConstructorTest()
        {
            int seconds = 0;
            RetryAfterHeaderField target = new RetryAfterHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Retry-After");
            Assert.IsTrue(target.CompactName == "Retry-After");
            Assert.IsTrue(target.GetStringValue() == "0");
            Assert.IsTrue(target.HasParameters == false);

            seconds = 10;
            target = new RetryAfterHeaderField(seconds);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Retry-After");
            Assert.IsTrue(target.CompactName == "Retry-After");
            Assert.IsTrue(target.GetStringValue() == "10");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for RetryAfterHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RetryAfterHeaderFieldConstructorTest1()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Retry-After");
            Assert.IsTrue(target.CompactName == "Retry-After");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
        }

        /// <summary>
        ///A test for Seconds
        ///</summary>
        [TestMethod]
        public void SecondsTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            int? expected = 0;
            int? actual;
            target.Seconds = expected;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);

            expected = int.MaxValue;
            target.Seconds = expected;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);

            expected = null;
            target.Seconds = expected;
            actual = target.Seconds;
            Assert.AreEqual(expected, actual);
        }
 

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            RetryAfterHeaderField target = new RetryAfterHeaderField();
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Seconds = 1000;
            expected = "1000";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Duration = 1;
            expected = "1000;duration=1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Comment = "\"";
            expected = "1000 (\");duration=1";
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