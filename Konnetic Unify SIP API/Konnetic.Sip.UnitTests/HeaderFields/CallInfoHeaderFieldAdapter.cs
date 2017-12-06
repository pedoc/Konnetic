using System;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for CallInfoHeaderFieldAdapter and is intended
    ///to contain all CallInfoHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class CallInfoHeaderFieldAdapter
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
        ///A test for CallInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CallInfoHeaderFieldConstructorTest()
        {
            Uri uri = new Uri("http://192.168.171.1:80/images/iii.jpg");
            CallInfoHeaderField target = new CallInfoHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Call-Info");
            Assert.IsTrue(target.CompactName == "Call-Info");
            Assert.IsTrue(target.GetStringValue() == "<http://192.168.171.1/images/iii.jpg>");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.Purpose == "");
        }

        /// <summary>
        ///A test for CallInfoHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CallInfoHeaderFieldConstructorTest1()
        {
            string uri = "http://localhost/";
            CallInfoHeaderField target = new CallInfoHeaderField(uri);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Call-Info");
            Assert.IsTrue(target.CompactName == "Call-Info");
            Assert.IsTrue(target.GetStringValue() == "<http://localhost/>");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.Purpose == "");
        }

        [TestMethod]
        public void CallInfoHeaderFieldConstructorTest2()
        {
            Uri uri = new Uri("http://192.168.171.1:80/images/iii.jpg");
            CallInfoHeaderField target = new CallInfoHeaderField(uri,CallInfoPurpose.Card);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Call-Info");
            Assert.IsTrue(target.CompactName == "Call-Info");
            Assert.IsTrue(target.GetStringValue() == "<http://192.168.171.1/images/iii.jpg>;purpose=card");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.Purpose == "card");
        }

        [TestMethod]
        public void CallInfoHeaderFieldConstructorTest3()
        {
            string uri = "http://localhost/";
            CallInfoHeaderField target = new CallInfoHeaderField(uri, CallInfoPurpose.Icon);
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.FieldName == "Call-Info");
            Assert.IsTrue(target.CompactName == "Call-Info");
            Assert.IsTrue(target.GetStringValue() == "<http://localhost/>;purpose=icon");
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.Purpose == "icon");
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            Uri uri = new Uri("http://localhost/");
            CallInfoHeaderField target = new CallInfoHeaderField(uri);
            HeaderFieldBase expected = new CallInfoHeaderField(uri);
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target = new CallInfoHeaderField(uri,CallInfoPurpose.Card);
            ((CallInfoHeaderField)expected).Purpose = "card";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod]
        public void IsValidTest()
        {
            ParamatizedHeaderFieldBase target = new CallInfoHeaderField();
            bool expected = true;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            ((CallInfoHeaderField)target).Purpose = "Info";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            ((CallInfoHeaderField)target).AbsoluteUri = new Uri("http://localhost/");
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            ((CallInfoHeaderField)target).Purpose = "";
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            string uri = "http://localhost";
            CallInfoHeaderField target = new CallInfoHeaderField(uri);
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");

            value = "http://www.fred.com/photo.jpg";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "<http://www.fred.com/photo.jpg>");

            value = "Call-Info  \r\n :<http://www.fred.com/photo.jpg>\t;purpose=icon";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "<http://www.fred.com/photo.jpg>;purpose=icon");

            value = "Call-Info  \t :\thttp://www.fred.com/photo.jpg";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "<http://www.fred.com/photo.jpg>");

            value = "Call-Info  :  <http://www.fred.com/photo.jpg>;fff=123;purpose=icon";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "<http://www.fred.com/photo.jpg>;purpose=icon;fff=123");

            HeaderFieldGroup<CallInfoHeaderField> hfg = new HeaderFieldGroup<CallInfoHeaderField>();
            value = "Call-Info  :  <http://www.fred.com/photo.jpg>;fff=123;purpose=icon, <http://www.google.com/photo.jpg>";
            hfg.Parse(value);
            Assert.IsTrue(hfg[0].GetStringValue() == "<http://www.fred.com/photo.jpg>;purpose=icon;fff=123");
            Assert.IsTrue(hfg[1].GetStringValue() == "<http://www.google.com/photo.jpg>");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest1()
        {
            AbsoluteUriHeaderFieldBase target = new CallInfoHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.ToString() == "Call-Info: ");
            Assert.IsTrue(target.GetStringValue() == "");

            value = "http://123.123.432.234/images/iii.jpg";
            target.Parse(value);
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.ToString() == "Call-Info: <http://123.123.432.234/images/iii.jpg>");
            Assert.IsTrue(target.GetStringValue() == "<http://123.123.432.234/images/iii.jpg>");

            value = "<http://123.123.432.234/images/iii.jpg>;purpose=ggg";
            target.Parse(value);
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.ToString() == "Call-Info: <http://123.123.432.234/images/iii.jpg>;purpose=ggg");
            Assert.IsTrue(target.GetStringValue() == "<http://123.123.432.234/images/iii.jpg>;purpose=ggg");

            value = "<http://host.com/looks;like=aparameter>;purpose=ggg";
            target.Parse(value);
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.ToString() == "Call-Info: <http://host.com/looks;like=aparameter>;purpose=ggg");
            Assert.IsTrue(target.GetStringValue() == "<http://host.com/looks;like=aparameter>;purpose=ggg");

            value = "<http://host.com/looks;like=apara%20meter>;purpose=ggg";
            target.Parse(value);
            Assert.IsTrue(target.HasParameters == true);
            Assert.IsTrue(target.ToString() == "Call-Info: <http://host.com/looks;like=apara%20meter>;purpose=ggg");
            Assert.IsTrue(target.GetStringValue() == "<http://host.com/looks;like=apara%20meter>;purpose=ggg");

            HeaderFieldGroup<CallInfoHeaderField> g = new HeaderFieldGroup<CallInfoHeaderField>();
            g.Parse("Call-Info: <http://123.123.432.234/images/iii.jpg>;purpose=ggg, <http://atlanta.com/fred>;purpose=card");
            Assert.IsTrue(g[0].Purpose == "ggg");
            Assert.IsTrue(g[0].AbsoluteUri.ToString() == "http://123.123.432.234/images/iii.jpg");
            Assert.IsTrue(g[1].Purpose == "card");
            Assert.IsTrue(g[1].AbsoluteUri.ToString() == "http://atlanta.com/fred");
            Assert.IsTrue(g.ToString() == "Call-Info: <http://123.123.432.234/images/iii.jpg>;purpose=ggg, <http://atlanta.com/fred>;purpose=card");
            Assert.IsTrue(g.GetStringValue() == "<http://123.123.432.234/images/iii.jpg>;purpose=ggg, <http://atlanta.com/fred>;purpose=card");
        }

        /// <summary>
        ///A test for Purpose
        ///</summary>
        [TestMethod]
        public void PurposeTest()
        {
            string uri = "http://localhost";
            CallInfoHeaderField target = new CallInfoHeaderField(uri,CallInfoPurpose.None);
            string expected = string.Empty;
            string actual;
            actual = target.Purpose;
            Assert.AreEqual(expected, actual);

            expected = "Info";
            target.Purpose = expected;
            actual = target.Purpose;
            Assert.AreEqual(expected, actual);

            target = new CallInfoHeaderField(uri, CallInfoPurpose.Icon);
            expected = "icon";
            actual = target.Purpose;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PurposeTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(PurposeThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            Uri uri = new Uri("http://localhost");
            ParamatizedHeaderFieldBase target = new CallInfoHeaderField(uri,CallInfoPurpose.Icon);
            string expected = "<http://localhost/>;purpose=icon";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((CallInfoHeaderField)target).AddParameter(new SipParameter("fff", "fff"));
            expected = "<http://localhost/>;purpose=icon;fff=fff";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.ClearParameters();
            expected = "<http://localhost/>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest1()
        {
            AbsoluteUriHeaderFieldBase target = new CallInfoHeaderField();
            string expected = "";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new CallInfoHeaderField("http://localhost/");
            expected = "<http://localhost/>";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new CallInfoHeaderField("http://localhost/My Image.jpg",CallInfoPurpose.Icon);
            expected = "<http://localhost/My%20Image.jpg>;purpose=icon";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target = new CallInfoHeaderField("http://localhost/My%20Image.jpg", CallInfoPurpose.Icon);
            expected = "<http://localhost/My%20Image.jpg>;purpose=icon";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        public void op_ExplicitTest()
        {
            CallInfoHeaderField headerField = new CallInfoHeaderField();
            string expected = "Call-Info: ";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);

            headerField = new CallInfoHeaderField("http://www.fred.com/photo.jpg", CallInfoPurpose.Icon);
            expected = "Call-Info: <http://www.fred.com/photo.jpg>;purpose=icon";
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest()
        {
            string value = "Call-Info: <http://www.fred.com/photo.jpg>";
            CallInfoHeaderField expected = new CallInfoHeaderField("http://www.fred.com/photo.jpg");
            CallInfoHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);

            value = "<http://www.fred.com/photo.jpg>;purpose=icon";
            expected= new CallInfoHeaderField("http://www.fred.com/photo.jpg",CallInfoPurpose.Icon);
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        private bool PurposeThrowsError(string val)
        {
            try
                {
                CallInfoHeaderField target = new CallInfoHeaderField();
                target.Purpose = val;
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