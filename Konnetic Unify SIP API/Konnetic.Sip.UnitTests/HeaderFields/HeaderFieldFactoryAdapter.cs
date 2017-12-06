using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HeaderFieldFactoryAdapter and is intended
    ///to contain all HeaderFieldFactoryAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HeaderFieldFactoryAdapter
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
 

        /// <summary>
        ///A test for CreateHeaderFieldFromLine
        ///</summary>
        [TestMethod]
        public void CreateHeaderFieldFromLineTest()
        {
            string headerLine = "To: \"bob\" <sip:bbb@bbb.com>";  
            HeaderFieldBase expected = new ToHeaderField(new SipUri("sip:bbb@bbb.com"),"bob");  
            HeaderFieldBase actual;
            actual = HeaderFieldFactory.CreateHeaderFieldFromLine(headerLine);
            Assert.AreEqual(expected, actual); 
        }

		[TestMethod]
		[ExpectedException(typeof(System.ArgumentNullException))]
		public void CreateHeaderFieldFromLineTest1()
			{
			string headerLine = null;
			HeaderFieldBase actual;
			actual = HeaderFieldFactory.CreateHeaderFieldFromLine(headerLine);
			}
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void CreateHeaderFieldTest()
        {
            string name = null;
            HeaderFieldBase expected = null;
            HeaderFieldBase actual;
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.AreEqual(expected, actual, "Test using null");
        }

        /// <summary>
        ///A test for CreateHeaderField
        ///</summary>
        [TestMethod]
        public void CreateHeaderFieldTest1()
        {
            string name = null;
            HeaderFieldBase expected = null;
            HeaderFieldBase actual;

            name = "NotAField";
            expected = new ExtensionHeaderField("NotAField");
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.AreEqual(expected.GetType(), actual.GetType(), "Test using non-existant field");

			name = "Accept";
			expected = new AcceptHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Accept");

			name = "Accept-Encoding";
			expected = new AcceptEncodingHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Accept-Encoding");

			name = "Accept-Language";
			expected = new AcceptLanguageHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Accept-Language");

			name = "Alert-Info";
			expected = new AlertInfoHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Alert-Info");

			name = "Allow";
			expected = new AllowHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Allow");

			name = "Authorization";
			expected = new AuthorizationHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Authorization");

			name = "Authentication-Info";
			expected = new AuthenticationInfoHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Authentication-Info");

			name = "Call-ID";
			expected = new CallIdHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Call-ID");

			name = "i";
			expected = new CallIdHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Call-ID");

			name = "Call-Info";
			expected = new CallInfoHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Call-Info");

			name = "Contact";
			expected = new ContactHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Contact");

			name = "Content-Disposition";
			expected = new ContentDispositionHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Disposition");
			
			name = "Content-Encoding";
			expected = new ContentEncodingHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Encoding");

			name = "e";
			expected = new ContentEncodingHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Encoding");
			
			name = "Content-Language";
			expected = new ContentLanguageHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Language");

			name = "Content-Length";
			expected = new ContentLengthHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Length");

			name = "l";
			expected = new ContentLengthHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Length");

			name = "Content-Type";
			expected = new ContentTypeHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Type");

			name = "c";
			expected = new ContentTypeHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Content-Length");

			name = "CSeq";
			expected = new CSeqHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using CSeq");

			name = "Error-Info";
			expected = new ErrorInfoHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Error-Info");

			name = "Expires";
			expected = new ExpiresHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Expires");

            name = "From";
            expected = new FromHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.IsInstanceOfType(actual, expected.GetType(), "Test using From");

            name = "f";
            expected = new FromHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.IsInstanceOfType(actual, expected.GetType(), "Test using From");

			name = "In-Reply-To";
			expected = new InReplyToHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using In-Reply-To");

			name = "Max-Forwards";
			expected = new MaxForwardsHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using MaxForwards");

			name = "MIME-Version";
			expected = new MimeVersionHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using MIME-Version");

			name = "Min-Expires";
			expected = new MinExpiresHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Min-Expires");

			name = "Organization";
			expected = new OrganizationHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Organization");

			name = "Priority";
			expected = new PriorityHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Priority");

			name = "Proxy-Authenticate";
			expected = new ProxyAuthenticateHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Proxy-Authenticate");

			name = "Proxy-Authorization";
			expected = new ProxyAuthorizationHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Proxy-Authorization");

			name = "Proxy-Require";
			expected = new ProxyRequireHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Proxy-Require");

			name = "Record-Route";
			expected = new RecordRouteHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Record-Route");

			name = "Reply-To";
			expected = new ReplyToHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Reply-To");

			name = "Retry-After";
			expected = new RetryAfterHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Retry-After");

			name = "Route";
			expected = new RouteHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Route");

			name = "server";
			expected = new ServerHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Server");

			name = "Subject";
			expected = new SubjectHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Subject");

			name = "s";
			expected = new SubjectHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Subject");

			name = "Supported";
			expected = new SupportedHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Supported");

			name = "k";
			expected = new SupportedHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Supported");

			name = "Timestamp";
			expected = new TimestampHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Timestamp");

            name = "To";
            expected = new ToHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.IsInstanceOfType(actual, expected.GetType(), "Test using To");

            name = "t";
            expected = new ToHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
            Assert.IsInstanceOfType(actual, expected.GetType(), "Test using To");

            name = "User-Agent";
            expected = new UserAgentHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using User-Agent");

            name = "Via";
			expected = new ViaHeaderField();
            actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Via");

			name = "v";
			expected = new ViaHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Via");

			name = "Warning";
			expected = new WarningHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using Warning");

			name = "WWW-Authenticate";
			expected = new WwwAuthenticateHeaderField();
			actual = HeaderFieldFactory.CreateHeaderField(name);
			Assert.IsInstanceOfType(actual, expected.GetType(), "Test using WWW-Authenticate");
        }

 

        #endregion Methods

        #region Other
 

        #endregion Other
    }
}