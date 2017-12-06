using System;
using System.Collections;

using Konnetic.Sip.Headers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konnetic.Sip.UnitTests
{
    /// <summary>
    ///This is a test class for HeaderFieldGroupAdapter and is intended
    ///to contain all HeaderFieldGroupAdapter Unit Tests
    ///</summary>
    [TestClass]
    public class HeaderFieldGroupAdapter
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

        [TestMethod]
        public void CountTest()
        {
            CountTestHelper<ExtensionHeaderField>();
            CountTestHelper<AcceptHeaderField>();
            CountTestHelper<AcceptEncodingHeaderField>();
            CountTestHelper<AcceptLanguageHeaderField>();
            CountTestHelper<AlertInfoHeaderField>();
            CountTestHelper<AllowHeaderField>();
            CountTestHelper<CallInfoHeaderField>();
            CountTestHelper<ContactHeaderField>();
            CountTestHelper<ContentEncodingHeaderField>();
            CountTestHelper<ContentLanguageHeaderField>();
            CountTestHelper<ErrorInfoHeaderField>();
            CountTestHelper<InReplyToHeaderField>();
            CountTestHelper<ProxyRequireHeaderField>();
            CountTestHelper<RecordRouteHeaderField>();
            CountTestHelper<RequireHeaderField>();
            CountTestHelper<RouteHeaderField>();
            CountTestHelper<ServerHeaderField>();
            CountTestHelper<SupportedHeaderField>();
            CountTestHelper<UnsupportedHeaderField>();
            CountTestHelper<UserAgentHeaderField>();
     //       CountTestHelper<ViaHeaderField>();
            CountTestHelper<WarningHeaderField>();
             
            CountTestHelperSecurity<AuthorizationHeaderField>();
            CountTestHelperSecurity<ProxyAuthenticateHeaderField>();
            CountTestHelperSecurity<ProxyAuthorizationHeaderField>();
            CountTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        public void CountTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            int actual;
            int expected = 0;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = 1;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = 2;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Clear();
            expected = 0;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        public void CountTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            int actual;
            int expected = 0;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = 1;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = 2;
            actual = target.Count;
            Assert.AreEqual(expected, actual);

            target.Clear();
            expected = 0;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EqualsTest()
        {
            EqualsTestHelper<ExtensionHeaderField>();
            EqualsTestHelper<AcceptHeaderField>();
            EqualsTestHelper<AcceptEncodingHeaderField>();
            EqualsTestHelper<AcceptLanguageHeaderField>();
            EqualsTestHelper<AlertInfoHeaderField>();
            EqualsTestHelper<AllowHeaderField>();
            EqualsTestHelper<CallInfoHeaderField>();
            EqualsTestHelper<ContactHeaderField>();
            EqualsTestHelper<ContentEncodingHeaderField>();
            EqualsTestHelper<ContentLanguageHeaderField>();
            EqualsTestHelper<ErrorInfoHeaderField>();
            EqualsTestHelper<InReplyToHeaderField>();
            EqualsTestHelper<ProxyRequireHeaderField>();
            EqualsTestHelper<RecordRouteHeaderField>();
            EqualsTestHelper<RequireHeaderField>();
            EqualsTestHelper<RouteHeaderField>();
            EqualsTestHelper<ServerHeaderField>();
            EqualsTestHelper<SupportedHeaderField>();
            EqualsTestHelper<UnsupportedHeaderField>();
            EqualsTestHelper<UserAgentHeaderField>();
          //  EqualsTestHelper<ViaHeaderField>();
            EqualsTestHelper<WarningHeaderField>();
            // 
            EqualsTestHelperSecurity<AuthorizationHeaderField>();
            EqualsTestHelperSecurity<ProxyAuthenticateHeaderField>();
            EqualsTestHelperSecurity<ProxyAuthorizationHeaderField>();
            EqualsTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        [TestMethod]
        public void EqualsTest1()
        {
            EqualsTest1Helper<ExtensionHeaderField>();
            EqualsTest1Helper<AcceptHeaderField>();
            EqualsTest1Helper<AcceptEncodingHeaderField>();
            EqualsTest1Helper<AcceptLanguageHeaderField>();
            EqualsTest1Helper<AlertInfoHeaderField>();
            EqualsTest1Helper<AllowHeaderField>();
            EqualsTest1Helper<CallInfoHeaderField>();
            EqualsTest1Helper<ContactHeaderField>();
            EqualsTest1Helper<ContentEncodingHeaderField>();
            EqualsTest1Helper<ContentLanguageHeaderField>();
            EqualsTest1Helper<ErrorInfoHeaderField>();
            EqualsTest1Helper<InReplyToHeaderField>();
            EqualsTest1Helper<ProxyRequireHeaderField>();
            EqualsTest1Helper<RecordRouteHeaderField>();
            EqualsTest1Helper<RequireHeaderField>();
            EqualsTest1Helper<RouteHeaderField>();
            EqualsTest1Helper<ServerHeaderField>();
            EqualsTest1Helper<SupportedHeaderField>();
            EqualsTest1Helper<UnsupportedHeaderField>();
            EqualsTest1Helper<UserAgentHeaderField>();
          //  EqualsTest1Helper<ViaHeaderField>();
            EqualsTest1Helper<WarningHeaderField>();
            // 
            EqualsTest1HelperSecurity<AuthorizationHeaderField>();
            EqualsTest1HelperSecurity<ProxyAuthenticateHeaderField>();
            EqualsTest1HelperSecurity<ProxyAuthorizationHeaderField>();
            EqualsTest1HelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        public void EqualsTest1Helper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            HeaderFieldGroup<T> target1 = new HeaderFieldGroup<T>();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = target1;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            other = target1;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        public void EqualsTest1HelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            AuthHeaderFieldGroup<T> target1 = new AuthHeaderFieldGroup<T>();
            HeaderFieldBase other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = target1;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            other = target1;
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EqualsTest2()
        {
            EqualsTest2Helper<ExtensionHeaderField>();
            EqualsTest2Helper<AcceptHeaderField>();
            EqualsTest2Helper<AcceptEncodingHeaderField>();
            EqualsTest2Helper<AcceptLanguageHeaderField>();
            EqualsTest2Helper<AlertInfoHeaderField>();
            EqualsTest2Helper<AllowHeaderField>();
            EqualsTest2Helper<CallInfoHeaderField>();
            EqualsTest2Helper<ContactHeaderField>();
            EqualsTest2Helper<ContentEncodingHeaderField>();
            EqualsTest2Helper<ContentLanguageHeaderField>();
            EqualsTest2Helper<ErrorInfoHeaderField>();
            EqualsTest2Helper<InReplyToHeaderField>();
            EqualsTest2Helper<ProxyRequireHeaderField>();
            EqualsTest2Helper<RecordRouteHeaderField>();
            EqualsTest2Helper<RequireHeaderField>();
            EqualsTest2Helper<RouteHeaderField>();
            EqualsTest2Helper<ServerHeaderField>();
            EqualsTest2Helper<SupportedHeaderField>();
            EqualsTest2Helper<UnsupportedHeaderField>();
            EqualsTest2Helper<UserAgentHeaderField>();
        //    EqualsTest2Helper<ViaHeaderField>();
            EqualsTest2Helper<WarningHeaderField>();
            // 
            EqualsTest2HelperSecurity<AuthorizationHeaderField>();
            EqualsTest2HelperSecurity<ProxyAuthenticateHeaderField>();
            EqualsTest2HelperSecurity<ProxyAuthorizationHeaderField>();
            EqualsTest2HelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        public void EqualsTest2Helper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            target.Add(new T());
            HeaderFieldGroup<T> target1 = new HeaderFieldGroup<T>();
            object obj = target1;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            obj = target1;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            obj = target1;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        public void EqualsTest2HelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            target.Add(new T());
            AuthHeaderFieldGroup<T> target1 = new AuthHeaderFieldGroup<T>();
            object obj = target1;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            obj = target1;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

            target1.Add(new T());
            obj = target1;
            expected = false;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        public void EqualsTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            HeaderFieldGroup<T> other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new HeaderFieldGroup<T>();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Add(new T());
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        public void EqualsTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            AuthHeaderFieldGroup<T> other = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other = new AuthHeaderFieldGroup<T>();
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            other.Add(new T());
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);

            target.Add(new T());
            expected = true;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetHeaderField()
        {
            GetHeaderFieldTestHelper<ExtensionHeaderField>();
            GetHeaderFieldTestHelper<AcceptHeaderField>();
            GetHeaderFieldTestHelper<AcceptEncodingHeaderField>();
            GetHeaderFieldTestHelper<AcceptLanguageHeaderField>();
            GetHeaderFieldTestHelper<AlertInfoHeaderField>();
            GetHeaderFieldTestHelper<AllowHeaderField>();
            GetHeaderFieldTestHelper<CallInfoHeaderField>();
            GetHeaderFieldTestHelper<ContactHeaderField>();
            GetHeaderFieldTestHelper<ContentEncodingHeaderField>();
            GetHeaderFieldTestHelper<ContentLanguageHeaderField>();
            GetHeaderFieldTestHelper<ErrorInfoHeaderField>();
            GetHeaderFieldTestHelper<InReplyToHeaderField>();
            GetHeaderFieldTestHelper<ProxyRequireHeaderField>();
            GetHeaderFieldTestHelper<RecordRouteHeaderField>();
            GetHeaderFieldTestHelper<RequireHeaderField>();
            GetHeaderFieldTestHelper<RouteHeaderField>();
            GetHeaderFieldTestHelper<ServerHeaderField>();
            GetHeaderFieldTestHelper<SupportedHeaderField>();
            GetHeaderFieldTestHelper<UnsupportedHeaderField>();
            GetHeaderFieldTestHelper<UserAgentHeaderField>();
        //    GetHeaderFieldTestHelper<ViaHeaderField>();
            GetHeaderFieldTestHelper<WarningHeaderField>();
            // 
            GetHeaderFieldTestHelperSecurity<AuthorizationHeaderField>();
            GetHeaderFieldTestHelperSecurity<ProxyAuthenticateHeaderField>();
            GetHeaderFieldTestHelperSecurity<ProxyAuthorizationHeaderField>();
            GetHeaderFieldTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for GetFirstValue
        ///</summary>
        public void GetHeaderFieldTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            T expected = new T();
            T actual;
            target.Add(new T());
            actual = target.GetHeaderField(0);
            Assert.AreEqual(expected, actual);
        }

        public void GetHeaderFieldTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            T expected = new T();
            T actual;
            target.Add(new T());
            actual = target.GetHeaderField(0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void HeadersTest()
        {
            HeadersTestHelper<ExtensionHeaderField>();
            HeadersTestHelper<AcceptHeaderField>();
            HeadersTestHelper<AcceptEncodingHeaderField>();
            HeadersTestHelper<AcceptLanguageHeaderField>();
            HeadersTestHelper<AlertInfoHeaderField>();
            HeadersTestHelper<AllowHeaderField>();
            HeadersTestHelper<CallInfoHeaderField>();
            HeadersTestHelper<ContactHeaderField>();
            HeadersTestHelper<ContentEncodingHeaderField>();
            HeadersTestHelper<ContentLanguageHeaderField>();
            HeadersTestHelper<ErrorInfoHeaderField>();
            HeadersTestHelper<InReplyToHeaderField>();
            HeadersTestHelper<ProxyRequireHeaderField>();
            HeadersTestHelper<RecordRouteHeaderField>();
            HeadersTestHelper<RequireHeaderField>();
            HeadersTestHelper<RouteHeaderField>();
            HeadersTestHelper<ServerHeaderField>();
            HeadersTestHelper<SupportedHeaderField>();
            HeadersTestHelper<UnsupportedHeaderField>();
            HeadersTestHelper<UserAgentHeaderField>();
           // HeadersTestHelper<ViaHeaderField>();
            HeadersTestHelper<WarningHeaderField>();
            //
            //HeadersTestHelperSecurity<AuthenticationInfoHeaderField>();
            //HeadersTestHelperSecurity<AuthorizationHeaderField>();
            //HeadersTestHelperSecurity<ProxyAuthenticateHeaderField>();
            //HeadersTestHelperSecurity<ProxyAuthorizationHeaderField>();
            //HeadersTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Headers
        ///</summary>
        public void HeadersTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup_Accessor<T> target = new HeaderFieldGroup_Accessor<T>();
            target.Add(new T());
            ArrayList expected = new ArrayList();
            expected.Add(new T());
            ArrayList actual;
            actual = target.Headers;
            Assert.AreEqual(expected[0], actual[0]);

            expected.Add(new T());
            target.Headers = expected;
            actual = target.Headers;
            Assert.AreEqual(expected, actual);
        }

        public void HeadersTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup_Accessor<T> target = new AuthHeaderFieldGroup_Accessor<T>();
            target.Add(new T());
            ArrayList expected = new ArrayList();
            expected.Add(new T());
            ArrayList actual;
            actual = target.Headers;
            Assert.AreEqual(expected[0], actual[0]);

            expected.Add(new T());
            target.Headers = expected;
            actual = target.Headers;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsSynchronizedTest()
        {
            IsSynchronizedTestHelper<ExtensionHeaderField>();
            IsSynchronizedTestHelper<AcceptHeaderField>();
            IsSynchronizedTestHelper<AcceptEncodingHeaderField>();
            IsSynchronizedTestHelper<AcceptLanguageHeaderField>();
            IsSynchronizedTestHelper<AlertInfoHeaderField>();
            IsSynchronizedTestHelper<AllowHeaderField>();
            IsSynchronizedTestHelper<CallInfoHeaderField>();
            IsSynchronizedTestHelper<ContactHeaderField>();
            IsSynchronizedTestHelper<ContentEncodingHeaderField>();
            IsSynchronizedTestHelper<ContentLanguageHeaderField>();
            IsSynchronizedTestHelper<ErrorInfoHeaderField>();
            IsSynchronizedTestHelper<InReplyToHeaderField>();
            IsSynchronizedTestHelper<ProxyRequireHeaderField>();
            IsSynchronizedTestHelper<RecordRouteHeaderField>();
            IsSynchronizedTestHelper<RequireHeaderField>();
            IsSynchronizedTestHelper<RouteHeaderField>();
            IsSynchronizedTestHelper<ServerHeaderField>();
            IsSynchronizedTestHelper<SupportedHeaderField>();
            IsSynchronizedTestHelper<UnsupportedHeaderField>();
            IsSynchronizedTestHelper<UserAgentHeaderField>();
          //  IsSynchronizedTestHelper<ViaHeaderField>();
            IsSynchronizedTestHelper<WarningHeaderField>();
            // 
            IsSynchronizedTestHelperSecurity<AuthorizationHeaderField>();
            IsSynchronizedTestHelperSecurity<ProxyAuthenticateHeaderField>();
            IsSynchronizedTestHelperSecurity<ProxyAuthorizationHeaderField>();
            IsSynchronizedTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for IsSynchronized
        ///</summary>
        public void IsSynchronizedTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            bool actual;
            actual = target.IsSynchronized;
            Assert.AreEqual(false, actual);
        }

        public void IsSynchronizedTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            bool actual;
            actual = target.IsSynchronized;
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void ItemTest()
        {
            ItemTestHelper<ExtensionHeaderField>();
            ItemTestHelper<AcceptHeaderField>();
            ItemTestHelper<AcceptEncodingHeaderField>();
            ItemTestHelper<AcceptLanguageHeaderField>();
            ItemTestHelper<AlertInfoHeaderField>();
            ItemTestHelper<AllowHeaderField>();
            ItemTestHelper<CallInfoHeaderField>();
            ItemTestHelper<ContactHeaderField>();
            ItemTestHelper<ContentEncodingHeaderField>();
            ItemTestHelper<ContentLanguageHeaderField>();
            ItemTestHelper<ErrorInfoHeaderField>();
            ItemTestHelper<InReplyToHeaderField>();
            ItemTestHelper<ProxyRequireHeaderField>();
            ItemTestHelper<RecordRouteHeaderField>();
            ItemTestHelper<RequireHeaderField>();
            ItemTestHelper<RouteHeaderField>();
            ItemTestHelper<ServerHeaderField>();
            ItemTestHelper<SupportedHeaderField>();
            ItemTestHelper<UnsupportedHeaderField>();
            ItemTestHelper<UserAgentHeaderField>();
        //    ItemTestHelper<ViaHeaderField>();
            ItemTestHelper<WarningHeaderField>();
            // 
            ItemTestHelperSecurity<AuthorizationHeaderField>();
            ItemTestHelperSecurity<ProxyAuthenticateHeaderField>();
            ItemTestHelperSecurity<ProxyAuthorizationHeaderField>();
            ItemTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        [TestMethod]
        public void ItemTest1()
        {
            ItemTest1Helper<ExtensionHeaderField>();
            ItemTest1Helper<AcceptHeaderField>();
            ItemTest1Helper<AcceptEncodingHeaderField>();
            ItemTest1Helper<AcceptLanguageHeaderField>();
            ItemTest1Helper<AlertInfoHeaderField>();
            ItemTest1Helper<AllowHeaderField>();
            ItemTest1Helper<CallInfoHeaderField>();
            ItemTest1Helper<ContactHeaderField>();
            ItemTest1Helper<ContentEncodingHeaderField>();
            ItemTest1Helper<ContentLanguageHeaderField>();
            ItemTest1Helper<ErrorInfoHeaderField>();
            ItemTest1Helper<InReplyToHeaderField>();
            ItemTest1Helper<ProxyRequireHeaderField>();
            ItemTest1Helper<RecordRouteHeaderField>();
            ItemTest1Helper<RequireHeaderField>();
            ItemTest1Helper<RouteHeaderField>();
            ItemTest1Helper<ServerHeaderField>();
            ItemTest1Helper<SupportedHeaderField>();
            ItemTest1Helper<UnsupportedHeaderField>();
            ItemTest1Helper<UserAgentHeaderField>();
         //   ItemTest1Helper<ViaHeaderField>();
            ItemTest1Helper<WarningHeaderField>();
            // 
            ItemTest1HelperSecurity<AuthorizationHeaderField>();
            ItemTest1HelperSecurity<ProxyAuthenticateHeaderField>();
            ItemTest1HelperSecurity<ProxyAuthorizationHeaderField>();
            ItemTest1HelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest1Helper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
            T expected = new T();
            string fieldValue = expected.GetStringValue();
            T actual;
            target[fieldValue, comparisonType] = expected;
            actual = target[fieldValue, comparisonType];
            Assert.IsNull(actual);

            target.Add(expected);
            target[fieldValue, comparisonType] = expected;
            actual = target[fieldValue, comparisonType];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected,actual);
        }

        public void ItemTest1HelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
            T expected = new T();
            string fieldValue = expected.GetStringValue();
            T actual;
            target[fieldValue, comparisonType] = expected;
            actual = target[fieldValue, comparisonType];
            Assert.IsNull(actual);

            target.Add(expected);
            target[fieldValue, comparisonType] = expected;
            actual = target[fieldValue, comparisonType];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void ItemTest2()
        {
            ItemTest2Helper<ExtensionHeaderField>();
            ItemTest2Helper<AcceptHeaderField>();
            ItemTest2Helper<AcceptEncodingHeaderField>();
            ItemTest2Helper<AcceptLanguageHeaderField>();
            ItemTest2Helper<AlertInfoHeaderField>();
            ItemTest2Helper<AllowHeaderField>();
            ItemTest2Helper<CallInfoHeaderField>();
            ItemTest2Helper<ContactHeaderField>();
            ItemTest2Helper<ContentEncodingHeaderField>();
            ItemTest2Helper<ContentLanguageHeaderField>();
            ItemTest2Helper<ErrorInfoHeaderField>();
            ItemTest2Helper<InReplyToHeaderField>();
            ItemTest2Helper<ProxyRequireHeaderField>();
            ItemTest2Helper<RecordRouteHeaderField>();
            ItemTest2Helper<RequireHeaderField>();
            ItemTest2Helper<RouteHeaderField>();
            ItemTest2Helper<ServerHeaderField>();
            ItemTest2Helper<SupportedHeaderField>();
            ItemTest2Helper<UnsupportedHeaderField>();
            ItemTest2Helper<UserAgentHeaderField>();
         //   ItemTest2Helper<ViaHeaderField>();
            ItemTest2Helper<WarningHeaderField>();
            //
            //ItemTest2HelperSecurity<AuthenticationInfoHeaderField>();
            //ItemTest2HelperSecurity<AuthorizationHeaderField>();
            //ItemTest2HelperSecurity<ProxyAuthenticateHeaderField>();
            //ItemTest2HelperSecurity<ProxyAuthorizationHeaderField>();
            //ItemTest2HelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest2Helper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup_Accessor<T> target = new HeaderFieldGroup_Accessor<T>();
            int index = 0;
            T expected = new T();
            T actual;
            actual = target[index];
            Assert.IsNull(actual);

            target.Add(expected);
            target[index] = expected;
            actual = target[index];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        public void ItemTest2HelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup_Accessor<T> target = new AuthHeaderFieldGroup_Accessor<T>();
            int index = 0;
            T expected = new T();
            T actual;
            actual = target[index];
            Assert.IsNull(actual);

            target.Add(expected);
            target[index] = expected;
            actual = target[index];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();
            string fieldValue = string.Empty;
            T expected = new T();
            T actual;
            actual = target[fieldValue];
            Assert.IsNull(actual);

            target.Add(expected);
            fieldValue = expected.GetStringValue();
            target[fieldValue] = expected;
            actual = target[fieldValue];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        public void ItemTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
            string fieldValue = string.Empty;
            T expected = new T();
            T actual;
            actual = target[fieldValue];
            Assert.IsNull(actual);

            target.Add(expected);
            fieldValue = expected.GetStringValue();
            target[fieldValue] = expected;
            actual = target[fieldValue];
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            RemoveAtTestHelper<ExtensionHeaderField>();
            RemoveAtTestHelper<AcceptHeaderField>();
            RemoveAtTestHelper<AcceptEncodingHeaderField>();
            RemoveAtTestHelper<AcceptLanguageHeaderField>();
            RemoveAtTestHelper<AlertInfoHeaderField>();
            RemoveAtTestHelper<AllowHeaderField>();
            RemoveAtTestHelper<CallInfoHeaderField>();
            RemoveAtTestHelper<ContactHeaderField>();
            RemoveAtTestHelper<ContentEncodingHeaderField>();
            RemoveAtTestHelper<ContentLanguageHeaderField>();
            RemoveAtTestHelper<ErrorInfoHeaderField>();
            RemoveAtTestHelper<InReplyToHeaderField>();
            RemoveAtTestHelper<ProxyRequireHeaderField>();
            RemoveAtTestHelper<RecordRouteHeaderField>();
            RemoveAtTestHelper<RequireHeaderField>();
            RemoveAtTestHelper<RouteHeaderField>();
            RemoveAtTestHelper<ServerHeaderField>();
            RemoveAtTestHelper<SupportedHeaderField>();
            RemoveAtTestHelper<UnsupportedHeaderField>();
            RemoveAtTestHelper<UserAgentHeaderField>();
        //    RemoveAtTestHelper<ViaHeaderField>();
            RemoveAtTestHelper<WarningHeaderField>();
            //
            //RemoveAtTestHelperSecurity<AuthenticationInfoHeaderField>();
            //RemoveAtTestHelperSecurity<AuthorizationHeaderField>();
            //RemoveAtTestHelperSecurity<ProxyAuthenticateHeaderField>();
            //RemoveAtTestHelperSecurity<ProxyAuthorizationHeaderField>();
            //RemoveAtTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for RemoveAt
        ///</summary>
        public void RemoveAtTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();

            target.Add(new T());
            Assert.IsTrue(target.Count == 1);
            int index = 0;
            target.RemoveAt(index);
            Assert.IsTrue(target.Count == 0);
        }

        public void RemoveAtTestHelperSecurity<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup<T> target = new HeaderFieldGroup<T>();

            target.Add(new T());
            Assert.IsTrue(target.Count == 1);
            int index = 0;
            target.RemoveAt(index);
            Assert.IsTrue(target.Count == 0);
        }

        [TestMethod]
        [DeploymentItem("Konnetic.Sip.dll")]
        public void SeperatorTest()
        {
            SeperatorTestHelper<ExtensionHeaderField>();
            SeperatorTestHelper<AcceptHeaderField>();
            SeperatorTestHelper<AcceptEncodingHeaderField>();
            SeperatorTestHelper<AcceptLanguageHeaderField>();
            SeperatorTestHelper<AlertInfoHeaderField>();
            SeperatorTestHelper<AllowHeaderField>();
            SeperatorTestHelper<CallInfoHeaderField>();
            SeperatorTestHelper<ContactHeaderField>();
            SeperatorTestHelper<ContentEncodingHeaderField>();
            SeperatorTestHelper<ContentLanguageHeaderField>();
            SeperatorTestHelper<ErrorInfoHeaderField>();
            SeperatorTestHelper<InReplyToHeaderField>();
            SeperatorTestHelper<ProxyRequireHeaderField>();
            SeperatorTestHelper<RecordRouteHeaderField>();
            SeperatorTestHelper<RequireHeaderField>();
            SeperatorTestHelper<RouteHeaderField>();
            SeperatorTestHelper<ServerHeaderField>();
            SeperatorTestHelper<SupportedHeaderField>();
            SeperatorTestHelper<UnsupportedHeaderField>();
            SeperatorTestHelper<UserAgentHeaderField>();
         //   SeperatorTestHelper<ViaHeaderField>();
            SeperatorTestHelper<WarningHeaderField>();
            //
            //SeperatorTestHelperSecurity<AuthenticationInfoHeaderField>();
            //SeperatorTestHelperSecurity<AuthorizationHeaderField>();
            //SeperatorTestHelperSecurity<ProxyAuthenticateHeaderField>();
            //SeperatorTestHelperSecurity<ProxyAuthorizationHeaderField>();
            //SeperatorTestHelperSecurity<WwwAuthenticateHeaderField>();
        }

        /// <summary>
        ///A test for Seperator
        ///</summary>
        public void SeperatorTestHelper<T>()
            where T : HeaderFieldBase, new()
        {
            HeaderFieldGroup_Accessor<T> target = new HeaderFieldGroup_Accessor<T>();
            string expected = "\r\n";
            string actual;
            target.Seperator = expected;
            actual = target.Seperator;
            Assert.AreEqual(expected, actual);

            target = new HeaderFieldGroup_Accessor<T>(";");
            expected = ";";
            actual = target.Seperator;
            Assert.AreEqual(expected, actual);
        }

        public void SeperatorTestHelperSecurity<T>()
            where T : SecurityHeaderFieldBase, new()
        {
            AuthHeaderFieldGroup_Accessor<T> target = new AuthHeaderFieldGroup_Accessor<T>();
            string expected = "\r\n";
            string actual;
            target.Seperator = expected;
            actual = target.Seperator;
            Assert.AreEqual(expected, actual);
            PrivateObject p = new PrivateObject(";");
            target = new AuthHeaderFieldGroup_Accessor<T>(p);
            expected = ";";
            actual = target.Seperator;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetTest()
        {
            HeaderFieldGroup<AcceptLanguageHeaderField> target = new HeaderFieldGroup<AcceptLanguageHeaderField>();
            AcceptLanguageHeaderField oldHeader = new AcceptLanguageHeaderField("fr");
            AcceptLanguageHeaderField newHeader = new AcceptLanguageHeaderField("en");
            target.Add(oldHeader);
            Assert.AreEqual(target[0], oldHeader);
            target.Set(oldHeader, newHeader);
            Assert.AreEqual(target[0], newHeader);
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