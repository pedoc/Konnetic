using Konnetic.Sip;
using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for CallIdHeaderFieldAdapter and is intended
    ///to contain all CallIdHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class CallIdHeaderFieldAdapter
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

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        /// <summary>
        ///A test for CallIdHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CallIdHeaderFieldConstructorTest()
        {
            string callID = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~()<>:\\\"/[]?{}@";
            CallIdHeaderField target = new CallIdHeaderField(callID);
            Assert.AreEqual(callID, target.GetStringValue());
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Call-ID");
            Assert.IsTrue(target.CompactName == "i");
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~()<>:\\\"/[]?{}@");
        }

        /// <summary>
        ///A test for CallIdHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CallIdHeaderFieldConstructorTest1()
        {
            CallIdHeaderField target = new CallIdHeaderField();
            string expected = string.Empty;
            Assert.AreEqual("Call-ID", target.FieldName);
            Assert.IsTrue(target.GetStringValue().Length == 0);

            Assert.AreEqual(false, target.AllowMultiple);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            HeaderFieldBase target = new CallIdHeaderField();
            HeaderFieldBase headerField = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual,"headerField is null");

            target.Parse("123");
            headerField = new CallIdHeaderField();
            expected = false;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Different Values but same names");

            headerField.Parse("123");
            expected = true;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Same");

            headerField.Parse("123a");
            expected = false;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Same1");

            headerField.Parse("123A");
            target.Parse("123a");
            expected = true;
            actual = target.Equals(headerField);
            Assert.AreEqual(expected, actual, "Same1");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest1()
        {
            CallIdHeaderField target = new CallIdHeaderField();
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual,"obj is null");

            obj = new CallIdHeaderField(target.CallId);
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual,"Equalivalent Objects");

            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual, "Same Objects");
        }

        /// <summary>
        ///A test for FieldName
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void FieldNameTest()
        {
            HeaderFieldBase_Accessor target = new CallIdHeaderField_Accessor();
            string expected = "Call-ID";
            string actual;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the constructor");
            expected = "Call-ID1";
            target.FieldName = expected;
            actual = target.FieldName;
            Assert.AreEqual(expected, actual, "Test the assignement");
        }

        /// <summary>
        ///A test for NewCallID
        ///</summary>
        [TestMethod]
        public void NewCallIDTest()
        {
            string host = "Konnetic.com";
            string expected = string.Empty;
            string actual;
            actual = CallIdHeaderField.NewCallId(host);
            Assert.IsTrue(actual.EndsWith(host));
        }

        /// <summary>
        ///A test for NewCallID
        ///</summary>
        [TestMethod]
        public void NewCallIDTest1()
        {
            string expected = string.Empty;
            string actual;
            actual = CallIdHeaderField.NewCallId();
            Assert.IsTrue(actual.Length > 0);
        }

        /// <summary>
        ///A test for RegenerateCallID
        ///</summary>
        [TestMethod]
        public void RegenerateCallIDTest()
        {
            CallIdHeaderField target = new CallIdHeaderField();
            string host = "Konnetic.com";
            target.RecreateCallId(host);
            Assert.IsTrue(target.GetStringValue().EndsWith(host));
        }

        /// <summary>
        ///A test for RegenerateCallID
        ///</summary>
        [TestMethod]
        public void RegenerateCallIDTest1()
        {
            CallIdHeaderField target = new CallIdHeaderField();
            string before = target.GetStringValue();
            target.RecreateCallId();
            string after = target.GetStringValue();
            Assert.AreNotEqual(before, after);
            Assert.IsTrue(after.Length>0);
        }

        /// <summary>
        ///A test for ToBytes
        ///</summary>
        [TestMethod]
        public void ToBytesTest()
        {
            HeaderFieldBase target = new CallIdHeaderField("CallID");
            byte[] expected = System.Text.UTF8Encoding.UTF8.GetBytes("Call-ID: CallID".ToCharArray());
            byte[] actual;
            actual = target.GetBytes();
            Assert.AreEqual(new string(System.Text.UTF8Encoding.UTF8.GetChars(expected)), new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)), "Test the constructor");

            target.Parse("123");
            expected = System.Text.UTF8Encoding.UTF8.GetBytes("Call-ID: 123".ToCharArray());
            actual = target.GetBytes();
            Assert.AreEqual(new string(System.Text.UTF8Encoding.UTF8.GetChars(expected)), new string(System.Text.UTF8Encoding.UTF8.GetChars(actual)), "Test after assignment");
        }

        /// <summary>
        ///A test for ToChars
        ///</summary>
        [TestMethod]
        public void ToCharsTest()
        {
            HeaderFieldBase target = new CallIdHeaderField("CallID");
            char[] expected = "Call-ID: CallID".ToCharArray();
            char[] actual;
            actual = target.GetChars();
            Assert.AreEqual(new string(expected), new string(actual), "Test the constructor");
            target.Parse("123");
            expected = "Call-ID: 123".ToCharArray();
            actual = target.GetChars();
            Assert.AreEqual(new string(expected), new string(actual), "Test after assignment");
        }

        //[TestMethod]
        //[ExpectedException(typeof(System.UriFormatException))]
        //public void ToCharsTest2()
        //{
        //    HeaderFieldBase target = new CallIdHeaderField();
        //    target.GetChars();
        //}
        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            HeaderFieldBase target = new CallIdHeaderField("call-ID");
            string expected = "Call-ID: call-ID";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Test the constructor");
            target.Parse("123");
            expected = "Call-ID: 123" ;
            actual = target.ToString();
            Assert.AreEqual(expected, actual, "Test after assignment");
        }

        /// <summary>
        ///A test for Valid
        ///</summary>
        [TestMethod]
        public void ValidTest()
        {
        HeaderFieldBase target = new CallIdHeaderField("");
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test the constructor");
            expected = true;
            target.Parse("123");
            actual = target.IsValid();
            Assert.AreEqual(expected, actual, "Test after assignment");
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