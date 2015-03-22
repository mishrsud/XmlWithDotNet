using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Common;

namespace XmlWriterSample
{
	/// <summary>
	/// Provides methods to write an XML from a <see cref="Profile"/> object
	/// </summary>
	public class ProfileXmlWriter
	{
		private readonly XmlWriter _internalXmlWriter;
		private readonly Profile _profileObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProfileXmlWriter" /> class.
		/// </summary>
		/// <param name="profileObject">The profile object.</param>
		/// <param name="outputPath">The output path.</param>
		/// <param name="writeWithIndent">if set to <c>true</c> [write with indent].</param>
		/// <exception cref="System.ArgumentNullException">profileObject</exception>
		/// <exception cref="System.ArgumentNullException">outputPath</exception>
		/// <exception cref="System.ArgumentException">Invalid path specified. The path should be a valid path including a file name</exception>
		public ProfileXmlWriter(
			Profile profileObject,
			string outputPath, 
			bool writeWithIndent = true)
		{
			if (profileObject == null) throw new ArgumentNullException("profileObject");
			if (string.IsNullOrEmpty(outputPath)) throw new ArgumentNullException("outputPath");
			if (!IsValidPath(outputPath)) throw new ArgumentException("Invalid path specified. The path should be a valid path including a file name");
			_profileObject = profileObject;
			_internalXmlWriter = CreateWriter(outputPath, writeWithIndent);
		}

		/// <summary>
		/// Writes the XML.
		/// </summary>
		/// <returns>true if the XML was written, false otherwise</returns>
		/// <exception cref="System.ArgumentNullException">profile</exception>
		public bool WriteXml()
		{
			_internalXmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
			_internalXmlWriter.WriteStartElement("profile");
			_internalXmlWriter.WriteAttributeString("personName", _profileObject.PersonName);

			_internalXmlWriter.WriteElementString("summary", _profileObject.Summary);
			_internalXmlWriter.WriteStartElement("education");

			foreach (var school in _profileObject.Education)
			{
				_internalXmlWriter.WriteStartElement("school");
				_internalXmlWriter.WriteAttributeString("name", school.Name);
				_internalXmlWriter.WriteAttributeString("certificateType", school.Certificate.ToString());
				_internalXmlWriter.WriteElementString("headline", school.Headline);
				_internalXmlWriter.WriteElementString("description", school.Description);
				_internalXmlWriter.WriteStartElement("start");
				_internalXmlWriter.WriteAttributeString("month", school.StartMonth.ToString(CultureInfo.InvariantCulture));
				_internalXmlWriter.WriteAttributeString("year", school.StartYear.ToString(CultureInfo.InvariantCulture));
				_internalXmlWriter.WriteEndElement();
				_internalXmlWriter.WriteStartElement("end");
				_internalXmlWriter.WriteAttributeString("month", school.EndMonth.ToString(CultureInfo.InvariantCulture));
				_internalXmlWriter.WriteAttributeString("year", school.EndYear.ToString(CultureInfo.InvariantCulture));
				_internalXmlWriter.WriteEndElement();
				_internalXmlWriter.WriteEndElement(); 
			}

			// close education
			_internalXmlWriter.WriteEndElement();

			_internalXmlWriter.WriteStartElement("employmentHistory");
			foreach (var position in _profileObject.EmploymentHistory)
			{
				_internalXmlWriter.WriteStartElement("position");
				_internalXmlWriter.WriteAttributeString("title", position.Title);
				_internalXmlWriter.WriteAttributeString("employer", position.Employer);
				_internalXmlWriter.WriteAttributeString("tenure", position.Tenure);
				_internalXmlWriter.WriteAttributeString("isCurrent", position.IsCurrent.ToString());

				_internalXmlWriter.WriteElementString("role", position.Role);
				_internalXmlWriter.WriteElementString("responsibilities", position.Responsibilities);
				_internalXmlWriter.WriteElementString("achievements", position.Achievements);
				_internalXmlWriter.WriteElementString("technologies", position.Technologies);
				_internalXmlWriter.WriteEndElement();
			}

			// close employmentHistory
			_internalXmlWriter.WriteEndElement();

			// close _profileObject
			_internalXmlWriter.WriteEndElement();

			_internalXmlWriter.Close();
			return true;
		}

		/// <summary>
		/// Determines whether [is valid path] [the specified output path].
		/// </summary>
		/// <param name="outputPath">The output path.</param>
		/// <returns></returns>
		private bool IsValidPath(string outputPath)
		{
			var fileInfo = new FileInfo(outputPath);
			return fileInfo.Directory != null && fileInfo.Directory.Exists;
		}

		/// <summary>
		/// Creates the writer.
		/// </summary>
		/// <param name="outputPath">The output path.</param>
		/// <param name="writeWithIndent">if set to <c>true</c> [write with indent].</param>
		/// <returns></returns>
		private XmlWriter CreateWriter(string outputPath, bool writeWithIndent)
		{
			var writerSettings = new XmlWriterSettings()
			{
				Encoding = new UTF8Encoding(),
				Indent = writeWithIndent,
			};

			var writer = XmlWriter.Create(outputPath, writerSettings);
			return writer;
		}
	}
}
