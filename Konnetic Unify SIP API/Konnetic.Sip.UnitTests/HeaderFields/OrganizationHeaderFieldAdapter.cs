using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for OrganizationHeaderFieldAdapter and is intended
    ///to contain all OrganizationHeaderFieldAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class OrganizationHeaderFieldAdapter
		{


		private TestContext testContextInstance;

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

		#region Additional test attributes
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
		#endregion


		/// <summary>
		///A test for Organization
		///</summary>
		[TestMethod()]
		public void OrganizationTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();  
			string expected = string.Empty;  
			string actual;
			actual = target.Organization;
			Assert.AreEqual(expected, actual);

			target.Organization = expected;
			actual = target.Organization;
			Assert.AreEqual(expected, actual);

			expected = "Boxes by búb";  
			target.Organization = expected;
			actual = target.Organization;
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for GetStringValue
		///</summary>
		[TestMethod()]
		public void GetStringValueTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();  
			string expected = string.Empty;  
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			expected = "Boxes by búb";
			target.Organization = expected;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			expected = "Boxes \r\n by búb";
			target.Organization = expected;
			expected = "Boxes by búb";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual); 
			}

		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod()]
		public void ParseTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField(); 
			string value = string.Empty;  
			target.Parse(value);
			string expected = string.Empty;
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = " \t Boxes \r\n\t  \r\n by búb";
			target.Parse(value);
			expected = "Boxes by búb";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = " Organization: Organization: Boxes \r\n by \r\nbúb\t";
			target.Parse(value);
			expected = "Organization: Boxes by búb";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for IsValid
		///</summary>
		[TestMethod()]
		public void IsValidTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();  
			bool expected = false;  
			bool actual;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			string value = " Organization: Organization: Boxes \r\n by búb";
			target.Parse(value);
			expected = true; 
			actual = target.IsValid();
			Assert.AreEqual(expected, actual); 
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();
			OrganizationHeaderField other = null;
			bool expected = false;
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = new OrganizationHeaderField();
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other.Organization = "R úúúúúúúúúúúúúúúúúúúúúúúúú" + Common.MARK;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.Organization = "R\r\n úúúúúúúúúúúúúúúúúúúúúúúúú" + Common.MARK;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for Clone
		///</summary>
		[TestMethod()]
		public void CloneTest()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();
			HeaderFieldBase expected = new OrganizationHeaderField();    
			HeaderFieldBase actual;
			actual = target.Clone();
			Assert.AreEqual(expected, actual);

			((OrganizationHeaderField)expected).Organization = "úúúúúúú   úúúúúúúúúúúúú \r\n úúúúúú"; 
			actual = target.Clone();
			Assert.AreNotEqual(expected, actual);

			target.Organization = "úúúúúúú   úúúúúúúúúúúúú úúúúúú";
			actual = target.Clone();
			Assert.AreEqual(expected, actual);

			((OrganizationHeaderField)expected).Organization = "";
			actual = target.Clone();
			Assert.AreNotEqual(expected, actual);

			target.Organization = "";
			actual = target.Clone();
			Assert.AreEqual(expected, actual); 

			
 
			}

		/// <summary>
		///A test for OrganizationHeaderField Constructor
		///</summary>
		[TestMethod()]
		public void OrganizationHeaderFieldConstructorTest1()
			{
			OrganizationHeaderField target = new OrganizationHeaderField();
			Assert.IsTrue(target.Organization == string.Empty);
			}

		/// <summary>
		///A test for OrganizationHeaderField Constructor
		///</summary>
		[TestMethod()]
		public void OrganizationHeaderFieldConstructorTest()
			{
			string organization = "úúúúúúú   úúúúúúúúúúúúú \r\n úúúúúú";
			OrganizationHeaderField target = new OrganizationHeaderField(organization);
			Assert.IsTrue(target.Organization == "úúúúúúú   úúúúúúúúúúúúú úúúúúú");
			}
		}
}
