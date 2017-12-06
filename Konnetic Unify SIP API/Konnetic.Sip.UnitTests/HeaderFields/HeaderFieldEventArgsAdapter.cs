using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HeaderFieldEventArgsAdapter and is intended
    ///to contain all HeaderFieldEventArgsAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HeaderFieldEventArgsAdapter
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
        ///A test for Cancel
        ///</summary>
        [TestMethod]
        public void CancelTest()
        {
            bool cancel = false;
            string headerFieldName = "Bob";
            HeaderFieldEventArgs target = new HeaderFieldEventArgs(cancel, headerFieldName);
            bool expected = true;
            bool actual;
            target.Cancel = expected;
            actual = target.Cancel;
            Assert.AreEqual(expected, actual,"Method not setting Cancel Correctly.");
        }

        public void ConstructorTest()
        {
            bool cancel = false;
            string headerFieldName = "Bob";
            HeaderFieldEventArgs target = new HeaderFieldEventArgs(cancel, headerFieldName);
            bool expected = false;
            string expectedFieldName = "Bob";
            Assert.AreEqual(expected, target.Cancel, "Constructor not setting cancel properly.");
            Assert.AreEqual(expectedFieldName, target.HeaderFieldName, "Constructor not setting HeaderFieldName properly.");

            cancel = true;
            HeaderFieldEventArgs target1 = new HeaderFieldEventArgs(cancel, headerFieldName);
            bool expected1 = true;
            Assert.AreEqual(expected1, target1.Cancel, "Constructor not setting cancel properly.");
        }

        /// <summary>
        ///A test for HeaderFieldName
        ///</summary>
        [TestMethod]
        public void HeaderFieldNameTest()
        {
            bool cancel = false;
            string headerFieldName = "CallID";
            HeaderFieldEventArgs target = new HeaderFieldEventArgs(cancel, headerFieldName);
            string expected = "CallID";
            string actual;
            target.HeaderFieldName = expected;
            actual = target.HeaderFieldName;
            Assert.AreEqual(expected, actual,"HeaderFieldName not being set correctly.");
        }

        #endregion Methods
    }
}