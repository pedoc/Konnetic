using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AcceptLanguageHeaderFieldAdapter and is intended
    ///to contain all AcceptLanguageHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AcceptLanguageHeaderFieldAdapter
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
        ///A test for AcceptLanguageHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptLanguageHeaderFieldConstructorTest()
        {
            string languageRange = string.Empty;
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField(languageRange);
            string value = "gbb;q=0.68877;bob=fred";
            target.Parse(value);
            Assert.IsTrue(target.FieldName == "Accept-Language");
            Assert.IsTrue(target.GetStringValue() == "gbb;q=0.689;bob=fred");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.ToString() == "Accept-Language: gbb;q=0.689;bob=fred");

            languageRange = "us";
            target = new AcceptLanguageHeaderField(languageRange);
            Assert.IsTrue(target.GetStringValue() == "us");
        }

        /// <summary>
        ///A test for AcceptLanguageHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptLanguageHeaderFieldConstructorTest1()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            Assert.IsTrue(target.FieldName == "Accept-Language");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.AllowMultiple == true);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            HeaderFieldBase expected = new AcceptLanguageHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.LanguageRange = "en-gb";
            target.QValue = 0.7891f;
            actual = target.Clone();
            Assert.AreEqual("en-gb;q=0.789", actual.GetStringValue());
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new AcceptLanguageHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.1f;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            AcceptLanguageHeaderField t = new AcceptLanguageHeaderField();
            t.Parse("en-gb;q=0.5");
            obj = t;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.5f;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            ((AcceptLanguageHeaderField)target).LanguageRange = "en-gb";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            QValueHeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField();
            ((AcceptLanguageHeaderField)other).LanguageRange = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField("eeee");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AcceptLanguageHeaderField("eeee");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.QValue = 1f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 1f;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.1f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 1f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            ((AcceptLanguageHeaderField)other).QValue = 1f;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.156f;
            ((QValueHeaderFieldBase)other).QValue = 0.156f;
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
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            AcceptLanguageHeaderField target1 = new AcceptLanguageHeaderField();
            target.QValue = 0.5f;
            target.AddParameter("bob", "jane");
            target1.QValue = 0.5f;
            target1.AddParameter("bob", "jane");
            ParamatizedHeaderFieldBase p = target1;
            obj = p;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.RemoveParameter("bob");
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            QValueHeaderFieldBase p1 = target1;
            p1.QValue = 0.8f;
            obj = p1;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            p1.RemoveParameter("bob");
            p1.QValue = 0.5f;
            obj = p1;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptLanguageHeaderField> hg = new HeaderFieldGroup<AcceptLanguageHeaderField>();
            hg.Add(target);
            actual = hg.Equals(target);
            expected = true;
            Assert.AreEqual(expected, actual);

            actual = hg.Equals(target1);
            expected = true;
            Assert.AreEqual(expected, actual);

            actual = target1.Equals(hg);
            expected = true;
            Assert.AreEqual(expected, actual);

            target1.QValue = 0.555f;
            actual = hg.Equals(target1);
            expected = false;
            Assert.AreEqual(expected, actual);

            hg.Add(target1);
            actual = hg.Equals(target);
            expected = false;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest4()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            AcceptLanguageHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AcceptLanguageHeaderField("hhh");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.LanguageRange = "jjj";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.LanguageRange = "hhh";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest5()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptLanguageHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Parse("ug-ad;q=0.5");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.LanguageRange = "ug-ad";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.5f;
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
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.QValue=0.5f;
             expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.LanguageRange ="";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.LanguageRange = "a";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.LanguageRange = "oooooooooooooooooooooooooooooooooooooobbbbbbbbbbbbbbbbbb";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptLanguageHeaderField> hg = new HeaderFieldGroup<AcceptLanguageHeaderField>();
            hg.Add(target);
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);

            hg[0].LanguageRange = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LanguageRangeParameterTest()
        {
            for(int i = 33; i < 127; i++)
                {
                char c = new char();
                c = (char)i;
                string val = c.ToString();
                if(!Syntax.IsAlpha(val) && val!="-" && val!="*" && val!="/")
                    {
                Assert.IsTrue(LanguageRangeThrowsError(val), "Exception Not thrown on: " + val);
                    }
                }
        }

        /// <summary>
        ///A test for LanguageRange
        ///</summary>
        [TestMethod]
        public void LanguageRangeTest()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            string expected = string.Empty;
            string actual;
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual(expected, actual);

            expected = "null";
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual(expected, actual);

            expected = " ";
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual(string.Empty, actual);

            expected = " gggg/fff ";
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual("gggg/fff", actual);

            expected = " * ";
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual("*", actual);

            expected = " */* ";
            target.LanguageRange = expected;
            actual = target.LanguageRange;
            Assert.AreEqual("*/*", actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ParamatizedHeaderFieldBase target = new AcceptLanguageHeaderField();
            string value = "gb-gb;q=0.6;bob=fred";
            target.Parse(value);
            Assert.IsTrue(target.FieldName == "Accept-Language");
            Assert.IsTrue(target.GetStringValue() == "gb-gb;q=0.6;bob=fred");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.GetStringValue() == "gb-gb;q=0.6;bob=fred");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest1()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            AcceptLanguageHeaderField expected = new AcceptLanguageHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.AreEqual(expected, target);

            value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.LanguageRange == "");
            Assert.IsTrue(target.HasParameters == false);

            value = "  accEpt-Language  \t      :  lllllllllll*\t";
            target.Parse(value);
            Assert.IsTrue(target.LanguageRange == "lllllllllll*");
            Assert.IsTrue(target.HasParameters == false);

            value = "  accEpt-Language        : \taaa;q=0.5";
            target.Parse(value);
            Assert.IsTrue(target.LanguageRange == "aaa");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.QValue == 0.5f);

            value = "  accEpt-Language   : ;q=0.5";
            target = new AcceptLanguageHeaderField();
            target.Parse(value);
            Assert.IsTrue(target.LanguageRange == "");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.QValue == 0.5f);

            HeaderFieldGroup<AcceptLanguageHeaderField> hg = new HeaderFieldGroup<AcceptLanguageHeaderField>();
            hg.Parse(value);
            Assert.IsTrue(hg.GetStringValue() == ";q=0.5");
        }

        /// <summary>
        ///A test for QValue
        ///</summary>
        [TestMethod]
        public void QValueTest()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            float? expected = 0F;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);

            expected = 1F;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void QValueTest1()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            float? expected = -0.1F;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void QValueTest2()
        {
            QValueHeaderFieldBase target = new AcceptLanguageHeaderField();
            float? expected = 1.1F;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptLanguageHeaderField("en-gb");
            expected = "en-gb";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.LanguageRange = "us";
            expected = "us";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptLanguageHeaderField> hfg = new HeaderFieldGroup<AcceptLanguageHeaderField>();

            hfg.Add(target);
            hfg.Add(new AcceptLanguageHeaderField("en-gb"));

            expected = "us, en-gb";
            actual = hfg.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.QValue = 0.6f;

            expected = "us;q=0.6, en-gb";
            actual = hfg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            AcceptLanguageHeaderField headerField = new AcceptLanguageHeaderField();
            string expected = "Accept-Language: ";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField.LanguageRange = "h-h";
            expected = "Accept-Language: h-h";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "Accept-Language: h-h";
            AcceptLanguageHeaderField expected = new AcceptLanguageHeaderField("h-h");
            AcceptLanguageHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);

            value = "   Accept-Language  :      h-h;q=0.7";
            expected = new AcceptLanguageHeaderField("h-h");
            expected.QValue = 0.7f;
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        private bool LanguageRangeThrowsError(string val)
        {
            try
                {
                AcceptLanguageHeaderField target = new AcceptLanguageHeaderField();
                target.LanguageRange = val;
                }
            catch(SipFormatException  )
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