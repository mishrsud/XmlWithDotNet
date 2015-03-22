using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqToXmlSample.Test
{
	[TestClass]
	public class XDocumentReaderTests
	{
		[TestMethod, TestCategory("Unit, LinqToXml")]
		public void Reader_Throws_ArgumentNullException_IfArgumentIsNull()
		{
			Action act = () => new XDocumentReader(null);
			act.ShouldThrow<ArgumentNullException>("because the argument is null");
		}

		[TestMethod, TestCategory("Unit, LinqToXml")]
		public void Reader_Throws_ArgumentException_IfArgumentIsInvalidPath()
		{
			Action act = () => new XDocumentReader("C:\\doesnotexist.xml");
			act.ShouldThrow<ArgumentException>("because the argument is invalid");
		}

		[TestMethod, TestCategory("Unit, LinqToXml")]
		public void Reader_CanReadXml_IfValidPathSpecified()
		{
			var pathToXml = Path.Combine(GetExecutingDirectoryName(), "Data", "PurchaseOrder.xml");
			var reader = new XDocumentReader(pathToXml);

			reader.ReadXml().Should().BeTrue("because the xml is valid and exists");
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
