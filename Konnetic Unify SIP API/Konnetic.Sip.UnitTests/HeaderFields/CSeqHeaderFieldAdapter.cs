using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for CSeqHeaderFieldAdapter and is intended
    ///to contain all CSeqHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class CSeqHeaderFieldAdapter
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
        ///A test for CSeqHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CSeqHeaderFieldConstructorTest()
        {
            int cseq = 0;
            CSeqHeaderField target = new CSeqHeaderField(cseq);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == "0");

            cseq = int.MaxValue;
            target = new CSeqHeaderField(cseq);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == int.MaxValue.ToString());
        }

        /// <summary>
        ///A test for CSeqHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CSeqHeaderFieldConstructorTest1()
        {
            CSeqHeaderField target = new CSeqHeaderField(0);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == "0");
        }

        /// <summary>
        ///A test for CSeqHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CSeqHeaderFieldConstructorTest2()
        {
            int cseq = 0;
            SipMethod method = SipMethod.Cancel;
            CSeqHeaderField target = new CSeqHeaderField(cseq, method);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == "0 CANCEL");

            cseq = 11111110;
            method = SipMethod.Empty;
            target = new CSeqHeaderField(cseq, method);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == "11111110");
        }

        /// <summary>
        ///A test for CSeqHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void CSeqHeaderFieldConstructorTest3()
        { 
            CSeqHeaderField target = new CSeqHeaderField(0);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() == "0");
			 
            target = new CSeqHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "CSeq");
            Assert.IsTrue(target.CompactName == "CSeq");
            Assert.IsTrue(target.GetStringValue() != "0");
        }

        [ExpectedException(typeof(SipOutOfRangeException))]
        [TestMethod]
        public void CSeqHeaderFieldConstructorTest5()
        {
            int cseq = -1010100;
            CSeqHeaderField target = new CSeqHeaderField(cseq);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            HeaderFieldBase expected = new CSeqHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
			Assert.AreNotEqual(expected, actual);
			((CSeqHeaderField)actual).Sequence = ((CSeqHeaderField)expected).Sequence;
			Assert.AreEqual(expected, actual);

            target.Method = " ssss ";
            actual = target.Clone();
            Assert.AreNotEqual(expected, actual);

            ((CSeqHeaderField)expected).Method = " ssss ";
            actual = target.Clone();
			Assert.IsTrue(((CSeqHeaderField)expected).Method == ((CSeqHeaderField)actual).Method);

            target.Sequence = CSeqHeaderField.NewSequence();
			actual = target.Clone();
			Assert.IsTrue(((CSeqHeaderField)expected).Method == ((CSeqHeaderField)actual).Method);
			Assert.IsFalse(((CSeqHeaderField)expected).Sequence == ((CSeqHeaderField)actual).Sequence);

            ((CSeqHeaderField)expected).Sequence = target.Sequence;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            CSeqHeaderField target = new CSeqHeaderField(123);
            CSeqHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

			other = new CSeqHeaderField(123);
            expected = true;
            actual = target.Equals(other);
			Assert.AreEqual(expected, actual); 

            other.Next();
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Next();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Method="aaa";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Method = "Aaa";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Method = "";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Method = "";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Method = "  \r\n  ";
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
            CSeqHeaderField target = new CSeqHeaderField();
            bool expected = false;
            bool actual;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Sequence = 100;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Method = Common.TOKEN;
            expected = true;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Sequence = 0;
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);

            target.Sequence = 110;
            target.Method = "  \r\n ";
            expected = false;
            actual = target.IsValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        public void MethodTest()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            SipMethod expected = SipMethod.Empty;
            SipMethod actual;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected = SipMethod.Invite;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected = SipMethod.Cancel;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected = SipMethod.Empty;
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);

            expected = new SipMethod(Common.TOKEN);
            target.Method = expected;
            actual = target.Method;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MethodTest1()
        {
            for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
                {
                string val = new string(Common.TOKENRESERVED[i], 1);
                Assert.IsTrue(MethodThrowsError(val), "Exception Not thrown on: " + val);
                }
        }

        /// <summary>
        ///A test for NewSequence
        ///</summary>
        [TestMethod]
        public void NewSequenceTest()
        {
            long? actual;
            actual = CSeqHeaderField.NewSequence();
            Assert.IsTrue(actual>0);
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [TestMethod]
        public void NextTest()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            long? before = target.Sequence;
            target.Next();
            long? after = target.Sequence;
            Assert.IsTrue(after == before + 1);

            target.Sequence = CSeqHeaderField.MaxSequence;
            target.Next();
            after = target.Sequence;
            Assert.IsTrue(after == 1);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            string value = string.Empty;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "");

            value = " \r\n 99 ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "99");

            value = "\tcSeq   : \r\n " + int.MaxValue;
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == int.MaxValue.ToString() + "");

            value = " cSeq\t:\t\r\n\t4294967295 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~\t";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "4294967295 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~".ToUpperInvariant());

            value = " cSeq   : \r\n 1234567890\tabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "1234567890 abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~".ToUpperInvariant());
        }

        [TestMethod]
        [ExpectedException(typeof(SipParseException))]
        public void ParseTest1()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            long lng = 4294967296;
            string value = lng.ToString();
            target.Parse(value);
        }

        /// <summary>
        ///A test for RecreateSequence
        ///</summary>
        [TestMethod]
        public void RecreateSequenceTest()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            long? before = target.Sequence;
            target.RecreateSequence();
            long? after = target.Sequence;
            Assert.AreNotEqual(before, after);
            Assert.IsTrue(after>0);
        }

        /// <summary>
        ///A test for Sequence
        ///</summary>
        [TestMethod]
        public void SequenceTest()
        {
            CSeqHeaderField target = new CSeqHeaderField(0);
            long? expected = 0;
            long? actual;
            actual = target.Sequence;
            Assert.AreEqual(expected, actual);

            target.Sequence = expected;
            actual = target.Sequence;
            Assert.AreEqual(expected, actual);

            expected = 4294967295;
            target.Sequence = expected;
            actual = target.Sequence;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void SequenceTest1()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            int expected = -1;
            target.Sequence = expected;
        }

        [TestMethod]
		[ExpectedException(typeof(SipOutOfRangeException))]
        public void SequenceTest2()
        {
            CSeqHeaderField target = new CSeqHeaderField();
            long expected = 4294967296;
            target.Sequence = expected;
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            CSeqHeaderField target = new CSeqHeaderField(1);
            string expected = "1";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Sequence = 4294967295;
            expected = "4294967295";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Sequence = 0;
            expected = "0";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Sequence = uint.MaxValue;
            expected = uint.MaxValue.ToString() + "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Method = "";
            target.Sequence = int.MaxValue;
            expected = int.MaxValue.ToString() + "";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Method = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~";
            target.Sequence = int.MaxValue;
            expected = int.MaxValue.ToString() +" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~".ToUpperInvariant() ;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Method = "";
            target.Sequence = 1;
            expected = "1" ;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        private bool MethodThrowsError(string val)
        {
            try
                {
                CSeqHeaderField target = new CSeqHeaderField();
                target.Method = new SipMethod(val);
                }
            catch(SipException)
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