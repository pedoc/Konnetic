using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for OptionHeaderFieldAdapter and is intended
    ///to contain all OptionHeaderFieldAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class OptionHeaderFieldAdapter
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
		///A test for Option
		///</summary>
		[TestMethod()]
		public void OptionTest()
			{
			OptionHeaderFieldBase target = CreateOptionHeaderField();  
			string expected = string.Empty;  
			string actual;
			actual = target.Option;
			Assert.AreEqual(expected, actual);

			target.Option = expected;
			actual = target.Option;
			Assert.AreEqual(expected, actual);

			expected = Common.TOKEN;
			target.Option = expected;
			actual = target.Option;
			Assert.AreEqual(expected, actual);
			}

		public void OptionTypeTest1()
			{
			for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
				{
				string val = new string(Common.TOKENRESERVED[i], 1);
				Assert.IsTrue(OptionThrowsError(val), "Exception Not thrown on: " + val);
				}
			}
		private bool OptionThrowsError(string val)
			{
			try
				{
				OptionHeaderFieldBase target = CreateOptionHeaderField();  
				target.Option = val;
				}
			catch(SipFormatException)
				{
				return true;
				}
			return false;
			}
		/// <summary>
		///A test for GetStringValue
		///</summary>
		[TestMethod()]
		public void GetStringValueTest()
			{
			OptionHeaderFieldBase target = CreateOptionHeaderField();  
			string expected = string.Empty;  
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target.Option = Common.TOKEN;
			expected = Common.TOKEN; 
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target.Option = Common.NUMBER;
			expected = Common.NUMBER;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual); 
			}

		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod()]
		public void ParseTest()
			{
			OptionHeaderFieldBase target = CreateOptionHeaderField();  
			string value = string.Empty;
			target.Parse(value);
			string expected = string.Empty;
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = Common.TOKEN;
			target.Parse(value);
			expected = Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "\tReqUire\t:\t\r\n " + Common.TOKEN;
			target.Parse(value);
			expected = Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "Require: \r\n ";
			target.Parse(value);
			expected = "";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);


			}

		internal virtual OptionHeaderFieldBase CreateOptionHeaderField()
			{
			OptionHeaderFieldBase target = new RequireHeaderField();
			return target;
			}

		/// <summary>
		///A test for IsValid
		///</summary>
		[TestMethod()]
		public void IsValidTest()
			{
			OptionHeaderFieldBase target = CreateOptionHeaderField();  
			bool expected = false;  
			bool actual;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.Option="100rel";
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
			OptionHeaderFieldBase target = CreateOptionHeaderField();  
			OptionHeaderFieldBase other = null;  
			bool expected = false; 
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = CreateOptionHeaderField();  
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.Option = "abc";
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			
			other.Option = "abc";
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);


			}
		}
}
