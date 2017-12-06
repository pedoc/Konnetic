using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SecondsHeaderFieldAdapter and is intended
    ///to contain all SecondsHeaderFieldAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class SecondsHeaderFieldAdapter
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

		
		[TestMethod()]
		[ExpectedException(typeof(SipOutOfRangeException))]
		public void SecondsTest2()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField(); 
			long expected = -1;
			target.Seconds = expected;
			}
		[TestMethod()]
		[ExpectedException(typeof(SipOutOfRangeException))]
		public void SecondsTest1()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();
			double e = System.Math.Pow(2,32);
			long expected = (long)e;
			target.Seconds = expected;
			}
		/// <summary>
		///A test for Seconds
		///</summary>
		[TestMethod()]
		public void SecondsTest()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();
			long? expected = 0;
			long? actual;
			target.Seconds = expected;
			actual = target.Seconds;
			Assert.AreEqual(expected, actual);

			double e = System.Math.Pow(2, 32)-1;
			expected = (long)e; 
			target.Seconds = expected;
			actual = target.Seconds;
			Assert.AreEqual(expected, actual);
			 
			expected = 10000;
			target.Seconds = expected;
			actual = target.Seconds;
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for MinSeconds
		///</summary>
		[TestMethod()]
		public void MinSecondsTest()
			{
			long actual;
			actual = SecondsHeaderFieldBase.MinSeconds;
			Assert.AreEqual(0, actual);
			}

		/// <summary>
		///A test for MaxSeconds
		///</summary>
		[TestMethod()]
		public void MaxSecondsTest()
			{
			long actual;
			actual = SecondsHeaderFieldBase.MaxSeconds;
			double e = System.Math.Pow(2, 32) - 1;
			long expected = (long)e;
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for GetStringValue
		///</summary>
		[TestMethod()]
		public void GetStringValueTest()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();
			string expected = "0";
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target.Seconds=1234567890;
			expected = "1234567890";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

            target = new ExpiresHeaderField();
             expected = ""; 
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod()]
		public void ParseTest()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();
			string value = string.Empty;
			target.Parse(value);
			string expected = "";
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "\t1234567890";
			target.Parse(value);
			expected = "1234567890";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "\r\n 1234567890i777\t";
			target.Parse(value);
			expected = "1234567890";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for IsValid
		///</summary>
		[TestMethod()]
		public void IsValidTest()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();  
			bool expected = true;  
			bool actual;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual); 
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest2()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();
			HeaderFieldBase other = null;
			bool expected = false;
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = CreateSecondsHeaderField();
			expected = true; 
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			((SecondsHeaderFieldBase)other).Seconds = 1;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.Seconds = 1;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.Seconds = 0;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			((SecondsHeaderFieldBase)other).Seconds = 0;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest1()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();  
			SecondsHeaderFieldBase other = null;  
			bool expected = false; 
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = CreateSecondsHeaderField();  
			other.Seconds = 100000001;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.Seconds = 100000001;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual); 
			}

		internal virtual SecondsHeaderFieldBase CreateSecondsHeaderField()
			{ 
			SecondsHeaderFieldBase target = new ExpiresHeaderField(0);
			return target;
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest()
			{
			SecondsHeaderFieldBase target = CreateSecondsHeaderField();  
			object obj = null;  
			bool expected = false;  
			bool actual;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

			obj = CreateSecondsHeaderField();  
			expected = true; 
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

			((SecondsHeaderFieldBase)obj).Seconds = 10;
			expected = false;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

			target.Seconds = 10;
			expected = true;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual); 


			}
		}
}
