using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for MaxForwardsHeaderFieldAdapter and is intended
    ///to contain all MaxForwardsHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class MaxForwardsHeaderFieldAdapter
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
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            HeaderFieldBase expected = new MaxForwardsHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.MaxForwards = 255;
            expected = new MaxForwardsHeaderField(255);
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new MaxForwardsHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((MaxForwardsHeaderField)other).MaxForwards=80;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.MaxForwards = 80;
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
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new MaxForwardsHeaderField();
            expected = true;
            target.MaxForwards=1;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new MaxForwardsHeaderField();
            expected = true;
            target.MaxForwards = 255;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new MaxForwardsHeaderField();
            expected = true;
            target.MaxForwards = 0;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaxForwardsHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MaxForwardsHeaderFieldConstructorTest()
        {
            byte forwards = 0;
            MaxForwardsHeaderField target = new MaxForwardsHeaderField(forwards);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Max-Forwards");
            Assert.IsTrue(target.CompactName == "Max-Forwards")	;
            Assert.IsTrue(target.GetStringValue() == "0");

            forwards = 1;
            target = new MaxForwardsHeaderField(forwards);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Max-Forwards");
            Assert.IsTrue(target.CompactName == "Max-Forwards");
            Assert.IsTrue(target.GetStringValue() == "1");
        }

        /// <summary>
        ///A test for MaxForwardsHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MaxForwardsHeaderFieldConstructorTest1()
        { 
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Max-Forwards");
            Assert.IsTrue(target.CompactName == "Max-Forwards");
            Assert.IsTrue(target.GetStringValue() == "70");
			  
        }

        /// <summary>
        ///A test for MaxForwardsHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void MaxForwardsHeaderFieldConstructorTest2()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField(90);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Max-Forwards");
            Assert.IsTrue(target.CompactName == "Max-Forwards");
            Assert.IsTrue(target.GetStringValue() == "90");

			target = new MaxForwardsHeaderField();
			Assert.IsTrue(target.AllowMultiple == false);

			Assert.IsTrue(target.FieldName == "Max-Forwards");
			Assert.IsTrue(target.CompactName == "Max-Forwards");
			Assert.IsTrue(target.GetStringValue() == "70");
        }

        /// <summary>
        ///A test for MaxForwards
        ///</summary>
        [TestMethod]
        public void MaxForwardsTest()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            byte? expected = 0;
            byte? actual;
            target.MaxForwards = expected;
            actual = target.MaxForwards;
            Assert.AreEqual(expected, actual);

            expected = 10;
            target.MaxForwards = expected;
            actual = target.MaxForwards;
            Assert.AreEqual(expected, actual);

            expected = 255;
            target.MaxForwards = expected;
            actual = target.MaxForwards;
            Assert.AreEqual(expected, actual);

            expected = 0;
            target.MaxForwards = expected;
            actual = target.MaxForwards;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t176 \r\n";
            target.Parse(value);
            expected = "176";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
		[ExpectedException(typeof(SipParseException))]
        public void ParseTesta()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            string value = "256";
            target.Parse(value);
        }

        [TestMethod]
        public void ParseTestb()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            string value = "-1";
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            MaxForwardsHeaderField target = new MaxForwardsHeaderField();
            string expected = "70";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new MaxForwardsHeaderField(70);
            expected = "70";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "0";
            target.MaxForwards = 0;
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