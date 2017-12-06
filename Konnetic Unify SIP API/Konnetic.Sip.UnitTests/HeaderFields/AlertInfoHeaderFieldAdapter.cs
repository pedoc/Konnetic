using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AlertInfoHeaderFieldAdapter and is intended
    ///to contain all AlertInfoHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AlertInfoHeaderFieldAdapter
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
        ///A test for AlertInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AlertInfoHeaderFieldConstructorTest()
        {
            AlertInfoHeaderField target = new AlertInfoHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Alert-Info");
            Assert.IsTrue(target.CompactName == "Alert-Info");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://localhost/");
        }

        /// <summary>
        ///A test for AlertInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AlertInfoHeaderFieldConstructorTest1()
        {
            string uri = "http://localhost";
            AlertInfoHeaderField target = new AlertInfoHeaderField(uri);
            Assert.IsTrue(target.AbsoluteUri == new System.Uri(uri));

            uri = "http://bob@love.com";
            target = new AlertInfoHeaderField(uri);
            Assert.IsTrue(target.AbsoluteUri.Host == "love.com");
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://bob@love.com/");
        }

        /// <summary>
        ///A test for AlertInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AlertInfoHeaderFieldConstructorTest2()
        {
            System.Uri uri = new System.Uri("http://atlanta.com");
            AlertInfoHeaderField target = new AlertInfoHeaderField(uri);
            Assert.IsTrue(target.AbsoluteUri.Host == "atlanta.com");
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://atlanta.com/");
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AlertInfoHeaderField target = new AlertInfoHeaderField("http://atlanta.com");
            HeaderFieldBase expected = new AlertInfoHeaderField("http://atlanta.com");
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddParameter("sSs", "uuu");
            ((AlertInfoHeaderField)expected).AddParameter("sss", "uuu");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AlertInfoHeaderField> hg = new HeaderFieldGroup<AlertInfoHeaderField>();
            hg.Add(target);
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            hg.Add(new AlertInfoHeaderField("http://www.janenonnon.com"));
            actual = hg.Clone();
            HeaderFieldGroup<AlertInfoHeaderField> hg1 = actual as HeaderFieldGroup<AlertInfoHeaderField>;
            bool b = hg.Equals(hg1);

            Assert.IsTrue(b);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AlertInfoHeaderField target = new AlertInfoHeaderField("http://fred@atlanta.com");
            AlertInfoHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AlertInfoHeaderField("http://fred@atlanta.com");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.AddParameter("sss", "uuu");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AlertInfoHeaderField target = new AlertInfoHeaderField();
            string value = string.Empty;
            target.Parse(value);

            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://localhost/");
            Assert.IsTrue(target.HasParameters == false);

            value = "<http://fred@jjj.com>";
            target = new AlertInfoHeaderField();
            target.Parse(value);
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://fred@jjj.com/");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.GetStringValue() == "<http://fred@jjj.com/>");
            Assert.IsTrue(target.ToString() == "Alert-Info: <http://fred@jjj.com/>");

            value = "\t<http://fred@jjj.com>;\tggg=ssss";
            target = new AlertInfoHeaderField();
            target.Parse(value);
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://fred@jjj.com/");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.GenericParameters[0].Name == "ggg");
            Assert.IsTrue(target.GetStringValue() == "<http://fred@jjj.com/>;ggg=ssss");

            value = " Alert-Info  \t :\t<http://fred@jjj.com>\t;ggg=ssss";
            target = new AlertInfoHeaderField();
            target.Parse(value);
            Assert.IsTrue(target.AbsoluteUri.ToString() == "http://fred@jjj.com/");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.GenericParameters[0].Name == "ggg");
            Assert.IsTrue(target.GetStringValue() == "<http://fred@jjj.com/>;ggg=ssss");

            HeaderFieldGroup<AlertInfoHeaderField> hfg = new HeaderFieldGroup<AlertInfoHeaderField>();
            value = " Alert-Info  \t :\t<http://fred@jjj.com>\t;ggg=ssss,<http://henry@jjj.com>\t;ggg=ssss ";
            hfg.Parse(value);

            Assert.IsTrue(hfg[0].AbsoluteUri.ToString() == "http://fred@jjj.com/");
            Assert.IsTrue(hfg[1].AbsoluteUri.ToString() == "http://henry@jjj.com/");
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