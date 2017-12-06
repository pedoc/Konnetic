using Konnetic.Sip.Headers;

using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ParamatizedHeaderFieldAdapter and is intended
    ///to contain all ParamatizedHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ParamatizedHeaderFieldAdapter
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
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            ParamatizedHeaderFieldBase_Accessor other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = CreateHeaderFieldWithParameters_Accessor();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.InternalAddGenericParameter("Param", "Value");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            SipParameter sp = new SipParameter("PAram","Value");
            other.InternalAddGenericParameter(sp);
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.RemoveParameter("PAram");
            sp = new SipParameter("PAram", "VAlue");
            other.InternalAddGenericParameter(sp);
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);


            other.RemoveParameter("PAram");
            sp = new SipParameter("PAram", "VAlue");
            sp.CaseSensitiveComparison = true;
            other.InternalAddGenericParameter(sp);
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);


            other.RemoveParameter("PAram");
            sp = new SipParameter("Param", "Value");
            sp.CaseSensitiveComparison = true;
            other.InternalAddGenericParameter(sp);
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.InternalAddGenericParameter("Param1", "Value1");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.InternalAddGenericParameter("param1", "value1");
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FieldValue
        ///</summary>
        [TestMethod]
        public void FieldValueTest()
        {
            ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter expected1 = new SipParameter("Param", "Val");
            string expected = "\"gg \r\n jk <sip:Bob@bob.com>";
            string actual;
            target.Parse(expected);
            actual = target.GetStringValue();
            expected = "\"gg jk\" <sip:Bob@bob.com>";
            Assert.AreEqual(expected, actual, "Test the Constructor");

            expected = "\"gg\r\njk <sip:Bob@bob.com>";
            target.Parse(expected);
            actual = target.GetStringValue();
            expected = "\"ggjk\" <sip:Bob@bob.com>";
            Assert.AreEqual(expected, actual, "Test the Constructor");

            expected = "gg\r\njk\" <sip:Bob@bob.com>";
            target.Parse(expected);
            actual = target.GetStringValue();
            expected = "\"ggjk\" <sip:Bob@bob.com>";
            Assert.AreEqual(expected, actual, "Test the Constructor");
        }

        /// <summary>
        ///A test for HasParameters
        ///</summary>
        [TestMethod]
        public void HasParametersTest()
        {
            ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            bool expected = false;
            bool actual;
            SipParameter param = new SipParameter("Param", "Val");
            actual = target.HasParameters;
            Assert.AreEqual(expected, actual, "Test the Constructor");
            target.InternalAddGenericParameter(param);
            expected = true;
            actual = target.HasParameters;
            Assert.AreEqual(expected, actual, "After an assignement");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void IsValidTest()
        {
		ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parameters
        ///</summary>
        [TestMethod]
        public void ParametersTest()
        {
		    ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter param = new SipParameter();
            ReadOnlyCollection<SipParameter> expected = new ReadOnlyCollection<SipParameter>(new SipParameterCollection());
            ReadOnlyCollection<SipParameter> actual;
            actual = target.InternalGenericParameters;
            Assert.AreEqual(expected.GetType(), actual.GetType(), "Test the Constructor");
            SipParameter expected1 = new SipParameter("Param", "Val");
            param = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(param);
            actual = target.InternalGenericParameters;
            Assert.AreEqual(expected1, actual[0], "Attempt to set to a valid parameter"); 
        }

        [TestMethod]
        //[ExpectedException(typeof(SipDuplicateItemException))]
        public void ParametersTest1()
        {
		    ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter param = new SipParameter();
            SipParameterCollection expected = new SipParameterCollection();

            param = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(param);
            param = new SipParameter("Param", "Value");
            target.InternalAddGenericParameter(param);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
        ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            string value = string.Empty;
            target.Parse(value);
            string actual;
            actual = target.GetStringValue();
            Assert.IsTrue(actual=="");

            value = "\t;Param\r\n =\tValue\t";
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.IsTrue(actual == ";param=Value");

            value = "Param\r\n =\tValue";
            target.Parse(value); 
            Assert.IsTrue(target.InternalGenericParameters.Count == 0);

            value = ";Param=Value\t;\tParam1";
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.IsTrue(actual == ";param=Value;param1");

            value = ";Param=Value;\r\nParam1 ;Param2=\"my value\"";
            target.Parse(value);
            actual = target.GetStringValue();
            Assert.IsTrue(actual == ";param=Value;param1;param2=\"my value\"");
            Assert.IsTrue(target.InternalGenericParameters[0].Name == "param");
            Assert.IsTrue(target.InternalGenericParameters[0].Value == "Value");
            Assert.IsTrue(target.InternalGenericParameters[0].ValuelessParameter == false);
            Assert.IsTrue(target.InternalGenericParameters[0].CaseSensitiveComparison == false);
            Assert.IsTrue(target.InternalGenericParameters[1].Name == "param1");
            Assert.IsTrue(target.InternalGenericParameters[1].Value == "");
            Assert.IsTrue(target.InternalGenericParameters[1].ValuelessParameter == true);
            Assert.IsTrue(target.InternalGenericParameters[1].CaseSensitiveComparison == false);
            Assert.IsTrue(target.InternalGenericParameters[2].Name == "param2");
            Assert.IsTrue(target.InternalGenericParameters[2].Value == "\"my value\"");
            Assert.IsTrue(target.InternalGenericParameters[2].ValuelessParameter == false);
            Assert.IsTrue(target.InternalGenericParameters[2].CaseSensitiveComparison == true);
        }

        /// <summary>
        ///A test for ToBytes
        ///</summary>
        [TestMethod]
        public void ToBytesTest()
        {
		ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();

            SipParameter expected1 = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(expected1);
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("To: <sip:Bob@bob.com>;param=Val".ToCharArray());

            byte[] actual;
            actual = target.GetBytes();
            Assert.AreEqual(new string(System.Text.UTF8Encoding.UTF8.GetChars(expected)), new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)));
        }

        /// <summary>
        ///A test for ToChars
        ///</summary>
        [TestMethod]
        public void ToCharsTest()
        {
		ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter expected1 = new SipParameter("param", "Val");
            target.InternalAddGenericParameter(expected1);
            char[] expected = "To: <sip:Bob@bob.com>;param=Val".ToCharArray();
            char[] actual;
            actual = target.GetChars();
            Assert.AreEqual(new string(expected), new string(actual));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest1()
        {
		ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter expected1 = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(expected1);
            string expected = "To: <sip:Bob@bob.com>";
            string actual;
            actual = target.ToString();
            Assert.IsTrue(actual.Contains("param=Val"), "Test the Constructor");
            Assert.IsTrue(actual.StartsWith(expected), "Test the Constructor2");

            target.ClearParameters();
            target.InternalAddGenericParameter("ParamName", "ParamVal");
            expected = "To: <sip:Bob@bob.com>;paramname=ParamVal";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Just a param");

            target.FieldName = "Name1";
            target.Parse("sip:Value1@bob.com");
            target.ClearParameters();
            expected = "Name1: <sip:Value1@bob.com>";
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "No param but name + value");

            target.InternalAddGenericParameter("ParamName", "ParamVal"); 
            actual = target.ToString();
            expected = "Name1: <sip:Value1@bob.com>;paramname=ParamVal";
            Assert.AreEqual(expected, actual, "Empty value");

            expected = "Name1: <sip:Value1@bob.com>;paramname=ParamVal1";
            target.InternalAddGenericParameter("ParamName", "ParamVal1"); 
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Param + value but no name");
        }

        [TestMethod]
        //[ExpectedException(typeof(SipDuplicateItemException))]
        public void GetStringValue2()
        {
        ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();

            SipParameter expected1 = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(expected1);
            expected1 = new SipParameter("param", "Val");
            target.InternalAddGenericParameter(expected1);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
		ParamatizedHeaderFieldBase_Accessor target = CreateHeaderFieldWithParameters_Accessor();
            SipParameter expected1 = new SipParameter("Param", "Val");
            target.InternalAddGenericParameter(expected1);
            string actual;
            actual = target.GetStringValue();
            Assert.IsTrue(actual.Contains("param=Val"), "Test the Constructor");
            Assert.IsTrue(actual.StartsWith("<sip:Bob@bob.com>"), "Test the Constructor2");
        }

        internal virtual ParamatizedHeaderFieldBase CreateHeaderFieldWithParameters()
        {
            ParamatizedHeaderFieldBase target = new ToHeaderField(new SipUri("sip:Bob@bob.com"));
            return target;
        }

        internal virtual ParamatizedHeaderFieldBase CreateHeaderFieldWithParameters1()
        {
            ParamatizedHeaderFieldBase target = new ContentDispositionHeaderField();
            return target;
        }

        internal virtual ParamatizedHeaderFieldBase_Accessor CreateHeaderFieldWithParameters_Accessor()
        {
            PrivateObject p = new PrivateObject(new ToHeaderField(new SipUri("sip:Bob@bob.com")));
			ParamatizedHeaderFieldBase_Accessor target = new ParamatizedHeaderFieldBase_Accessor(p);
            return target;
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