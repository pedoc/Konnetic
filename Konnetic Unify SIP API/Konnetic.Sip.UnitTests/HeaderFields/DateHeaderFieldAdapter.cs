using System;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for DateHeaderFieldAdapter and is intended
    ///to contain all DateHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class DateHeaderFieldAdapter
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
            DateHeaderField target = new DateHeaderField();
            HeaderFieldBase expected = new DateHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.SetDate(DateTime.Today);
            ((DateHeaderField)expected).SetDate(DateTime.Today);
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void DateHeaderFieldConstructorTest()
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Today;
            DateHeaderField target = new DateHeaderField(dateTime);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Date");
            Assert.IsTrue(target.CompactName == "Date");

            System.Globalization.DateTimeFormatInfo i = new System.Globalization.DateTimeFormatInfo();
            string expected = DateTime.Today.ToString(i.RFC1123Pattern);

            Assert.IsTrue(target.GetStringValue() == expected);
        }

        /// <summary>
        ///A test for DateHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void DateHeaderFieldConstructorTest1()
        {
            DateHeaderField target = new DateHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Date");
            Assert.IsTrue(target.CompactName == "Date");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for Date
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void DateTest()
        {
            DateHeaderField_Accessor target = new DateHeaderField_Accessor();
            string expected = string.Empty;
            string actual;
            actual = target.Date;
            Assert.AreEqual(expected, actual);

            target.Date = expected;
            actual = target.Date;
            Assert.AreEqual(expected, actual);

            target.Date = "15/12/2009";
            expected = "15/12/2009";
            actual = target.Date;
            Assert.AreEqual(expected, actual);

            System.Globalization.DateTimeFormatInfo i = new System.Globalization.DateTimeFormatInfo();
            expected = DateTime.Now.ToString(i.RFC1123Pattern);
            target.Date = expected;
            actual = target.Date;
            Assert.AreEqual(expected, actual);

            DateTime dt = DateTime.Parse(expected);
            target.SetDate(dt);
            expected = dt.ToString(i.RFC1123Pattern);
            actual = target.Date;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            DateHeaderField target = new DateHeaderField();
            DateHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = true;
            other = new DateHeaderField();
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = false;
            other.SetDate(DateTime.Today);
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = false;
            target.SetDate(DateTime.Now);
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = true;
            target.SetDate(DateTime.Today);
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = false;
            target.SetDate(DateTime.Today.AddDays(-1));
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            DateHeaderField target = new DateHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.SetDate(DateTime.Now);
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            DateHeaderField target = new DateHeaderField();
            string value = string.Empty;
            target.Parse(value);

            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Mon,\t01\tNov\t2010\t02:09:00\tGMT\t";
            target.Parse(value);
            expected = "Mon, 01 Nov 2010 02:09:00 GMT";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Mon, 01 Nov 2010 02:09:00";
            target.Parse(value);
            expected = "Mon, 01 Nov 2010 02:09:00 GMT";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = " \t  Date  \t:\t \r\n  \r\n   Mon, 1 Nov 2010 2:9:00";
            target.Parse(value);
            expected = "Mon, 01 Nov 2010 02:09:00 GMT";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            DateHeaderField target = new DateHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            DateTime dt = new DateTime(2050,12,8,23,59,59);
            target.SetDate(dt);
            expected = "Thu, 08 Dec 2050 23:59:59 GMT";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            dt = new DateTime(2000, 1, 1, 0, 0, 0);
            target.SetDate(dt);
            expected = "Sat, 01 Jan 2000 00:00:00 GMT";
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