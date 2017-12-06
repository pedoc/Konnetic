using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RequireHeaderFieldAdapter and is intended
    ///to contain all RequireHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RequireHeaderFieldAdapter
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
            RequireHeaderField target = new RequireHeaderField();
            HeaderFieldBase expected = new RequireHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target = new RequireHeaderField("henry");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((RequireHeaderField)expected).Option = "henry";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            RequireHeaderField target = new RequireHeaderField();
            string value = string.Empty;
            target.Parse(value);

            target = new RequireHeaderField();
            value = "Require\t:\t" + Common.TOKEN;
            target.Parse(value);
            string expected = Common.TOKEN;
            string actual = target.Option;
            Assert.AreEqual(expected, actual);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "Require : ";
            target.Parse(value);
            expected = string.Empty;
            actual = target.Option;
            Assert.AreEqual(expected, actual);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "\r\n \t"+Common.TOKEN;
            target.Parse(value);
            expected = Common.TOKEN;
            actual = target.Option;
            Assert.AreEqual(expected, actual);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = string.Empty;
            target.Parse(value);
            expected = string.Empty;
            actual = target.Option;
            Assert.AreEqual(expected, actual);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<RequireHeaderField> hfg = new HeaderFieldGroup<RequireHeaderField>();
            value = "Require\t : 1234\t, 5678";
            hfg.Parse(value);
            expected = "1234";
            actual = hfg[0].Option;
            Assert.AreEqual(expected, actual);
            actual = hfg[0].GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "5678";
            actual = hfg[1].Option;
            Assert.AreEqual(expected, actual);
            actual = hfg[1].GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "1234, ";
            hfg.Parse(value);
            expected = "1234";
            actual = hfg[0].Option;
            Assert.AreEqual(expected, actual);
            actual = hfg[0].GetStringValue();
            Assert.AreEqual(expected, actual);
            expected = "";
            actual = hfg[1].Option;
            Assert.AreEqual(expected, actual);
            actual = hfg[1].GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RequireHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RequireHeaderFieldConstructorTest()
        {
            string option = string.Empty;
            RequireHeaderField target = new RequireHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Require");
            Assert.IsTrue(target.CompactName == "Require");
            Assert.IsTrue(target.GetStringValue() == "");

            option = Common.TOKEN;
            target = new RequireHeaderField(option);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Require");
            Assert.IsTrue(target.CompactName == "Require");
            Assert.IsTrue(target.GetStringValue() == Common.TOKEN);
            Assert.IsTrue(target.GetStringValue() == Common.TOKEN);
            Assert.IsTrue(target.Option == Common.TOKEN);
        }

        /// <summary>
        ///A test for RequireHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void RequireHeaderFieldConstructorTest1()
        {
            RequireHeaderField target = new RequireHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Require");
            Assert.IsTrue(target.CompactName == "Require");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            RequireHeaderField headerField = new RequireHeaderField("bob");
            string expected = "bob";
            string actual;
            actual = ((string)(headerField));
            expected = "Require: bob";
            Assert.AreEqual(expected, actual);

            expected = "fred";
            headerField = "fred";
            actual = ((string)(headerField));
            expected = "Require: fred";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "";
            RequireHeaderField expected = new RequireHeaderField();
            RequireHeaderField actual = new RequireHeaderField();
            actual = value;
            Assert.AreEqual(expected, actual);

            value = "Fred";
            expected = new RequireHeaderField(value);
            actual = new RequireHeaderField();
            actual = value;
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