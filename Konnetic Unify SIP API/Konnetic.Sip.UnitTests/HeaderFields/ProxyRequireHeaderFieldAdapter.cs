using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ProxyRequireHeaderFieldAdapter and is intended
    ///to contain all ProxyRequireHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ProxyRequireHeaderFieldAdapter
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
            ProxyRequireHeaderField target = new ProxyRequireHeaderField();
            HeaderFieldBase expected = new ProxyRequireHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target = new ProxyRequireHeaderField("123");
            ((ProxyRequireHeaderField)expected).Option = "123";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ProxyRequireHeaderField target = new ProxyRequireHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Parse("123");
            other = new ProxyRequireHeaderField();
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ProxyRequireHeaderField)other).Option = "123";
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
            ProxyRequireHeaderField target = new ProxyRequireHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual = target.GetStringValue();
            Assert.AreEqual(expected,actual);

            value = "\tProxy-Require\t: 6\t";
            target.Parse(value);
            expected = "6";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Proxy-Require:\t \r\n  ";
            target.Parse(value);
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<ProxyRequireHeaderField> hfg = new HeaderFieldGroup<ProxyRequireHeaderField>();
            value = "Proxy-Require: \r\n 6";
            hfg.Parse(value);

            expected = "6";
            actual = hfg[0].GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Proxy-Require: 6\t,\t \thhh";
            hfg.Parse(value);

            expected = "6";
            actual = hfg[0].GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "hhh";
            actual = hfg[1].GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProxyRequireHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ProxyRequireHeaderFieldConstructorTest()
        {
            string option = string.Empty;
            ProxyRequireHeaderField target = new ProxyRequireHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Proxy-Require");
            Assert.IsTrue(target.CompactName == "Proxy-Require");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.Option == "");

            option = Common.TOKEN;
            target = new ProxyRequireHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Proxy-Require");
            Assert.IsTrue(target.CompactName == "Proxy-Require");
            Assert.IsTrue(target.GetStringValue() == Common.TOKEN);
            Assert.IsTrue(target.Option == Common.TOKEN);
        }

        /// <summary>
        ///A test for ProxyRequireHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ProxyRequireHeaderFieldConstructorTest3()
        {
            ProxyRequireHeaderField target = new ProxyRequireHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Proxy-Require");
            Assert.IsTrue(target.CompactName == "Proxy-Require");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.Option == "");
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