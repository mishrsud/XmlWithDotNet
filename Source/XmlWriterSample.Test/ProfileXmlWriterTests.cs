using System;
using System.Configuration;
using System.IO;
using Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlWriterSample.Test
{
	[TestClass]
	public class ProfileXmlWriterTests
	{
		private static string _testFilePath = "C:\\profile.xml";
		private static string _validFilePath;
		private static Profile _profileObject;
		private static string _invalidFilePath;

		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void Initialize(TestContext testContext)
		{
			_validFilePath = ConfigurationManager.AppSettings["test:validFilePath"];
			_testFilePath = ConfigurationManager.AppSettings["test:testFilePath"];
			_invalidFilePath = ConfigurationManager.AppSettings["test:invalidFilePath"];
			_profileObject = ProfileObjectMother.Create();
		}

		[TestMethod, TestCategory("Integration, Writer")]
		public void Writer_ThrowsArgumentException_IfInvalidPathSpecified()
		{
			Action act = () => new ProfileXmlWriter(_profileObject, _invalidFilePath);
			act.ShouldThrow<ArgumentException>();
		}

		[TestMethod, TestCategory("Integration, Writer")]
		public void Writer_ThrowsArgumentNullException_WhenAttemptToWriteNull()
		{
			Action act = () =>  new ProfileXmlWriter(null, _testFilePath, true);

			act.ShouldThrow<ArgumentNullException>("because we cannot write if object is null");
		}

		[TestMethod, TestCategory("Integration, Writer")]
		public void Writer_CanWriteXml_WithValidObjectAndPath()
		{
			var writer = new ProfileXmlWriter(_profileObject, _validFilePath, true);
			writer.WriteXml()
				.Should()
				.BeTrue("because we expect the XML to be written");
		}

		[TestCleanup]
		public void Cleanup()
		{
			if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
			//if (File.Exists(_validFilePath)) File.Delete(_validFilePath);
		}
	}
}
