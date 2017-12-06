using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for GenericHeaderFieldAdapter and is intended
    ///to contain all GenericHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class GenericHeaderFieldAdapter
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
            ExtensionHeaderField target = new ExtensionHeaderField("Name");
            HeaderFieldBase expected = new ExtensionHeaderField("Name");
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GenericHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void GenericHeaderFieldConstructorTest()
        {
            string name = Common.TOKEN;
            string value = string.Empty;
            ExtensionHeaderField target = new ExtensionHeaderField(name, value);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == Common.TOKEN);
            Assert.IsTrue(target.CompactName == Common.TOKEN);
            Assert.IsTrue(target.GetStringValue() == "");

            name = Common.TOKEN;
            value = "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target = new ExtensionHeaderField(name, value);
            Assert.IsTrue(target.GetStringValue() == "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~");
        }

        /// <summary>
        ///A test for GenericHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void GenericHeaderFieldConstructorTest1()
        {
            string name = Common.TOKEN;
            ExtensionHeaderField target = new ExtensionHeaderField(name);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == Common.TOKEN);
            Assert.IsTrue(target.CompactName == Common.TOKEN);
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for GenericHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void GenericHeaderFieldConstructorTest2()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("Name");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Name");
            Assert.IsTrue(target.CompactName == "Name");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        [TestMethod]
		[ExpectedException(typeof(System.ArgumentException))]
        public void GenericHeaderFieldConstructorTesta()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("", "hhh");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("Generic");
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "  Generic :   úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target.Parse(value);
            expected = "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("Name");
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target.Value = expected;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod]
        public void ValueTest()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("Name");
            string expected = string.Empty;
            string actual;
            actual = target.Value;
            Assert.AreEqual(expected, actual);

            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);

            expected = "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod]
        public void ValueTest1()
        {
            ExtensionHeaderField target = new ExtensionHeaderField("Name");
            string expected = "úabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            string actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);

            expected = "";
            target.Value = expected;
            actual = target.Value;
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