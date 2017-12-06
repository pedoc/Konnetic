using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for FromHeaderFieldAdapter and is intended
    ///to contain all FromHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class FromHeaderFieldAdapter
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
            FromHeaderField target = new FromHeaderField();
            HeaderFieldBase expected = new FromHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);
            Assert.IsTrue(target.Tag != string.Empty);
            Assert.IsTrue(((FromHeaderField)expected).Tag != string.Empty);

            ((FromHeaderField)actual).Tag = ((FromHeaderField)expected).Tag;
            Assert.AreEqual(expected, actual);


            target.DisplayName = "bob";
            ((FromHeaderField)target).Tag = ((FromHeaderField)expected).Tag;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(((FromHeaderField)actual).DisplayName == "bob");

            ((FromHeaderField)expected).DisplayName = "bob";
            actual = target.Clone(); 
            Assert.AreEqual(expected.GetStringValue(), actual.GetStringValue());

            target.RemainHidden = true;
            actual = target.Clone();
            Assert.IsTrue(((FromHeaderField)actual).RemainHidden==true);

            string t = FromHeaderField.NewTag();
            target.Tag = t;
            actual = target.Clone();
            Assert.IsTrue(((FromHeaderField)actual).Tag == t);
        }

        /// <summary>
        ///A test for DisplayName
        ///</summary>
        [TestMethod]
        public void DisplayNameTest1()
        {
            AddressedHeaderFieldBase target = new FromHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.DisplayName;
            Assert.AreEqual(expected, actual);

            target.DisplayName = expected;
            Assert.AreEqual(expected, actual);

            expected = Common.QUOTEDSTRING;
            target.DisplayName = expected;
            expected = Common.QUOTEDSTRINGRESULT;
            actual = target.DisplayName;
            Assert.AreEqual(expected, actual);

            FromHeaderField target1 = new FromHeaderField();
            expected = string.Empty;
            target1.DisplayName = expected;
            actual = target1.DisplayName;
            Assert.AreEqual(expected, actual);

            target1.DisplayName = expected;
            Assert.AreEqual(expected, actual);

            expected = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz0123456789-.!%*_+`'~";
            target1.DisplayName = expected;
            actual = target1.DisplayName;
            Assert.AreEqual(expected, actual);
        }

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            HeaderFieldBase target = new FromHeaderField();
            HeaderFieldBase headerField = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "headerField is null");

            target.Parse("sips:123@bob.com");
            headerField = new FromHeaderField();
            expected = false;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Different Values");

            headerField.Parse("<sIp:123@bob.com>");

            expected = true;
            actual = target.Equals(headerField);
            Assert.AreNotEqual(expected, actual, "Same apart from tag");
            Assert.IsTrue(headerField.ToString().StartsWith("From:"), "Same value");

            ((FromHeaderField)headerField).RecreateTag();
            Assert.IsTrue(headerField.ToString().StartsWith("From: <sip:123@bob.com"), "Same value");

            target = new FromHeaderField("sip:Fred@bob.com;param=name");
            headerField = new FromHeaderField("sip:Fred@bob.com;param=name");
            expected = true;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Same2");

            ((FromHeaderField)target).Tag = FromHeaderField.NewTag();
            Assert.IsTrue(target.ToString().StartsWith("From: <sip:Fred@bob.com;param=name>"), "Same2 value");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            FromHeaderField target = new FromHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "obj is null");

            obj = new FromHeaderField();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreNotEqual(expected, actual);

            target.Tag = ((FromHeaderField)obj).Tag;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same references");

            obj = target.Clone();
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Clones");

            target = new FromHeaderField("sip:fred@bob.com");
            obj = new FromHeaderField("sip:fred@bob.com");
            target.RecreateTag();
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same Objects but differenet tags");
            ((FromHeaderField)obj).RecreateTag();
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same Objects but differenet tags2");

            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Equalivalent Objects");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest2()
        {
            FromHeaderField target = new FromHeaderField();
            FromHeaderField fromHeaderField = null;
            bool expected = false;
            bool actual;
            target = new FromHeaderField("sip:Fred@bob.com;param=name");
            fromHeaderField = new FromHeaderField("sip:Fred@bob.com;param=name");
            expected = true;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Same tag");
            Assert.AreEqual(target.Uri, fromHeaderField.Uri, "Same Uris");
            target.Tag = FromHeaderField.NewTag();
            Assert.IsTrue(target.ToString().StartsWith("From: <sip:Fred@bob.com;param=name>;tag="), "Same Output");

            target.RecreateTag();
            expected = false;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Different tags");

            fromHeaderField.RecreateTag();
            expected = false;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Different tags2");

            expected = true;
            actual = target.Uri.Parameters.Equals(fromHeaderField.Uri.Parameters);
            Assert.AreEqual(expected, actual, "Same Uri Parameters");

            fromHeaderField.ClearParameters();
            expected = false;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "The Same except params");

            
            fromHeaderField.Tag=target.Tag;
            expected = true;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "The Same");

            fromHeaderField.AddParameter(new SipParameter("param1=name"));
            expected = false;
            actual = target.GenericParameters.Equals(fromHeaderField.GenericParameters);
            Assert.AreEqual(expected, actual, "Not the Same2");

            fromHeaderField.ClearParameters();
            fromHeaderField.AddParameter(new SipParameter("param=name1"));
            actual = target.GenericParameters.Equals(fromHeaderField.GenericParameters);
            Assert.AreEqual(expected, actual, "Not the Same3");
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Not the Same3a");

            fromHeaderField.ClearParameters();
            fromHeaderField.AddParameter(new SipParameter("paramname1"));
            actual = target.GenericParameters.Equals(fromHeaderField.GenericParameters);
            Assert.AreEqual(expected, actual, "Not the Same4");
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Not the Same4a");

            fromHeaderField.AddParameter(new SipParameter("param1=name"));
            actual = target.GenericParameters.Equals(fromHeaderField.GenericParameters);
            Assert.AreEqual(expected, actual, "Not the Same5");
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Not the Same5a");

            fromHeaderField.ClearParameters();
            fromHeaderField.Tag=target.Tag;
            expected = true;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "The Same");

            fromHeaderField = target;
            target.DisplayName = "Fredrick";
            expected = true;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Same2");
            Assert.IsTrue(target.ToString().StartsWith(@"From: ""Fredrick"" <sip:Fred@bob.com;param=name>"), "Same2 Output");

            target.RemainHidden = true;
            expected = true;
            actual = target.Equals(fromHeaderField);
            Assert.AreEqual(expected, actual, "Same3");
            Assert.IsTrue(target.ToString().StartsWith(@"From: ""Anonymous"""), "Anonymous Start");
        }

        /// <summary>
        ///A test for FieldName
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void FieldNameTest()
        {
            HeaderFieldBase_Accessor target = new FromHeaderField_Accessor();
            string expected = "From";
            string actual;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the constructor");
            expected = "From";
            target.FieldName = expected;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the assignement");
        }

        /// <summary>
        ///A test for FromHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void FromHeaderFieldConstructorTest()
        {
            string From = "sip:fred@bob.com";
            FromHeaderField target = new FromHeaderField(From);
            Assert.AreEqual(From, target.Uri.OriginalString);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "From");
            Assert.IsTrue(target.CompactName == "f");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.GetStringValue() == "<sip:fred@bob.com>");
        }

        /// <summary>
        ///A test for FromHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void FromHeaderFieldConstructorTest1()
        {
            FromHeaderField target = new FromHeaderField();
            string expected = string.Empty;
            Assert.AreEqual("From", target.FieldName,"FieldName");
            Assert.IsTrue(target.GetStringValue().Length != 0, "Init FieldValue");
            Assert.AreEqual(true, target.HasParameters, "HasParameters");

            Assert.AreEqual(false, target.AllowMultiple, "AllowDuplicates");

            target = new FromHeaderField();
            Assert.AreEqual(true, target.HasParameters, "HasNoParameters");
            Assert.IsTrue(target.GetStringValue().Length != 0, "Init FieldValue with nothing");
             
        }

        /// <summary>
        ///A test for New
        ///</summary>
        [TestMethod]
        public void NewTest()
        {
            FromHeaderField actual = new FromHeaderField();
            Assert.AreEqual("From", actual.FieldName);
            Assert.IsTrue(actual.GetStringValue().Length != 0);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            FromHeaderField target = new FromHeaderField();
            string value = string.Empty;
            target.Parse(value);

            string expectedstr = "";
            string actualstr;
            actualstr = target.GetStringValue();
            Assert.AreEqual(expectedstr, actualstr);

            value = "From: \"bob\" <sips:google.com>";
            target.Parse(value);
            expectedstr = "\"bob\" <sips:google.com>";
            actualstr = target.GetStringValue();
            Assert.AreEqual(expectedstr, actualstr);

            value = "\tf\t:\t\r\n\t\"bob\"\t<sips:google.com>";
            target.Parse(value);
            expectedstr = "\"bob\" <sips:google.com>";
            actualstr = target.GetStringValue();
            Assert.AreEqual(expectedstr, actualstr);

            value = "\tfrOM\t:\t\r\n\t\"bob\"\t<sips:google.com>;tag=123";
            target.Parse(value);
            expectedstr = "\"bob\" <sips:google.com>;tag=123";
            actualstr = target.GetStringValue();
            Assert.AreEqual(expectedstr, actualstr);
        }

        /// <summary>
        ///A test for RemainHidden
        ///</summary>
        [TestMethod]
        public void RemainHiddenTest()
        {
            FromHeaderField target = new FromHeaderField();
            bool expected = false;
            bool actual;
            target.RemainHidden = expected;
            actual = target.RemainHidden;
            Assert.AreEqual(expected, actual);

            target.Parse(@"From: ""bob"" <sips:google.com>");

            string expectedstr = "From: \"bob\" <sips:google.com>";
            string actualstr;
            actualstr = target.ToString();
            Assert.AreEqual(expectedstr, actualstr);

            target.RemainHidden = true;
            expectedstr = "From: \"Anonymous\" <sips:unknown@private>";
            actualstr = target.ToString();
            Assert.AreEqual(expectedstr, actualstr);

            target.Parse("From: \"bob\" <sip:google.com>");
            target.RemainHidden = true;
            expectedstr = "From: \"Anonymous\" <sip:unknown@private>";
            actualstr = target.ToString();
            Assert.AreEqual(expectedstr, actualstr);
        }

        /// <summary>
        ///A test for Tag
        ///</summary>
        [TestMethod]
        public void TagTest()
        {
            TagAddressedHeaderFieldBase target = new FromHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.Tag;
            Assert.AreNotEqual(expected, actual);

            target.Tag = expected;
            actual = target.Tag;
            Assert.AreEqual(expected, actual);

            expected = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz0123456789-.!%*_+`'~";
            target.Tag = expected;
            actual = target.Tag;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToBytes
        ///</summary>
        [TestMethod]
        public void ToBytesTest()
        {
            HeaderFieldBase target = new FromHeaderField();
            ((FromHeaderField)target).Tag = "";
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("From: ".ToCharArray());
            byte[] actual;
            actual = target.GetBytes();
            Assert.AreEqual(new string(System.Text.UTF8Encoding.UTF8.GetChars(expected)), new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)), "Test the constructor");

            target.Parse("sip:123@bob.com");
            expected = System.Text.UTF8Encoding.UTF8.GetBytes("From: <sip:123@bob.com>".ToCharArray());
            actual = target.GetBytes();
            Assert.IsTrue(new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)).StartsWith("From: <sip:123@bob.com>"), "Test after assignment");
        }

        /// <summary>
        ///A test for ToChars
        ///</summary>
        [TestMethod]
        public void ToCharsTest()
        {
            HeaderFieldBase target = new FromHeaderField();
            char[] expected = "From: ".ToCharArray();
            char[] actual;
            actual = target.GetChars();
            Assert.IsTrue(new string(actual).StartsWith(new string(expected)), "Test the constructor");
            target.Parse("sip:!!!@bob.com");
            expected = "From: <sip:!!!@bob.com>".ToCharArray();
            actual = target.GetChars();
            Assert.IsTrue(new string(actual).StartsWith(new string(expected)), "Test after assignment");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            HeaderFieldBase target = new FromHeaderField();
            string expected = "From: ;tag=";
            string actual;
            actual = target.ToString();
            Assert.IsTrue(actual.StartsWith(expected), "Test the constructor");
            target.Parse("sip:asd@bob.com");
            expected = "From: <sip:asd@bob.com>";
            actual = target.ToString();
            Assert.IsTrue(actual.StartsWith(expected), "Test after assignment");
        }

        /// <summary>
        ///A test for Uri
        ///</summary>
        [TestMethod]
        public void UriTest()
        {
            SipUriHeaderFieldBase target = new FromHeaderField();
            SipUri expected = new SipUri("sip:1232@555:6593");
            SipUri actual;
            target.Uri = expected;
            actual = target.Uri;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Valid
        ///</summary>
        [TestMethod]
        public void ValidTest()
        {
            HeaderFieldBase_Accessor target = new FromHeaderField_Accessor();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test the constructor");

            expected = false;
            target.Parse("sip:()@bob.com");
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test after assignment");

            expected = true;
            ((FromHeaderField_Accessor)target).RecreateTag();
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test after assignment");

            target = new FromHeaderField_Accessor();
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test the constructor2");
        }

        #endregion Methods

        #region Other

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