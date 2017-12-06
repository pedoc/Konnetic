using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AcceptHeaderFieldAdapter and is intended
    ///to contain all AcceptHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AcceptHeaderFieldAdapter
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
        ///A test for AcceptHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptHeaderFieldConstructorTest()
        {
            string mediaType = "*";
            string mediaSubType = "*";
            AcceptHeaderField target = new AcceptHeaderField(mediaType, mediaSubType);
            string expected = "Accept: */*";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "*";
            actual = target.MediaType;
            Assert.AreEqual(expected, actual);

            mediaType = "";
            mediaSubType = "aa";
            target = new AcceptHeaderField(mediaType, mediaSubType);
             expected = "Accept: */aa";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "aa";
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AcceptHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptHeaderFieldConstructorTest1()
        {
            AcceptHeaderField target = new AcceptHeaderField();
            string expected = "Accept: application/sdp";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AcceptHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptHeaderFieldConstructorTest2()
        {
            MediaType mediaType = MediaType.All;
            string mediaSubType = string.Empty;
            AcceptHeaderField target = new AcceptHeaderField(mediaType, mediaSubType);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Accept");
            Assert.IsTrue(target.CompactName == "Accept");
            Assert.IsTrue(target.GetStringValue() == "*/*");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.QValue == null);

            string expected = "Accept: */*";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);

            mediaSubType = "bob";
            target = new AcceptHeaderField(mediaType, mediaSubType);
            expected = "*/bob";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void AcceptHeaderFieldConstructorTest3()
        {
            AcceptHeaderField target = new AcceptHeaderField(null, null);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AcceptHeaderField target = new AcceptHeaderField();
            HeaderFieldBase expected = new AcceptHeaderField(MediaType.Application, "sdp");
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            expected = new AcceptHeaderField(MediaType.Application, "sdp");
            ((AcceptHeaderField)expected).AddParameter(new SipParameter("level", "1"));
            target.AddParameter(new SipParameter("level", "1"));
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            //target.MediaParameters.Add("bob", "fred");
            //((AcceptHeaderField)expected).MediaParameters.Add(new SipParameter("bob", "fred"));
            //actual = target.Clone();
            //Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptHeaderField> hg = new HeaderFieldGroup<AcceptHeaderField>();
            hg.Add(target);
            actual = hg.Clone();
            Assert.AreEqual(hg, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipFormatException))]
        public void ConstructorTest3()
        {
            AcceptHeaderField target = new AcceptHeaderField("  sss  ss", "");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ConstructorTest4()
        {
            AcceptHeaderField headerField = new AcceptHeaderField("",null);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AcceptHeaderField target = new AcceptHeaderField();
            AcceptHeaderField other = new AcceptHeaderField();
            bool expected = true;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            other = new AcceptHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            other = new AcceptHeaderField();
            target.AddParameter(new SipParameter("LEvel", "1"));
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.AddParameter(new SipParameter("level=1"));
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            //target.MediaParameters.Add(new SipParameter("abc=123"));
            //expected = false;
            //actual = target.Equals(other);
            //Assert.AreEqual(expected, actual);

            //other.MediaParameters.Add(new SipParameter("abc=123"));
            //expected = true;
            //actual = target.Equals(other);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            AcceptHeaderField target1 = new AcceptHeaderField();
            AcceptHeaderField target = new AcceptHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            other = new AcceptHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            AcceptHeaderField target = new AcceptHeaderField();
            object obj = (object)(new AcceptHeaderField());
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            AcceptHeaderField target1 = new AcceptHeaderField();
            obj = (object)target1;
            target.MediaType = "APplication";
            target.MediaSubType = "SDp";
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.MediaType = "application1";
            target.MediaSubType = "sdp";
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target.MediaType = "application";
            target.MediaSubType = "*";
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target1 = new AcceptHeaderField();
            target1.QValue = 0.3434444f;
            target.QValue = 0.3434444f;
            expected = true;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);

            target1.QValue = 0.3444444f;
            target.QValue = 0.3434444f;
            expected = false;
            actual = target.Equals(target1);
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptHeaderField> hg = new HeaderFieldGroup<AcceptHeaderField>();
            hg.Add(target);
            actual = hg.Equals(target1);
            Assert.AreEqual(expected, actual);

            hg[0].QValue = 0.3444444f;
            expected = true;
            actual = hg.Equals(target1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField(MediaType.Message,"777");
             expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.MediaType="";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.MediaType = "123";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.MediaSubType = "9999-9999";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.MediaSubType = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.MediaType = "";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            AcceptHeaderField target1 = (AcceptHeaderField)target;
            HeaderFieldGroup<AcceptHeaderField> hg = new HeaderFieldGroup<AcceptHeaderField>();
            hg.Add(target1);
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MediaSubTypeParameterTest()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(MediaSubTypeThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for MediaSubType
        ///</summary>
        [TestMethod]
        public void MediaSubTypeTest()
        {
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
            string expected = string.Empty;
            string actual;
            target.MediaSubType = expected;
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);

            expected = "*";
            target.MediaSubType = expected;
            expected = "*";
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);

            expected = "*";
            target.MediaSubType = expected;
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);

            expected = "null";
            target.MediaSubType = expected;
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);

            expected = "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii";
            target.MediaSubType = expected;
            actual = target.MediaSubType;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void MediaSubTypeTest1()
        {
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
            string expected = null;
            target.MediaSubType = expected;
        }

        [TestMethod]
        public void MediaTypeParameterTest()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(MediaTypeThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for MediaType
        ///</summary>
        [TestMethod]
        public void MediaTypeTest()
        {
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
            string expected = string.Empty;
            string actual;
            target.MediaType = expected;
            actual = target.MediaType;
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            expected = "application";
            actual = target.MediaType;
            Assert.AreEqual(expected, actual);

            expected = "application1";
            target.MediaType = expected;
            actual = target.MediaType;
            Assert.AreEqual(expected, actual);

            expected = "All";
            target.MediaType = expected;
            actual = target.MediaType;
            Assert.AreEqual("All", actual);
            
            expected = "*";
            target.MediaType = expected;
            actual = target.MediaType;
            Assert.AreEqual("*", actual);

            expected = "";
            target.MediaType = expected;
            actual = target.MediaType;
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void MediaTypeTest1()
        {
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
            string expected = null;
            target.MediaType = expected;
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AcceptHeaderField target = new AcceptHeaderField("", "");
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");

            target = new AcceptHeaderField("", "");
            value = "Accept \t : */bob\t";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "*/bob");

            target = new AcceptHeaderField(MediaType.All, "*");
            value = " Accept:    \t*/*\t";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "*/*");

            target = new AcceptHeaderField(MediaType.All, "*");
            value = " */*";
            target.Parse(value);
            Assert.IsTrue(target.ToString() == "Accept: */*");

            target = new AcceptHeaderField(MediaType.All, "*");
            value = " text/*";
            target.Parse(value);
            Assert.IsTrue(target.ToString() == "Accept: text/*");

            HeaderFieldGroup<AcceptHeaderField> hg = new HeaderFieldGroup<AcceptHeaderField>();
            hg.Parse(value);
            Assert.IsTrue(hg.ToString() == "Accept: text/*");
            hg.Parse("text/*, */*");
            Assert.IsTrue(hg.ToString() == "Accept: text/*, */*");
        }

        //[TestMethod]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest1()
        //{
        //    AcceptHeaderField target = new AcceptHeaderField();
        //    string value = "  accept*: */*";
        //    target.Parse(value);
        //    Assert.IsTrue(target.GetStringValue() == "Accept:");
        //}
        /// <summary>
        ///A test for QValue
        ///</summary>
        [TestMethod]
        public void QValueTest()
        {
            AcceptHeaderField target = new AcceptHeaderField();
            float? expected = 0.111111111111F;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.IsTrue(System.Math.Abs((float)expected - (float)actual) < 0.001);
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void QValueTest1()
        {
            AcceptHeaderField target = new AcceptHeaderField();
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
            AcceptHeaderField target = new AcceptHeaderField();
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
            MediaTypeHeaderFieldBase target = new AcceptHeaderField();
			string expected = "application/sdp";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            expected = "application/sdp";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target.MediaType = "audio";
            expected = "audio/sdp";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target.MediaType = "";
            expected = "*/sdp";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target.MediaType = "text1";
            expected = "text1/sdp";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target.MediaSubType = "";
            expected = "application/*";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new AcceptHeaderField();
            target.MediaSubType = "123";
            expected = "application/123";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            AcceptHeaderField target1 = (AcceptHeaderField)target;
            HeaderFieldGroup<AcceptHeaderField> hg = new HeaderFieldGroup<AcceptHeaderField>();
            hg.Add(target1);
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);

            hg.Add(new AcceptHeaderField("aaa","bbb"));
            actual = hg.GetStringValue();
            expected = "application/123, aaa/bbb";
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            AcceptHeaderField headerField = new AcceptHeaderField("","");
            string expected = "Accept: ";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new AcceptHeaderField("*", "*");
            expected = "Accept: */*";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new AcceptHeaderField("*", "");
            expected = "Accept: */*";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new AcceptHeaderField();
            expected = "Accept: application/sdp";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new AcceptHeaderField();
            headerField.AddParameter(new SipParameter("level=1"));
            expected = "Accept: application/sdp;level=1";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "Accept: */bob";
            AcceptHeaderField expected = new AcceptHeaderField(MediaType.All,"bob");
            AcceptHeaderField actual = new AcceptHeaderField();
            actual = value;

            value = "audio/123";
            expected = new AcceptHeaderField(MediaType.Audio, "123");
            actual = value;
            Assert.AreEqual(expected, actual);

            value = "abs/123";
            expected = new AcceptHeaderField("abs", "123");
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        private bool MediaSubTypeThrowsError(string val)
        {
            try
                {
                AcceptHeaderField target = new AcceptHeaderField();
                target.MediaSubType = val;
                }
            catch(SipFormatException  )
                {
                return true;
                }
            return false;
        }

        private bool MediaTypeThrowsError(string val)
        {
            try
                {
                AcceptHeaderField target = new AcceptHeaderField();
                target.MediaType = val;
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