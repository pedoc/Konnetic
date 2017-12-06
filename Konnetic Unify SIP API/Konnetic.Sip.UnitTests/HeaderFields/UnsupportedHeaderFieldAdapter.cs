using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for UnsupportedHeaderFieldAdapter and is intended
    ///to contain all UnsupportedHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class UnsupportedHeaderFieldAdapter
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
            UnsupportedHeaderField target = new UnsupportedHeaderField();
            HeaderFieldBase expected = new UnsupportedHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Option = Common.TOKEN;
            expected = new UnsupportedHeaderField();
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((UnsupportedHeaderField)expected).Option = "123";
            Assert.AreNotEqual(expected, actual);
            ((UnsupportedHeaderField)expected).Option = Common.TOKEN;
            Assert.AreEqual(expected, actual);

            target.Option = string.Empty;
            expected = new UnsupportedHeaderField();
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((UnsupportedHeaderField)expected).Option = "123";
            Assert.AreNotEqual(expected, actual);
            ((UnsupportedHeaderField)expected).Option = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            UnsupportedHeaderField target = new UnsupportedHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new UnsupportedHeaderField();
            ((UnsupportedHeaderField)other).Option = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Option = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Option = System.String.Empty;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((UnsupportedHeaderField)other).Option = System.String.Empty;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            UnsupportedHeaderField target = new UnsupportedHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "UnsuPPorted:";
            target.Parse(value);
            expected = string.Empty;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "UnsuPPorted: a";
            target.Parse(value);
            expected = "a";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tUnsuPPorted\t:\r\n \t.";
            target.Parse(value);
            expected = ".";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\tUnsuPPorted\t:\r\n \t."+Common.TOKEN;
            target.Parse(value);
            expected = "." + Common.TOKEN;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UnsupportedHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void UnsupportedHeaderFieldConstructorTest()
        {
            UnsupportedHeaderField target = new UnsupportedHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Unsupported");
            Assert.IsTrue(target.CompactName == "Unsupported");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for UnsupportedHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void UnsupportedHeaderFieldConstructorTest1()
        {
            string option = string.Empty;
            UnsupportedHeaderField target = new UnsupportedHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Unsupported");
            Assert.IsTrue(target.CompactName == "Unsupported");
            Assert.IsTrue(target.GetStringValue() == "");

            option = Common.TOKEN;
            target = new UnsupportedHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Unsupported");
            Assert.IsTrue(target.CompactName == "Unsupported");
            Assert.IsTrue(target.GetStringValue() == Common.TOKEN);
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