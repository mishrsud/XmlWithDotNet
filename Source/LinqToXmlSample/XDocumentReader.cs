using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXmlSample
{
	/// <summary>
	/// Uses LINQ to XML to read an XML document
	/// </summary>
	public class XDocumentReader
	{
		private string _pathToXml;

		/// <summary>
		/// Initializes a new instance of the <see cref="XDocumentReader"/> class.
		/// </summary>
		/// <param name="pathToXml">The path to XML.</param>
		/// <exception cref="System.ArgumentNullException">pathToXml</exception>
		/// <exception cref="System.ArgumentException">Invalid or non existent path</exception>
		public XDocumentReader(string pathToXml)
		{
			if (string.IsNullOrEmpty(pathToXml)) throw new ArgumentNullException("pathToXml");
			if (!File.Exists(pathToXml)) throw new ArgumentException("Invalid or non existent path");
			_pathToXml = pathToXml;
		}

		public bool ReadXml()
		{
			XElement xElement = XElement.Load(_pathToXml);

			/*
			 * Query Syntax
			 * IEnumerable<XElement> address =
											from el in xElement.Elements("Address")
											where (string)el.Attribute("Type") == "Billing"
											select el;
			 */
			IEnumerable<XElement> address = xElement
												.Elements("Address")
												.Where(el => (string) el.Attribute("Type") == "Billing");

			var value = address.Descendants("Name").First().Value.ToString();
			return true;
		}
	}
}
