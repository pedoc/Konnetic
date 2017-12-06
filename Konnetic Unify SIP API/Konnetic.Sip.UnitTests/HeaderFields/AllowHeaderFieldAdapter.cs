using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AllowHeaderFieldAdapter and is intended
    ///to contain all AllowHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AllowHeaderFieldAdapter
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
        ///A test for AllowHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AllowHeaderFieldConstructorTest()
        {
            SipMethod method = SipMethod.Info;
            AllowHeaderField target = new AllowHeaderField(method);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Allow");
            Assert.IsTrue(target.CompactName == "Allow");
            Assert.IsTrue(target.GetStringValue() == "INFO");
            string expected = "Allow: INFO";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.GetStringValue() == "INFO");

            target.Method = new SipMethod("hhh");
            expected = "HHH";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AllowHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AllowHeaderFieldConstructorTest1()
        {
            AllowHeaderField target = new AllowHeaderField();
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Allow");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.Method == new SipMethod(""));
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            SipMethod method = SipMethod.Invite;
            AllowHeaderField target = new AllowHeaderField(method);
            HeaderFieldBase expected = new AllowHeaderField();
            ((AllowHeaderField)expected).Method = SipMethod.Invite;
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AllowHeaderField> hg = new HeaderFieldGroup<AllowHeaderField>();
            hg.Add(target);
            HeaderFieldGroup<AllowHeaderField> hg1 = new HeaderFieldGroup<AllowHeaderField>();
            hg1.Add(target);
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(hg1, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            SipMethod method = SipMethod.Ack;
            AllowHeaderField target = new AllowHeaderField();
            AllowHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AllowHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AllowHeaderField(method);
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Method = SipMethod.Ack;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Method = SipMethod.Invite;
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
            AllowHeaderField target = new AllowHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Method = SipMethod.Message;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Method = new SipMethod("");
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            HeaderFieldGroup<AllowHeaderField> hg = new HeaderFieldGroup<AllowHeaderField>();
            hg.Add(target);
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);

            hg[0].Method = SipMethod.Message;
            expected = true;
            actual = hg.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        public void MethodTest()
        {
            AllowHeaderField target = new AllowHeaderField();
            SipMethod expected = new SipMethod("");
            SipMethod actual;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected =  SipMethod.Register;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            AllowHeaderField target = new AllowHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.Method == "");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.ToString() == "Allow: ");

            value = " alLow: ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.Method == "");
            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.ToString() == "Allow: ");

            value = " alLow  \t : assd";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "ASSD");
            Assert.IsTrue(target.Method == "ASSD");
            Assert.IsTrue(target.GetStringValue() == "ASSD");
            Assert.IsTrue(target.ToString() == "Allow: ASSD");

            value = null;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "ASSD");
            Assert.IsTrue(target.Method == "ASSD");
            Assert.IsTrue(target.GetStringValue() == "ASSD");
            Assert.IsTrue(target.ToString() == "Allow: ASSD");

            HeaderFieldGroup<AllowHeaderField> g = new HeaderFieldGroup<AllowHeaderField>();
            g.Parse("alLow\t:\tassd\t,\tINVITE\t");
            Assert.IsTrue(g[0].Method == "ASSD");
            Assert.IsTrue(g[1].Method == "INVITE");
            Assert.IsTrue(g.GetStringValue() == "ASSD, INVITE");
            Assert.IsTrue(g.ToString() == "Allow: ASSD, INVITE");
        }

        //[TestMethod]
        //[ExpectedException(typeof(SipFormatException))]
        //public void ParseTest2()
        //{
        //    AllowHeaderField target = new AllowHeaderField();
        //    string value = " Alloww: INVITE";
        //    target.Parse(value);
        //}
        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            AllowHeaderField target = new AllowHeaderField();
            string expected = string.Empty;
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Method = SipMethod.Ok;
            expected = "OK";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            AllowHeaderField headerField = new AllowHeaderField();

            string expected = "Allow: ";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            string value = " alLow: assd";
            headerField.Parse(value);
            expected = "Allow: ASSD";
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
            AllowHeaderField expected = new AllowHeaderField();
            AllowHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);

            expected.Method = SipMethod.Invite;
            value = "INVITE OK";
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