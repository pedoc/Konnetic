using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for QValueHeaderFieldAdapter and is intended
    ///to contain all QValueHeaderFieldAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class QValueHeaderFieldAdapter
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
		public void QValueTest()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();
			float expected = -0.1F; 
			target.QValue = expected;
			}

		[TestMethod()]
		[ExpectedException(typeof(SipOutOfRangeException))]
		public void QValueTest1()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();
			float expected = 1.1F;
			target.QValue = expected;
			}
		/// <summary>
		///A test for QValue
		///</summary>
		[TestMethod()]
		public void QValueTest2()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();
            float? expected = null;  
			float? actual;
			target.QValue = expected;
			actual = target.QValue;
			Assert.AreEqual(expected, actual);

			expected = 0.999F;
			target.QValue = expected;
			actual = target.QValue;
			Assert.AreEqual(expected, actual);

			}

		/// <summary>
		///A test for ParseQValue
		///</summary>
		[TestMethod()]
		public void ParseQValueTest()
			{
			string value = string.Empty;  
			float? expected = null;  
			float? actual;
			actual = QValueHeaderFieldBase.ParseQValue(value);
			Assert.AreEqual(expected, actual);

			value = "q=0.001";
			expected = 0.001F;
			actual = QValueHeaderFieldBase.ParseQValue(value);
			Assert.AreEqual(expected, actual);

			value = "q=1.000";
			expected = 1F;
			actual = QValueHeaderFieldBase.ParseQValue(value);
			Assert.AreEqual(expected, actual); 
			}

		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod()]
		public void ParseTest()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();  
			string value = string.Empty; 
			target.Parse(value);
			string expected = "";
			string actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "\tcoding\t q=0.55699\t";
			target.Parse(value);
			expected = "0.557";
			actual = target.QValue.ToString();
			Assert.AreEqual(expected, actual); 

			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest2()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();  
			HeaderFieldBase other = null;  
			bool expected = false;  
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = CreateQValueHeaderField(); 
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			((QValueHeaderFieldBase)other).QValue = 0.500F;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.QValue = 0.500F;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			
			((QValueHeaderFieldBase)other).QValue = 1.000F;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.QValue = 1F;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			
			((QValueHeaderFieldBase)other).QValue = 0.000F;
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.QValue = 0F;
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
			QValueHeaderFieldBase target = CreateQValueHeaderField();
			QValueHeaderFieldBase other = null;
			bool expected = false;
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			other = CreateQValueHeaderField();
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			((QValueHeaderFieldBase)other).QValue = 1.000F; 
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.QValue = 1.000F;
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
			}

		internal virtual QValueHeaderFieldBase CreateQValueHeaderField()
			{ 
			QValueHeaderFieldBase target = new AcceptEncodingHeaderField("coding");
			return target;
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest()
			{
			QValueHeaderFieldBase target = CreateQValueHeaderField();
			object obj = null;
			bool expected = false;
			bool actual;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

			obj = CreateQValueHeaderField();
			((QValueHeaderFieldBase)obj).QValue = 0.000F;
			expected = false;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

            target.QValue = 0.000F;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);

			obj = CreateQValueHeaderField();
			((QValueHeaderFieldBase)obj).QValue = 0.001F;
			expected = false;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);

			target.QValue = 0.001F;
			expected = true;
			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
			}
		}
}
