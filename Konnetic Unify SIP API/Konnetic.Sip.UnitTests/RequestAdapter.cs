using System.Collections.Generic;

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for RequestAdapter and is intended
    ///to contain all RequestAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class RequestAdapter
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
        ///A test for Request Constructor
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void DefaultMethodConstructorTest()
        {
			//Request_Accessor target = new Request_Accessor(SipMethod.Empty);
			//Assert.AreEqual(target.Method, SipMethod.Empty, "Request does not set SIPMethod to default Unknown value");
        }

        /// <summary>
        ///A test for Method
        ///</summary>
        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void MethodSetTest()
        {
		//Request_Accessor target = new Request_Accessor(SipMethod.Invite);
		//SipMethod expected = SipMethod.Invite;
		//SipMethod actual;
		//target.Method = expected;
		//actual = target.Method;
		//Assert.AreEqual(expected, actual, "Request does not set SIPMethod correctly");
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