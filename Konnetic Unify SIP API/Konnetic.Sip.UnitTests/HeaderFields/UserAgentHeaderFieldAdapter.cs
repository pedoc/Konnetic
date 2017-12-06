using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for UserAgentHeaderFieldAdapter and is intended
    ///to contain all UserAgentHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class UserAgentHeaderFieldAdapter
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
            UserAgentHeaderField target = new UserAgentHeaderField();
            HeaderFieldBase expected = new UserAgentHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Comment = "123";
            ((UserAgentHeaderField)expected).Comment = "123";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.ProductName = "456";
            ((UserAgentHeaderField)expected).ProductName = "456";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.ProductVersion = "...";
            ((UserAgentHeaderField)expected).ProductVersion = "...";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            UserAgentHeaderField target = new UserAgentHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new UserAgentHeaderField();
            target.Comment=Common.COMMENTSTRING;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((UserAgentHeaderField)other).Comment = Common.COMMENTSTRING;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ProductName = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((UserAgentHeaderField)other).ProductName = Common.TOKEN;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ProductVersion = Common.TOKEN;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((UserAgentHeaderField)other).ProductVersion = Common.TOKEN;
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
            UserAgentHeaderField target = new UserAgentHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value ="USER-Agent  : ";
            target.Parse(value);
            expected = string.Empty;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "USER-Agent\r\n :\r\n a";
            target.Parse(value);
            expected = "a";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "USER-Agent\r\n :\r\n (ü)";
            target.Parse(value);
            expected = "(ü)";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "USER-Agent\r\n :\r\n a/b";
            target.Parse(value);
            expected = "a/b";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UserAgentHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void UserAgentHeaderFieldConstructorTest()
        {
            UserAgentHeaderField target = new UserAgentHeaderField("");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "User-Agent");
            Assert.IsTrue(target.CompactName == "User-Agent");
            Assert.IsTrue(target.GetStringValue() == "");

            target = new UserAgentHeaderField("222");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "User-Agent");
            Assert.IsTrue(target.CompactName == "User-Agent");
            Assert.IsTrue(target.GetStringValue() == "(222)");
        }

        /// <summary>
        ///A test for UserAgentHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void UserAgentHeaderFieldConstructorTest1()
        {
            UserAgentHeaderField target = new UserAgentHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "User-Agent");
            Assert.IsTrue(target.CompactName == "User-Agent");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        [TestMethod]
        public void UserAgentHeaderFieldConstructorTest3()
        {
            UserAgentHeaderField target = new UserAgentHeaderField("","");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "User-Agent");
            Assert.IsTrue(target.CompactName == "User-Agent");
            Assert.IsTrue(target.GetStringValue() == "");

            target = new UserAgentHeaderField("...","!!!");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "User-Agent");
            Assert.IsTrue(target.CompactName == "User-Agent");
            Assert.IsTrue(target.GetStringValue() == ".../!!!");
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