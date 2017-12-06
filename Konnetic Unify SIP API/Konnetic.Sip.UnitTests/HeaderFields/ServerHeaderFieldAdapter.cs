using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ServerHeaderFieldAdapter and is intended
    ///to contain all ServerHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ServerHeaderFieldAdapter
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
            ServerHeaderField target = new ServerHeaderField();
            HeaderFieldBase expected = new ServerHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Comment = "123";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ServerHeaderField)expected).Comment = "123";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.ProductName = "abc";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ServerHeaderField)expected).ProductName = "abc";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.ProductVersion = "xyz";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((ServerHeaderField)expected).ProductVersion = "xyz";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ServerHeaderField target = new ServerHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ServerHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).ProductName = "Name";
            ((ServerHeaderField)other).ProductVersion = "Version";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Comment = "\"()";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).Comment = "\"()";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ProductName = "Name";
            target.ProductVersion = "Version";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            ServerHeaderField target = new ServerHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new ServerHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)obj).Comment = "123";
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.Comment = "123";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            ServerHeaderField target = new ServerHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ServerHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).Comment = " \t";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Comment = " \t";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).ProductName = "Name";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ProductName = "Name";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).ProductVersion = "Version";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.ProductVersion = "Version";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((ServerHeaderField)other).Comment = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Comment = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest3()
        {
            ServerHeaderField target = new ServerHeaderField();
            ServerHeaderField target1 = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);

            target1 = new ServerHeaderField();
            expected = true;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);

            target1.Comment="!!!!";
            expected = false;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);

            target.Comment = "!!!!";
            expected = true;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ServerHeaderField target = new ServerHeaderField();
            string value = string.Empty;
            target.Parse(value);
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t \r\n Server        \r\n : \t abc/def \t";
            target.Parse(value);
            expected = "abc/def";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\t \r\n     \r\n \t (abc \t ü def) \t";
            target.Parse(value);
            expected = "(abc \\\t ü def)";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ServerHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ServerHeaderFieldConstructorTest()
        {
            ServerHeaderField target = new ServerHeaderField("My Comment");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Server");
            Assert.IsTrue(target.CompactName == "Server");
            Assert.IsTrue(target.GetStringValue() == "(My Comment)");

            target = new ServerHeaderField("Name","Version");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Server");
            Assert.IsTrue(target.CompactName == "Server");
            Assert.IsTrue(target.GetStringValue() == "Name/Version");
        }

        /// <summary>
        ///A test for ServerHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ServerHeaderFieldConstructorTest1()
        {
            ServerHeaderField target = new ServerHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Server");
            Assert.IsTrue(target.CompactName == "Server");
            Assert.IsTrue(target.GetStringValue() == "");
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