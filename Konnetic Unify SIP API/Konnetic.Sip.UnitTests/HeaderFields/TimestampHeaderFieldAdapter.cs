using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for TimestampHeaderFieldAdapter and is intended
    ///to contain all TimestampHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class TimestampHeaderFieldAdapter
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
            TimestampHeaderField target = new TimestampHeaderField();
            HeaderFieldBase expected = new TimestampHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual); 

            target.Time = 54f;
            ((TimestampHeaderField)expected).Time = 54f;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Time = 54.50496111f;
            ((TimestampHeaderField)expected).Time = 54.50496111f;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual.GetStringValue() == "54.504");

            target.Delay = 0.001f;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((TimestampHeaderField)expected).Delay = 0.001f;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual.GetStringValue() == "54.504 0.001");

            ((TimestampHeaderField)expected).Delay = 0f;
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(expected.GetStringValue() == "54.504 0");
        }

        /// <summary>
        ///A test for Delay
        ///</summary>
        [TestMethod]
        public void DelayTest()
        {
            TimestampHeaderField target = new TimestampHeaderField();
            float? expected = 0F;
            float? actual;
            target.Delay = expected;
            actual = target.Delay;
            Assert.AreEqual(expected, actual);

            expected = 999990F;
            target.Delay = expected;
            actual = target.Delay;
            Assert.AreEqual(expected, actual);

            expected = 999990.2341234F;
            target.Delay = expected;
            actual = target.Delay;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            TimestampHeaderField target = new TimestampHeaderField();
            TimestampHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new TimestampHeaderField();
            ((TimestampHeaderField)other).Delay = 0.005f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Delay = 0.0049f;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Time = 99999999999f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((TimestampHeaderField)other).Time = 99999999998f;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((TimestampHeaderField)other).Time = 99999999999f;
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
            TimestampHeaderField target = new TimestampHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Delay=1f;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new TimestampHeaderField();
            target.Time = 1f;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Delay = 1f;
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
            TimestampHeaderField target = new TimestampHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "0";
            target.Parse(value);
            expected = "0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\r\n Timestamp \t:\t 0.001332123";
            target.Parse(value);
            expected = "0.001";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\r\n Timestamp \t:\t 0.001332123 0";
            target.Parse(value);
            expected = "0.001 0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\r\n Timestamp \t:\t 0.001332123 732534.12";
            target.Parse(value);
            expected = "0.001 732534.1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t 0.001332123\r\n 732534.1";
            target.Parse(value);
            expected = "0.001 732534.1";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Time
        ///</summary>
        [TestMethod]
        public void TimeTest()
        {
            TimestampHeaderField target = new TimestampHeaderField();
            float? expected = 0F;
            float? actual;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);

            expected = 1110F;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);

            expected = 1110.000000F;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);

            expected = 1110.000001F;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimestampHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void TimestampHeaderFieldConstructorTest()
        {
            TimestampHeaderField target = new TimestampHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Timestamp");
            Assert.IsTrue(target.CompactName == "Timestamp");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for TimestampHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void TimestampHeaderFieldConstructorTest1()
        {
            float time = 0F;
            float delay = 0F;
            TimestampHeaderField target = new TimestampHeaderField(time, delay);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Timestamp");
            Assert.IsTrue(target.CompactName == "Timestamp");
            Assert.IsTrue(target.GetStringValue() == "0 0");
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            TimestampHeaderField target = new TimestampHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Time = 13.9999f;
            expected = "13.999";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Delay = 0f;
            expected = "13.999 0";
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