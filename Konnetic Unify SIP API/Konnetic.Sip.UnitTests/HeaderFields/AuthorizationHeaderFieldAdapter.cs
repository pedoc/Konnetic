using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for AuthorizationHeaderFieldAdapter and is intended
    ///to contain all AuthorizationHeaderFieldAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class AuthorizationHeaderFieldAdapter
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
        ///A test for AuthorizationHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AuthorizationHeaderFieldConstructorTest()
        {
            string scheme = string.Empty;
            AuthorizationHeaderField target = new AuthorizationHeaderField(scheme);
            Assert.IsTrue(target.Scheme == "");

            scheme = "sss";
            target = new AuthorizationHeaderField(scheme);
            Assert.IsTrue(target.Scheme == @"sss");
            Assert.IsTrue(target.Algorithm == "");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.CNonce == "");
            Assert.IsTrue(target.FieldName == "Authorization");
            Assert.IsTrue(target.GetStringValue() == @"sss");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.Nonce == "");
            Assert.IsTrue(target.NonceCount == "");
            Assert.IsTrue(target.Opaque == "");
            Assert.IsTrue(target.Realm == "");
            Assert.IsTrue(target.Response == "");
            Assert.IsTrue(target.CompactName == "Authorization");
        }

        /// <summary>
        ///A test for AuthorizationHeaderField Constructor
        ///</summary>
        [TestMethod]
        public void AuthorizationHeaderFieldConstructorTest1()
        {
            AuthorizationHeaderField target = new AuthorizationHeaderField();
            Assert.IsTrue(target.Scheme == @"Digest");
            Assert.IsTrue(target.Algorithm == "");
            Assert.IsTrue(target.AllowMultiple == true);

            Assert.IsTrue(target.CNonce == "");
            Assert.IsTrue(target.FieldName == "Authorization");
            Assert.IsTrue(target.GetStringValue() == @"Digest");
            Assert.IsTrue(target.HasParameters == false);
            Assert.IsTrue(target.MessageQop == "");
            Assert.IsTrue(target.Nonce == "");
            Assert.IsTrue(target.NonceCount == "");
            Assert.IsTrue(target.Opaque == "");
            Assert.IsTrue(target.Realm == "");
            Assert.IsTrue(target.Response == "");
            Assert.IsTrue(target.CompactName == "Authorization");
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            AuthorizationHeaderField target = new AuthorizationHeaderField();
            HeaderFieldBase expected = new AuthorizationHeaderField();
            HeaderFieldBase actual;
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.Response="111";
            ((AuthorizationHeaderField)expected).Response = "111";
            actual = target.Clone();
            Assert.AreEqual(expected, actual);

            target.AddParameter("bob","111");
            ((AuthorizationHeaderField)expected).AddParameter("bob", "111");
            actual = target.Clone();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            AuthorizationHeaderField target = new AuthorizationHeaderField();
            AuthorizationHeaderField other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AuthorizationHeaderField();
            other = new AuthorizationHeaderField();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Realm="hhh";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Realm = "hhh";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target = new AuthorizationHeaderField("123");
            target.Realm = "hhh";
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AuthorizationHeaderField("123");
            other.Realm = "hhh";
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStringValue
        ///</summary>
        [TestMethod]
        public void GetStringValueTest()
        {
            SchemeAuthHeaderFieldBase target = new AuthorizationHeaderField();
            string expected = "Digest";
            string actual;
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Algorithm="MD5";
            expected = "Digest algorithm=MD5";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            ((AuthorizationHeaderField)target).MessageQop = "auth";
            expected = "Digest algorithm=MD5, qop=auth";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Nonce = "adfasdf7asd7fas87sadf7as8fd7asd8f7as8f";
            expected = "Digest algorithm=MD5, qop=auth, nonce=\"adfasdf7asd7fas87sadf7as8fd7asd8f7as8f\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Algorithm = "";
            ((AuthorizationHeaderField)target).MessageQop = "";
            target.Nonce = "";
            target.Opaque = "aa55";
            expected = "Digest opaque=\"aa55\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            target.Realm = "127:121:677:334:8556";
            expected = "Digest opaque=\"aa55\", realm=\"127:121:677:334:8556\"";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            AuthorizationHeaderField target1 = (AuthorizationHeaderField)target;
            AuthHeaderFieldGroup<AuthorizationHeaderField> hg = new AuthHeaderFieldGroup<AuthorizationHeaderField>();
            hg.Add(target1);

            actual = hg.GetStringValue();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void op_ExplicitTest()
        {
            AuthorizationHeaderField headerField = new AuthorizationHeaderField();
            headerField.Response = "bbb";
            string expected = "Authorization: Digest response=\"bbb\"";
            string actual;
            actual = ((string)(headerField));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void op_ExplicitTest1()
        {
            AuthorizationHeaderField headerField = null;
            string expected = string.Empty;
            string actual;
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
            AuthorizationHeaderField expected = new AuthorizationHeaderField("");
            AuthorizationHeaderField actual;
            actual = value;
            Assert.AreEqual(expected, actual);

            value = "Authorization : Digest username=\"Alice\", realm=\"atlanta.com\"";
            expected = new AuthorizationHeaderField("Digest");
            expected.Username = "Alice";
            expected.Realm = "atlanta.com";
            actual = value;
            Assert.AreEqual(expected, actual);
        }

        #endregion Methods
    }
}