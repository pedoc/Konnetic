using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for PriorityHeaderFieldAdapter and is intended
    ///to contain all PriorityHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class PriorityHeaderFieldAdapter
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
            PriorityHeaderField target = new PriorityHeaderField();
            HeaderFieldBase expected = new PriorityHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((PriorityHeaderField)expected).Priority="123";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            target.Priority = "123";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            PriorityHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new PriorityHeaderField();
            other.Priority = "ABC";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Priority = "abc";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Priority = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Priority = "";
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
            PriorityHeaderField target = new PriorityHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Priority = "a";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Priority = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = Common.TOKEN;
            target.Parse(value);
            expected = Common.TOKEN;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "pRIORITY: \r\n "+ Common.TOKEN;
            target.Parse(value);
            expected = Common.TOKEN;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "PRIORITY" + Common.TOKEN;
            target.Parse(value);
            expected = "PRIORITY" + Common.TOKEN;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "PRIORITY: emergency";
            target.Parse(value);
            expected = "emergency";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PriorityHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void PriorityHeaderFieldConstructorTest()
        {
            Priority priority = Priority.Normal;
            PriorityHeaderField target = new PriorityHeaderField(priority);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Priority");
            Assert.IsTrue(target.CompactName == "Priority");
            Assert.IsTrue(target.GetStringValue() == "normal");
        }

        /// <summary>
        ///A test for PriorityHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void PriorityHeaderFieldConstructorTest1()
        {
            string priority = string.Empty;
            PriorityHeaderField target = new PriorityHeaderField(priority);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Priority");
            Assert.IsTrue(target.CompactName == "Priority");
            Assert.IsTrue(target.GetStringValue() == "");

            priority = Common.TOKEN;
            target = new PriorityHeaderField(priority);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Priority");
            Assert.IsTrue(target.CompactName == "Priority");
            Assert.IsTrue(target.GetStringValue() == Common.TOKEN);
        }

        /// <summary>
        ///A test for PriorityHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void PriorityHeaderFieldConstructorTest2()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Priority");
            Assert.IsTrue(target.CompactName == "Priority");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for Priority
        ///</summary>
        [TestMethod]
        public void PriorityTest()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.Priority;
            Assert.AreEqual(expected, actual);

            target.Priority = expected;
            actual = target.Priority;
            Assert.AreEqual(expected, actual);

            expected = "\t\r\n "+Common.TOKEN;
            target.Priority = expected;
            actual = target.Priority;
            expected =  Common.TOKEN;
            Assert.AreEqual(expected, actual);

            expected = "\t";
            target.Priority = expected;
            expected = "";
            actual = target.Priority;
            Assert.AreEqual(expected, actual);
        }

        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void PriorityTest1()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            string expected = Common.TOKENRESERVED;
            string actual;
            target.Priority = expected;
            actual = target.Priority;
            Assert.AreEqual(expected, actual);
        }

        public void PriorityTest2()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(PriorityTestThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            PriorityHeaderField target = new PriorityHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Priority = Common.TOKEN;
            expected = Common.TOKEN;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        private bool PriorityTestThrowsError(string val)
        {
            try
                {
                PriorityHeaderField target = new PriorityHeaderField();
                target.Priority = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
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