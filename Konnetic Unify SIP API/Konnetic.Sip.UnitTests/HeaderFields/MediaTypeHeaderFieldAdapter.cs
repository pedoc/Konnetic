using Konnetic.Sip.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Konnetic.Sip.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for MediaTypeHeaderFieldAdapter and is intended
    ///to contain all MediaTypeHeaderFieldAdapter Unit Tests
    ///</summary>
	[TestClass()]
	public class MediaTypeHeaderFieldAdapter
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
		///A test for MediaType
		///</summary>
		[TestMethod()]
		public void MediaTypeTest()
			{
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();
			string expected = "application";
			string actual;
			actual = target.MediaType;
			Assert.AreEqual(expected, actual);

			target.MediaType = expected;
			actual = target.MediaType;
			Assert.AreEqual(expected, actual);

			expected = Common.TOKEN;
			target.MediaType = expected;
			actual = target.MediaType;
			Assert.AreEqual(expected, actual);
			}

		public void MediaTypeTest1()
			{
			for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
				{
				string val = new string(Common.TOKENRESERVED[i], 1);
				Assert.IsTrue(MediaThrowsError(val), "Exception Not thrown on: " + val);
				}
			}
		private bool MediaThrowsError(string val)
			{
			try
				{
				MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();
				target.MediaType = val;
				}
			catch(SipFormatException)
				{
				return true;
				}
			return false;
			}
		/// <summary>
		///A test for MediaSubType
		///</summary>
		[TestMethod()]
		public void MediaSubTypeTest()
			{
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();  
			string expected = "sdp";  
			string actual;
			actual = target.MediaSubType;
			Assert.AreEqual(expected, actual);

			target.MediaSubType = expected;
			actual = target.MediaSubType;
			Assert.AreEqual(expected, actual);

			expected = Common.TOKEN;
			target.MediaSubType = expected;
			actual = target.MediaSubType;
			Assert.AreEqual(expected, actual);
			}

		public void MediaSubTypeTest1()
			{
			for(int i = 0; i < Common.TOKENRESERVED.Length; i++)
				{
				string val = new string(Common.TOKENRESERVED[i], 1);
				Assert.IsTrue(MediaSubTypeThrowsError(val), "Exception Not thrown on: " + val);
				}
			}
		private bool MediaSubTypeThrowsError(string val)
			{
			try
				{
				MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();
				target.MediaSubType = val;
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
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();
			string expected = "application/sdp";
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target = CreateMediaTypeHeaderField1();
			expected = "audio/*";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target.MediaSubType = Common.TOKEN;
			expected = "audio/"+Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			target.MediaType = Common.TOKEN;
			expected =  Common.TOKEN+"/"+Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			((AcceptHeaderField)target).QValue = 0.5f;
			expected =  Common.TOKEN + "/" + Common.TOKEN +  ";q=0.5";
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			HeaderFieldGroup<AcceptHeaderField> hfg = new HeaderFieldGroup<AcceptHeaderField>();
			hfg.Add((AcceptHeaderField)target);

			expected = Common.TOKEN + "/" + Common.TOKEN + ";q=0.5";
			actual = hfg.GetStringValue();
			Assert.AreEqual(expected, actual);

			hfg.Add(new AcceptHeaderField(MediaType.All,"all"));


			expected = Common.TOKEN + "/" + Common.TOKEN + ";q=0.5, */all";
			actual = hfg.GetStringValue();
			Assert.AreEqual(expected, actual);

			}

		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod()]
		public void ParseTest()
			{
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField(); 
			string value = string.Empty;  
			target.Parse(value);
			string expected = "";
			string actual;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "all/aLl";
			target.Parse(value);
            expected = "all/aLl";
			actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

            value = "*/*";
            target.Parse(value);
            expected = "*/*";
            actual = target.GetStringValue();
            Assert.AreEqual(expected, actual);

			value = "*/"+Common.TOKEN;
			target.Parse(value);
			expected = "*/" + Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = Common.TOKEN + "/" + Common.TOKEN;
			target.Parse(value);
			expected = Common.TOKEN + "/" + Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);

			value = "\t \r\n "+Common.TOKEN + "/" + Common.TOKEN;
			target.Parse(value);
			expected = Common.TOKEN + "/" + Common.TOKEN;
			actual = target.GetStringValue();
			Assert.AreEqual(expected, actual);
			}

		/// <summary>
		///A test for IsValid
		///</summary>
		[TestMethod()]
		public void IsValidTest()
			{
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();  
			bool expected = true;  
			bool actual;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.MediaSubType = "";
			target.MediaType = "123";
			expected = false; 
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.MediaSubType = "all";
			expected = true;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.MediaSubType = "*";
			expected = true;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.MediaType = "*";
			expected = true;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);

			target.MediaType = "";
			expected = false;
			actual = target.IsValid();
			Assert.AreEqual(expected, actual);
			}

		internal virtual MediaTypeHeaderFieldBase CreateMediaTypeHeaderField()
			{ 
			MediaTypeHeaderFieldBase target = new AcceptHeaderField();
			return target;
			}
		internal virtual MediaTypeHeaderFieldBase CreateMediaTypeHeaderField1()
			{
			MediaTypeHeaderFieldBase target = new AcceptHeaderField(MediaType.Audio,"*");
			return target;
			}

		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest()
			{
			MediaTypeHeaderFieldBase target = CreateMediaTypeHeaderField();
			MediaTypeHeaderFieldBase other = CreateMediaTypeHeaderField1();   
			bool expected = false; 
			bool actual;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.MediaSubType = "*";
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.MediaType = "image";
			expected = false;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.MediaType = "Audio";
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);

			target.MediaType = "audio";
			expected = true;
			actual = target.Equals(other);
			Assert.AreEqual(expected, actual); 
			}
		}
}
