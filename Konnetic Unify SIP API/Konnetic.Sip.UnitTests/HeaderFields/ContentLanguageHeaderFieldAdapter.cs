using System.Globalization;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContentLanguageHeaderFieldAdapter and is intended
    ///to contain all ContentLanguageHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContentLanguageHeaderFieldAdapter
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
            ContentLanguageHeaderField target = new ContentLanguageHeaderField();
            HeaderFieldBase expected = new ContentLanguageHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.LanguageTag = "en-GB";
            ((ContentLanguageHeaderField)expected).LanguageTag = "en-GB";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentLanguageHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentLanguageHeaderFieldConstructorTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Content-Language");
            Assert.IsTrue(target.CompactName == "Content-Language");
            Assert.IsTrue(target.GetStringValue() == "");
        }

        /// <summary>
        ///A test for ContentLanguageHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentLanguageHeaderFieldConstructorTest1()
        {
            string languageTag = string.Empty;
            ContentLanguageHeaderField target = new ContentLanguageHeaderField(languageTag);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Content-Language");
            Assert.IsTrue(target.CompactName == "Content-Language");
            Assert.IsTrue(target.GetStringValue() == (""));
            string expected = "";
            string actual;
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);

            languageTag = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            target = new ContentLanguageHeaderField(languageTag);
            expected = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ContentLanguageHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentLanguageHeaderFieldConstructorTest2()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            ContentLanguageHeaderField target = new ContentLanguageHeaderField(culture);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Content-Language");
            Assert.IsTrue(target.CompactName == "Content-Language");
            Assert.IsTrue(target.GetStringValue() == "en-GB");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField("en-GB");
            ContentLanguageHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new ContentLanguageHeaderField("EN-GB");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.LanguageTag = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new ContentLanguageHeaderField("en-GB");
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new ContentLanguageHeaderField(CultureInfo.CreateSpecificCulture("en-GB"));
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.LanguageTag = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LanguageTag
        ///</summary>
        [TestMethod]
        public void LanguageTagTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);

            target.LanguageTag = expected;
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);

            expected = "";
            target.LanguageTag = "  ";
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);

            expected = "";
            target.LanguageTag = " \r\n  ";
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);

            expected = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            target.LanguageTag = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ- ";
            actual = target.LanguageTag;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LanguageTagTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(LanguageTagThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.ToString() == "Content-Language: ");

            value = "  Content-LAnguage  \t :  \r\n  en";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "en");
            Assert.IsTrue(target.ToString() == "Content-Language: en");

            value = "  \r\n  \r\n fr-fr \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "fr-fr");
            Assert.IsTrue(target.ToString() == "Content-Language: fr-fr");

            value = "  \r\n  abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ- \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-");
            Assert.IsTrue(target.ToString() == "Content-Language: abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-");
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            ContentLanguageHeaderField target = new ContentLanguageHeaderField("fr");
            string expected = "fr";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            target.LanguageTag = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        private bool LanguageTagThrowsError(string val)
        {
            try
                {
                ContentLanguageHeaderField target = new ContentLanguageHeaderField();
                target.LanguageTag = val;
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