using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for AuthHeaderFieldGroupAdapter and is intended
    ///to contain all AuthHeaderFieldGroupAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class AuthHeaderFieldGroupAdapter
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
		///A test for Parse
		///</summary>
		public void ParseTestHelper<T>()
			where T : SecurityHeaderFieldBase, new()
			{
			AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>(); // TODO: Initialize to an appropriate value
			string value = string.Empty; // TODO: Initialize to an appropriate value
			target.Parse(value);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
			}

		[TestMethod()]
		public void ParseTest()
			{
			Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
					"Please call ParseTestHelper<T>() with appropriate type parameters.");
			}

		/// <summary>
		///A test for Clone
		///</summary>
		public void CloneTestHelper<T>()
			where T : SecurityHeaderFieldBase, new()
			{
			AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>(); // TODO: Initialize to an appropriate value
			HeaderFieldBase expected = null; // TODO: Initialize to an appropriate value
			HeaderFieldBase actual;
			actual = target.Clone();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
			}

		[TestMethod()]
		public void CloneTest()
			{
			Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
					"Please call CloneTestHelper<T>() with appropriate type parameters.");
			}

		/// <summary>
		///A test for AuthHeaderFieldGroup`1 Constructor
		///</summary>
		public void AuthHeaderFieldGroupConstructorTestHelper<T>()
			where T : SecurityHeaderFieldBase, new()
			{
			AuthHeaderFieldGroup<T> target = new AuthHeaderFieldGroup<T>();
			Assert.Inconclusive("TODO: Implement code to verify target");
			}

		[TestMethod()]
		public void AuthHeaderFieldGroupConstructorTest()
			{
			Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
					"Please call AuthHeaderFieldGroupConstructorTestHelper<T>() with appropriate " +
					"type parameters.");
			}
		}
}
