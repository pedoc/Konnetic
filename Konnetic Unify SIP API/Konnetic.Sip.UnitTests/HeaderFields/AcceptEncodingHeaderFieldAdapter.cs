using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AcceptEncodingHeaderFieldAdapter and is intended
    ///to contain all AcceptEncodingHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AcceptEncodingHeaderFieldAdapter
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
        ///A test for AcceptEncodingHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptEncodingHeaderFieldConstructorTest()
        {
            string coding = "Gzip";
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField(coding);
            target.QValue = 0.5f;
            Assert.IsTrue(target.GetStringValue() == "Gzip;q=0.5");
            Assert.IsTrue(target.QValue == 0.5);
        }

        /// <summary>
        ///A test for AcceptEncodingHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptEncodingHeaderFieldConstructorTest1()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Accept-Encoding");
			Assert.IsTrue(target.GetStringValue() == "identity");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.QValue == null);
            Assert.IsTrue(target.CompactName == "Accept-Encoding");
        }

        /// <summary>
        ///A test for AcceptEncodingHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AcceptEncodingHeaderFieldConstructorTest2()
        { 
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
			Assert.IsTrue(target.Encoding == "identity"); 
              target = new AcceptEncodingHeaderField();
            Assert.IsTrue(target.Encoding == "identity");
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            HeaderFieldBase expected = new AcceptEncodingHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target = new AcceptEncodingHeaderField("gzip");
            expected = new AcceptEncodingHeaderField("gzip");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.QValue=0.9f;
            expected = new AcceptEncodingHeaderField("gzip");
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((AcceptEncodingHeaderField)expected).QValue = 0.9f;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptEncodingHeaderField> hg = new HeaderFieldGroup<AcceptEncodingHeaderField>();
            hg.Add(target);
            actual = hg.Clone();
            Assert.AreEqual(hg, actual);
        }

        [TestMethod]
        public void CodingParameterTest()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(CodingThrowsError(val), "Exception Not thrown on: " + val);
                }
        }


        [TestMethod]
        public void GetHashCodeTest()
            {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            target.Encoding = "123";
            int actual = target.GetHashCode();
            int actual2 = target.GetHashCode();
            Assert.AreEqual(actual, actual2);
            
            AcceptEncodingHeaderField target1 = new AcceptEncodingHeaderField();
            target1.Encoding = "123";
            actual2 = target1.GetHashCode();
            Assert.AreEqual(actual, actual2);
             
            target1.Encoding = "1234";
            actual2 = target1.GetHashCode();
            Assert.AreNotEqual(actual, actual2);
            
            target.Encoding = "1234";
            target1.Encoding = "1234";
            actual = target.GetHashCode();
            actual2 = target1.GetHashCode();
            Assert.AreEqual(actual, actual2);

            target.QValue = 0.1f;
            target1.QValue = 0.2f;
            actual = target.GetHashCode();
            actual2 = target1.GetHashCode();
            Assert.AreEqual(actual, actual2);

            target.QValue = 0.1f;
            target1.QValue = 0.1f;
            actual = target.GetHashCode();
            actual2 = target1.GetHashCode();
            Assert.AreEqual(actual, actual2);

            target.AddParameter("1", "2");
            target1.AddParameter("1", "2");
            actual = target.GetHashCode();
            actual2 = target1.GetHashCode();
            Assert.AreEqual(actual, actual2);

            }
        /// <summary>
        ///A test for Coding
        ///</summary>
        [TestMethod]
        public void CodingTest()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            string expected = string.Empty;
            string actual;
            target.Encoding = expected;
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "gzip";
            target.Encoding = expected;
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void CodingTest1()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            string expected = null;
            string actual;
            target.Encoding = expected;
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new AcceptEncodingHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = new AcceptEncodingHeaderField("gzip");
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target = new AcceptEncodingHeaderField("Gzip");
            HeaderFieldBase b = (HeaderFieldBase)target;
            obj = b;
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
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            AcceptEncodingHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AcceptEncodingHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.QValue=0.8f;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.QValue = 0.8f;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Encoding = "gzip";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Encoding = "GZip";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptEncodingHeaderField> hg = new HeaderFieldGroup<AcceptEncodingHeaderField>();
            hg.Add(target);
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            hg.Add(target);
            expected = true;
            actual = other.Equals(target);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = (HeaderFieldBase)null;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = (HeaderFieldBase)target;
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
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Encoding="Gzip";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Encoding = "";
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            expected = false;
            target.Encoding = "";
            target.QValue = 1.0f;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptEncodingHeaderField> hg = new HeaderFieldGroup<AcceptEncodingHeaderField>();
            hg.Add(target);
            expected = false;
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);

            hg[0].Encoding = "Gzip";
            expected = true;
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest5()
        //    {
        //    AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
        //    string expected = "gzip";
        //    string actual;
        //    string value = string.Empty;
        //    value = "  aaa  Accept-encoding    :  gzip   ";
        //    target.Parse(value);
        //    actual = target.Coding;
        //    Assert.AreEqual(expected, actual);
        //    }
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            string expected = string.Empty;
            string actual;
            string value = string.Empty;
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = string.Empty;
            value = "    Accept-Encoding \t   :     ";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "343";
            value = "    Accept-Encoding  \t  :    343\t ";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            value = "    Accept-Encoding    : 	\t   343 hhh";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "u";
            value = "    accept-Encoding    :    u	";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "u";
            value = "accePt-Encoding  	  :    u";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "u";
            value = "   u";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "u";
            value = "   u;q=0.9";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            expected = "u;q=0.9";
            value = "   u;q=0.9   ";
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptEncodingHeaderField> hg = new HeaderFieldGroup<AcceptEncodingHeaderField>();
            hg.Parse(value);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "u;q=0.9, b;q=0.8";
            hg.Parse("   u;q=0.9, b;q=0.8  ");
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipParseException))]
        public void ParseTest1()
        {
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            string expected = string.Empty;
            string actual;
            string value = string.Empty;
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);

            value = "    Accept-Encoding    :    34ú3 hhh";
            target.Parse(value);
            actual = target.Encoding;
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest2()
        //{
        //    AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
        //    string expected = string.Empty;
        //    string actual;
        //    string value = string.Empty;
        //    value = "    Accept-Encoding2    :  gzip   ";
        //    target.Parse(value);
        //    actual = target.Coding;
        //    Assert.AreEqual(expected, actual);
        //}
        //[TestMethod()]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest3()
        //    {
        //    AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
        //    string expected = "444";
        //    string actual;
        //    string value = string.Empty;
        //    value = "    Accept Encoding    :   444";
        //    target.Parse(value);
        //    actual = target.Coding;
        //    Assert.AreEqual(expected, actual);
        //    }
        //[TestMethod]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest4()
        //{
        //    AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
        //    string expected = string.Empty;
        //    string actual;
        //    string value = string.Empty;
        //    value = "    Accept_Encoding 	   :   gzip    ";
        //    target.Parse(value);
        //    actual = target.Coding;
        //    Assert.AreEqual(expected, actual);
        //}
        /// <summary>
        ///A test for QValue
        ///</summary>
        [TestMethod]
        public void QValueTest()
        {
            QValueHeaderFieldBase target = new AcceptEncodingHeaderField();
            float? expected = 0.4f;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);

            expected = 1.0f;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(SipOutOfRangeException))]
        public void QValueTest2()
        {
            QValueHeaderFieldBase target = new AcceptEncodingHeaderField();
            float? expected = 1.1f;
            float? actual;
            target.QValue = expected;
            actual = target.QValue;
            Assert.AreEqual(expected, actual);
        }

		[ExpectedException(typeof(SipOutOfRangeException))]
        public void QValueTest3()
        {
            QValueHeaderFieldBase target = new AcceptEncodingHeaderField();
            float? expected = -0.1f;
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
            AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
            string expected = "identity";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "gzip";
            target.Encoding = expected;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AcceptEncodingHeaderField> hg = new HeaderFieldGroup<AcceptEncodingHeaderField>();
            hg.Add(target);
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            hg.Add(new AcceptEncodingHeaderField("nnn"));
            expected = "gzip, nnn";
            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            AcceptEncodingHeaderField headerField = new AcceptEncodingHeaderField();
            string expected = "Accept-Encoding: identity";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new AcceptEncodingHeaderField("gzip");
            expected = "Accept-Encoding: gzip";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = string.Empty;
            AcceptEncodingHeaderField expected = new AcceptEncodingHeaderField();
            AcceptEncodingHeaderField actual = new AcceptEncodingHeaderField();
            actual = value;
			Assert.AreNotEqual(expected.Encoding, actual.Encoding);
			Assert.IsTrue(expected.Encoding == "identity");
			Assert.IsTrue(actual.Encoding == "");

            value = "accePt-Encoding    :  	  u";
            expected = new AcceptEncodingHeaderField("u");
            actual = value;
            Assert.AreEqual(expected.GetStringValue(), actual.GetStringValue());
        }

        private bool CodingThrowsError(string val)
        {
            try
                {
                AcceptEncodingHeaderField target = new AcceptEncodingHeaderField();
                target.Encoding = val;
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