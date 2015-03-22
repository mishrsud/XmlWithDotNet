using System;
using System.IO;
using System.Xml;

namespace XmlDocumentSample
{
	/// <summary>
	/// Provides methods to read an xml using <see cref="XmlDocument"/>
	/// </summary>
	/// <remarks>
	/// XPath reference: http://www.w3schools.com/xpath/xpath_syntax.asp
	/// SO: http://stackoverflow.com/a/4532084/190476
	/// </remarks>
	public class XmlDocumentReader
	{
		private string _pathToXml;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlDocumentReader"/> class.
		/// </summary>
		/// <param name="pathToXml">The path to XML.</param>
		/// <exception cref="System.ArgumentException">Invalid pathToXml</exception>
		public XmlDocumentReader(string pathToXml)
		{
			if (!File.Exists(pathToXml)) throw new ArgumentException("Invalid pathToXml");
			_pathToXml = pathToXml;
		}

		/// <summary>
		/// Reads the XML.
		/// </summary>
		public bool ReadXml()
		{
			var xmlDocument = new XmlDocument();
			xmlDocument.Load(_pathToXml);
			var personName = xmlDocument.SelectSingleNode("//profile/@personName").InnerText;
			var summary = xmlDocument.SelectSingleNode("//profile/summary").InnerText;

			var schools = xmlDocument.SelectNodes("//profile/education/school");
			foreach (XmlNode xmlNode in schools)
			{
				var name = xmlNode.Attributes["name"].InnerText;
				var headline = xmlNode["headline"].InnerText;
			}

			return true;
		}
	}
}
