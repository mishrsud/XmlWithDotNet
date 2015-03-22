using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlDocumentSample.Test
{
	[TestClass]
	public class XmlDocumentReaderTests
	{
		[TestMethod, TestCategory("Integration, XmlDocument")]
		public void XmlDocumentReader_Throws_ArgumentException_WhenInvalidPathSpecified()
		{
			Action act = () => new XmlDocumentReader("Q:\\invalid.xml");
			act.ShouldThrow<ArgumentException>("because the path is invalid");
		}

		[TestMethod, TestCategory("Integration, XmlDocument")]
		public void XmlDocumentReader_CanReadXml_WithValidPath()
		{
			var fileName = Path.Combine(GetExecutingDirectoryName(), "Data", "Profile.xml");
			var xmlDocumentReader = new XmlDocumentReader(fileName);
			xmlDocumentReader.ReadXml()
				.Should()
				.BeTrue("because we know the XML is alright");
		}

		/// <summary>
		/// Gets the name of the directory of executing assembly.
		/// </summary>
		/// <returns></returns>
		public static string GetExecutingDirectoryName()
		{
			var location = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
			return new FileInfo(location.AbsolutePath).Directory.FullName;
		}
	}
}
