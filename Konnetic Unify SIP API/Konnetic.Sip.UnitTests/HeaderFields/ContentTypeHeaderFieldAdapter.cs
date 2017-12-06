using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for ContentTypeHeaderFieldAdapter and is intended
    ///to contain all ContentTypeHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class ContentTypeHeaderFieldAdapter
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
            ContentTypeHeaderField target = new ContentTypeHeaderField();
            HeaderFieldBase expected = new ContentTypeHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            ((ContentTypeHeaderField)expected).MediaType = "aaaa!";
            target.MediaType = "aaaa!";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddMediaParameter(new SipParameter(Common.TOKEN, Common.TOKEN));
            actual = target.Clone();

            Assert.IsTrue(actual.GetStringValue() == "aaaa!/sdp;abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz0123456789-.!%*_+`'~=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~");
        }

        /// <summary>
        ///A test for ContentTypeHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentTypeHeaderFieldConstructorTest()
        {
            ContentTypeHeaderField target = new ContentTypeHeaderField();
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Type");
            Assert.IsTrue(target.CompactName == "c");
            Assert.IsTrue(target.GetStringValue() == "application/sdp");
        }

        /// <summary>
        ///A test for ContentTypeHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentTypeHeaderFieldConstructorTest1()
        {
            string mediaType = string.Empty;
            string mediaSubType = string.Empty;
            ContentTypeHeaderField target = new ContentTypeHeaderField(mediaType, mediaSubType);
            Assert.IsTrue(target.GetStringValue() == "");

            mediaType = Common.TOKEN;
            mediaSubType = "abcdefghijklmnopqrstuvwxyz";
            target = new ContentTypeHeaderField(mediaType, mediaSubType);
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/abcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        ///A test for ContentTypeHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void ContentTypeHeaderFieldConstructorTest2()
        {
            MediaType mediaType = MediaType.Application;
            string mediaSubType = string.Empty;
            ContentTypeHeaderField target = new ContentTypeHeaderField(mediaType, mediaSubType);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Type");
            Assert.IsTrue(target.CompactName == "c");
            Assert.IsTrue(target.GetStringValue() == "application/*");

            mediaSubType = Common.TOKEN;
            target = new ContentTypeHeaderField(mediaType, mediaSubType);
            Assert.IsTrue(target.AllowMultiple == false);

            Assert.IsTrue(target.FieldName == "Content-Type");
            Assert.IsTrue(target.CompactName == "c");
            Assert.IsTrue(target.GetStringValue() == "application/abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~");
        }


        [TestMethod]
        [ExpectedException(typeof(SipException))]
        public void AddMediaParameterTest()
            {
            ContentTypeHeaderField target = new ContentTypeHeaderField();
            target.AddMediaParameter(new SipParameter("q", "123"));
            }
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            ContentTypeHeaderField target = new ContentTypeHeaderField();
            string value = "\t";
            target.Parse(value);

            Assert.IsTrue(target.GetStringValue() == "");
            Assert.IsTrue(target.ToString() == "Content-Type: ");

            value = "  Content-TYpe\t:\t\r\n\t\t\tabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~ \t";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/*");
            Assert.IsTrue(target.ToString() == "Content-Type: abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/*");

            value = "  \r\n  abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~;hhh=hjjkj \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/*;hhh=hjjkj");
            Assert.IsTrue(target.MediaParameters.Count==1);

            value = "  \r\n  abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/jj;k=0.1;hhh=hjjkj \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.!%*_+`'~/jj;hhh=hjjkj;k=0.1");
            Assert.IsTrue(target.MediaParameters.Count == 2);

            value = " \tc  \t :\t\r\n  asdf \r\n ";
            target.Parse(value);
            Assert.IsTrue(target.GetStringValue() == "asdf/*");
            Assert.IsTrue(target.ToString() == "Content-Type: asdf/*");
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