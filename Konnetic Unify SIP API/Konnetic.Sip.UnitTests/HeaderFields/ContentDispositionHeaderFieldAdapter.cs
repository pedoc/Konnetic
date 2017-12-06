using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContentDispositionHeaderFieldAdapter and is intended
    ///to contain all ContentDispositionHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContentDispositionHeaderFieldAdapter
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
            DispositionType dispositionType = DispositionType.Session;
            ContentDispositionHandling handling = ContentDispositionHandling.Optional;
            ContentDispositionHeaderField target = new ContentDispositionHeaderField(dispositionType, handling);
            target.AddParameter("bob", "fanny");
            HeaderFieldBase expected = new ContentDispositionHeaderField();
            ((ContentDispositionHeaderField)expected).Handling = "optional";
            ((ContentDispositionHeaderField)expected).DispositionType = "session";
            ((ContentDispositionHeaderField)expected).AddParameter("bob", "fanny");
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentDispositionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentDispositionHeaderFieldConstructorTest()
        {
            string dispositionType = string.Empty;
            ContentDispositionHeaderField target = new ContentDispositionHeaderField(dispositionType);
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            dispositionType = "optional";
            target = new ContentDispositionHeaderField(dispositionType);
            expected = "optional";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentDispositionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentDispositionHeaderFieldConstructorTest1()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();

            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Disposition");
            Assert.IsTrue(target.CompactName == "Content-Disposition");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
            string expected = "";
            string actual;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);

            actual = target.Handling;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentDispositionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentDispositionHeaderFieldConstructorTest2()
        {
            DispositionType dispositionType = DispositionType.Render;
            ContentDispositionHandling handling = ContentDispositionHandling.Required;
            ContentDispositionHeaderField target = new ContentDispositionHeaderField(dispositionType, handling);

            string expected = "render";
            string actual;
            target.DispositionType = expected;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);

            expected = "required";
            target.Handling = expected;
            actual = target.Handling;
            Assert.AreEqual(expected, actual);

            dispositionType = DispositionType.None;
            handling = ContentDispositionHandling.Optional;
            target = new ContentDispositionHeaderField(dispositionType);
            expected = "";
            target.DispositionType = expected;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);

            expected = "optional";
            target.Handling = expected;
            actual = target.Handling;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentDispositionHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentDispositionHeaderFieldConstructorTest3()
        {
            DispositionType dispositionType = DispositionType.Icon;
            ContentDispositionHeaderField target = new ContentDispositionHeaderField(dispositionType);
            string expected = "icon";
            string actual;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);

            dispositionType = DispositionType.None;
            target = new ContentDispositionHeaderField(dispositionType);
            expected = "";
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DispositionType
        ///</summary>
        [TestMethod]
        public void DispositionTypeTest()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);

            expected = Common.TOKEN;
            target.DispositionType = expected;
            actual = target.DispositionType;
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz0123456789-.!%*_+`'~", actual);

            expected = "";
            target.DispositionType = expected;
            actual = target.DispositionType;
            Assert.AreEqual(expected, actual);
        }

        public void DispositionTypeTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(DispositionTypeThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            DispositionType dispositionType = DispositionType.None;
            ContentDispositionHandling handling = ContentDispositionHandling.Optional;
            ContentDispositionHeaderField target = new ContentDispositionHeaderField(dispositionType, handling);
            ContentDispositionHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ContentDispositionHeaderField();
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.DispositionType = "bob";
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Handling = "bob";
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Handling = "Optional";
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            expected = true;
            other.DispositionType = "";
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.DispositionType = "Optional";
            other.DispositionType = "optional";
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Handling
        ///</summary>
        [TestMethod]
        public void HandlingTest()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.Handling;
            Assert.AreEqual(expected, actual);

            expected = Common.TOKEN;
            target.Handling = expected;
            actual = target.Handling;
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz0123456789-.!%*_+`'~", actual);

            expected = "";
            target.Handling = expected;
            actual = target.Handling;
            Assert.AreEqual(expected, actual);
        }

        public void HandlingTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(HandlingThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Handling = "required";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = true;
            target.DispositionType = "ahfasdl";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Handling = "";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = false;
            target.DispositionType = "";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = false;
            target.DispositionType = "    ";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = false;
            target.DispositionType = "   \r\n ";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue()=="");

            target = new ContentDispositionHeaderField("hhhhh");
            value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");

            target = new ContentDispositionHeaderField("hhhhh");
            target.AddParameter(new SipParameter("bob", "hhh"));
            value = "   contENT-DispositioN  \r\n   : \r\n fred";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "fred");

            value = "     contENT-DispositioN  \t   :\t\r\n\tfred\t;handling=required";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "fred;handling=required");

            value = "   \r\n fred;handling=required";
            target.Parse(value);
            Assert.IsTrue(target.ToString() == "Content-Disposition: fred;handling=required");
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Handling = "n1232";
            expected = ";handling=n1232";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Handling = "";
            expected = "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.DispositionType = "aas";
            expected = "aas";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Handling = "n1232";
            expected = "aas;handling=n1232";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipException))]
        public void GetStringValueTest1()
        {
            ContentDispositionHeaderField target = new ContentDispositionHeaderField();

            HeaderFieldGroup<ContentDispositionHeaderField> hg = new HeaderFieldGroup<ContentDispositionHeaderField>();
            hg.Add(target);
        }

        private bool DispositionTypeThrowsError(string val)
        {
            try
                {
                ContentDispositionHeaderField target = new ContentDispositionHeaderField();
                target.DispositionType = val;
                }
            catch(SipFormatException)
                {
                return true;
                }
            return false;
        }

        private bool HandlingThrowsError(string val)
        {
            try
                {
                ContentDispositionHeaderField target = new ContentDispositionHeaderField();
                target.Handling = val;
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