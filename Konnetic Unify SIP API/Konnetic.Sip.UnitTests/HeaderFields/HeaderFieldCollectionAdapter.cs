using System;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HeaderFieldCollectionAdapter and is intended
    ///to contain all HeaderFieldCollectionAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HeaderFieldCollectionAdapter
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
        ///A test for Add
        ///</summary>
        [TestMethod]
        public void AddTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = "cseq";
            string value = "478912 INVITE";
            target.Add(name, value);
            string expected = "478912 INVITE";
            string actual;
            actual = target[0].GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "CSeq";
            actual = target[0].FieldName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = string.Empty;
            string value = string.Empty;
            target.Add(name, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTest1b()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = null;
            string value = string.Empty;
            target.Add(name, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTest1c()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = "To";
            string value = null;
            target.Add(name, value);
        }

        [TestMethod]
        public void AddTest2()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string nameValuePair = "cseq=345435 INVITE";
            target.Add(nameValuePair);

            string expected = "345435 INVITE";
            string actual;
            actual = target[0].GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "CSeq";
            actual = target[0].FieldName;
            Assert.AreEqual(expected, actual);

            nameValuePair = "Expires=5";
            target.Add(nameValuePair);

            expected = "5";
            actual = target[1].GetStringValue();
            Assert.AreEqual(expected, actual);

            expected = "Expires";
            actual = target[1].FieldName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTest2a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string nameValuePair = "to";
            target.Add(nameValuePair);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTest2b()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string nameValuePair = "allow=";
            target.Add(nameValuePair);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTest2c()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string nameValuePair = string.Empty;
            target.Add(nameValuePair);
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod]
        public void ContainsTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = "To";
            bool expected = false;
            bool actual;
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);

            HeaderFieldBase field = new Konnetic.Sip.Headers.InReplyToHeaderField("jjsldfkjj");
            target.Add(field);
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);

            name = "in-Reply-To";
            expected = true;
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);

            name = "To";
            expected = false;
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);

            name = "inReplyTo";
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);

            name = "in-RePly-To";
            expected = true;
            actual = target.Contains(name);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod]
        public void ContainsTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase field = new Konnetic.Sip.Headers.InReplyToHeaderField("jjsldfkjj");
            bool expected = false;
            bool actual;
            actual = target.Contains(field);
            Assert.AreEqual(expected, actual);

            target.Add(new Konnetic.Sip.Headers.MaxForwardsHeaderField(40));
            actual = target.Contains(field);
            Assert.AreEqual(expected, actual);

            HeaderFieldBase field1 = new Konnetic.Sip.Headers.InReplyToHeaderField("rewtwe");
            expected = false;
            actual = target.Contains(field1);
            Assert.AreEqual(expected, actual);

            target.Add(field);
            expected = true;
            actual = target.Contains(field);
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = target.Contains(field1);
            Assert.AreEqual(expected, actual);

            ((InReplyToHeaderField)field1).CallId = "jjsldfkjj";
            expected = true;
            actual = target.Contains(field1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ContainsTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase field = null;
            bool actual;
            actual = target.Contains(field);
        }

 

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ContainsTestab()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = null;
            bool actual;
            actual = target.Contains(name);
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod]
        public void CountTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            int actual = 0;
            actual = target.Count;

            target.Add(new AcceptHeaderField());
            actual = 1;
            actual = target.Count;

            target.Remove("Accept");
            actual = 0;
            actual = target.Count;

            target.Add(new Konnetic.Sip.Headers.ServerHeaderField("hhasdfa","1.0"));
            actual = 1;
            actual = target.Count;

            target.Clear();
            actual = 0;
            actual = target.Count;
        }

        [TestMethod]
        public void ItemTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = AllowHeaderField.LongName;
            HeaderFieldBase expected = new AllowHeaderField(Konnetic.Sip.Messages.SipMethod.Notify);
            HeaderFieldBase actual;
            target[name] = expected;
            actual = target[name];
            Assert.AreEqual(null, actual);

            target.Add(new AllowHeaderField(Konnetic.Sip.Messages.SipMethod.Info));
            target[name] = expected;
            actual = target[name];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod]
		[ExpectedException(typeof(System.ArgumentNullException))]
        public void ItemTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = string.Empty;
            target[name] = null;
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ItemTest1ab()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name ="To";
            target[name] = null;
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod]
        public void ItemTest2()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            int index = 0;
            HeaderFieldBase expected = new AcceptHeaderField();
            target.Add(expected);
            HeaderFieldBase actual;
            target[index] = expected;
            actual = target[index];
            Assert.AreEqual(expected, actual);

            target.Add(new AcceptEncodingHeaderField("GZip"));
            index = 1;
            actual = target[index];
            Assert.AreNotEqual(expected, actual);

            index = 1;
            target[index] = new AcceptEncodingHeaderField("111");

            actual = target[index];
            Assert.AreNotEqual(expected, actual);

            expected = new AcceptEncodingHeaderField("111");
            actual = target[index];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void ItemTest2a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add(new AcceptHeaderField());
            int index = 1;
            target[index] = new AcceptEncodingHeaderField("GZip");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItemTest2b()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add(new AcceptHeaderField());
            target.Add(new AcceptEncodingHeaderField("GZip"));
            target[0] = new AcceptEncodingHeaderField("111");
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod]
        public void RemoveTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase hf = new Konnetic.Sip.Headers.RequireHeaderField("ggg");
            target.Set(hf);
            string name = string.Empty;
            target.Remove(name);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "ggg");

            name = "Require1";
            target.Remove(name);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "ggg");

            name = "require";
            target.Remove(name);
            Assert.IsTrue(target.Count==0);
        }

        [TestMethod]
		[ExpectedException(typeof(System.ArgumentNullException))]
        public void RemoveTesta()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = null;
            target.Remove(name);
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod]
        public void SetTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase hf = new Konnetic.Sip.Headers.RequireHeaderField("ggg");
            target.Set(hf);
            string name = "Require";
            string value = string.Empty;
            target.Set(name, value);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "");

            name = "Require";
            value = "yyy";
            target.Set(name, value);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "yyy");

            name = "Allow";
            value = "SEND";
            target.Set(name, value);
            Assert.IsTrue(((AllowHeaderField)target[1]).Method == "SEND");
        }

        /// <summary>
        ///A test for Set
        ///</summary>
        [TestMethod]
        public void SetTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase hf = new Konnetic.Sip.Headers.RequireHeaderField("ggg");
            target.Set(hf);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "ggg");

            hf = new Konnetic.Sip.Headers.RequireHeaderField("hhh");
            target.Set(hf);
            Assert.IsTrue(((RequireHeaderField)target[0]).Option == "hhh");

            hf = new Konnetic.Sip.Headers.PriorityHeaderField(Priority.Emergency);
            target.Set(hf);
            Assert.IsTrue(((PriorityHeaderField)target[1]).Priority == "emergency");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTesta()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            HeaderFieldBase hf = new Konnetic.Sip.Headers.RequireHeaderField("ggg");
            target.Set(hf);
            string name = string.Empty;
            string value = string.Empty;
            target.Set(name, value);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void SetTestb()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string name = "To";
            string value = null;
            target.Set(name, value);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void SetTestc()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
             			string name = null;
            string value = "dd";
            target.Set(name, value);
        }

        /// <summary>
        ///A test for ToStringCombineLines
        ///</summary>
        //[TestMethod]
        //[DeploymentItem("Konnetic.Sip.dll")]
        //public void ToStringCombineLinesTest()
        //{
        //    HeaderFieldCollection_Accessor target = new HeaderFieldCollection_Accessor();
        //    target.Add("to=bob@ms.com");
        //    target.Add(new AllowHeaderField("INVITE"));
        //    bool useCompactForm = false;
        //    string expected = "To: <bob@ms.com>, Allow: INVITE";
        //    string actual;
        //    actual = target.ToStringCombineLines(useCompactForm);
        //    Assert.AreEqual(expected, actual);
        //    useCompactForm = true;
        //    expected = "t=bob@ms.com;allow=INVITE";
        //    actual = target.ToStringCombineLines(useCompactForm);
        //    Assert.AreEqual(expected, actual);
        //}
        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("to=sip:bob@ms.com");
            bool useCompactForm = false;
            string expected = "To: <sip:bob@ms.com>\r\n";
            string actual;
            actual = target.ToString(useCompactForm);
            Assert.AreEqual(expected, actual);

            expected = "t: <sip:bob@ms.com>\r\nf: <sip:fanny@ms.com>\r\n";
            target.Add("from=sip:fanny@ms.com");
            useCompactForm = true;
            actual = target.ToString(useCompactForm);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("to=sip:bob@ms.com");
            string expected = "To: <sip:bob@ms.com>\r\n";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

            expected = "To: <sip:bob@ms.com>\r\nFrom: <sip:fanny@ms.com>\r\n";
            target.Add("From=<sip:fanny@ms.com>");
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToStringTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("to: sip:bob@ms.com");
        }

        /// <summary>
        ///A test for ToUriString
        ///</summary>
        [TestMethod]
        public void ToUriStringTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("to=sip:bob@ms.com");
            target.Add(new AllowHeaderField("INVITE"));
            string expected = "?to=%3Csip:bob@ms.com%3E&allow=INVITE";
            string actual;
            actual = target.ToUriString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToUriString
        ///</summary>
        [TestMethod]
        public void ToUriStringTest1()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("date=Sat, 13 Nov 2010 23:29:00 GMT");
            target.Add("to=bob@ms.com");
            string[] exclude = new string[] { "from" };
            string expected = "?date=Sat,%2013%20Nov%202010%2023:29:00%20GMT&to=";
            string actual;
            actual = target.ToUriString(exclude);
            Assert.AreEqual(expected, actual);

             exclude = new string[] { "to" };
            expected = "?date=Sat,%2013%20Nov%202010%2023:29:00%20GMT";
            actual = target.ToUriString(exclude);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ToUriStringTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("date=Sat, 13 Nov 2010 23:29:00 GMT");
            target.Add("to=bob@ms.com");
            string[] exclude = null;
            string expected = "Date=Sat,%2013%20Nov%202010%2023:29:00%20GMT;to=bob@ms.com";
            string actual;
            actual = target.ToUriString(exclude);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod]
        public void UpdateTest()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("date=Sat, 13 Nov 2010 23:29:00 GMT");
            string fieldName = "date";
            string newValue = string.Empty; 
            target.Update(fieldName, newValue); 
            Assert.IsTrue(target[fieldName].GetStringValue()==newValue);

            fieldName = "date";
           newValue = "Sat, 13 Nov 2010 23:29:00 GMT"; 
            target.Update(fieldName, newValue);
            Assert.IsTrue(target[fieldName].GetStringValue() == newValue);

            fieldName = "date1";
            target.Update(fieldName, newValue);
            fieldName = "date"; 
            Assert.IsTrue(target[fieldName].GetStringValue() == newValue);

            fieldName = "date";
            newValue = "Sat, 13 Nov 2010 23:29:00 GMT";
            target.Update(fieldName, newValue);
            Assert.IsTrue(target[fieldName].GetStringValue() == newValue);

            fieldName = "Date";
            newValue = "Sun, 14 Nov 2010 23:29:00 GMT";
            target.Update(fieldName, newValue);
            Assert.IsTrue(target[fieldName].GetStringValue() == newValue);
            Assert.IsTrue(target[0].GetStringValue()=="Sun, 14 Nov 2010 23:29:00 GMT");
        }

        ///// <summary>
        /////A test for Update
        /////</summary>
        //[TestMethod]
        //public void UpdateTest1()
        //{
        //    HeaderFieldCollection target = new HeaderFieldCollection();
        //    string fieldName = "To";
        //    string newValue = string.Empty;
        //    SipParameterCollection parameters = new SipParameterCollection(new  SipParameter("tag","asdfah44"));
        //    bool expected = true;
        //    bool actual;
        //    target.Add("To", "sip:alice@bob.com");
        //    actual = target.Update(fieldName, newValue, parameters);
        //    Assert.AreEqual(expected, actual);
        //    Assert.IsTrue(target[0].GetStringValue() == ";tag=asdfah44");

        //    fieldName = "To";
        //    actual = target.Update(fieldName, newValue, parameters);
        //    expected = true;
        //    Assert.AreEqual(expected, actual);
        //    Assert.IsTrue(target[0].GetStringValue() == "");

        //    fieldName = "To";
        //    newValue = "sip:alice@atlanta.org";
        //    actual = target.Update(fieldName, newValue, parameters);
        //    expected = true;
        //    Assert.AreEqual(expected, actual);
        //    Assert.IsTrue(target[0].GetStringValue() == "<sip:alice@atlanta.org>");
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateTest1a()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            string fieldName = string.Empty;
            string newValue = string.Empty;
            target.Add("To", "sip:alice@bob.com");
            target.Update(fieldName, newValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTesta()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("date=Sat, 13 Nov 2010 23:29:00 GMT");
            target.Update("date", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTestb()
        {
            HeaderFieldCollection target = new HeaderFieldCollection();
            target.Add("date=Sat, 13 Nov 2010 23:29:00 GMT");
            target.Update(null, "Sat, 13 Nov 2010 23:29:00 GMT");
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