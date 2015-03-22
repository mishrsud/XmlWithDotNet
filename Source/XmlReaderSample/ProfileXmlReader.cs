using System;
using System.IO;
using System.Reflection;
using System.Xml;
using Common;

namespace XmlReaderSample
{
	public class ProfileXmlReader
	{
		private const string EmbeddedFilePath = "XmlReaderSample.Data.Profile.xml";
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ProfileXmlReader"/> class.
		/// </summary>
		public ProfileXmlReader()
		{
			RawFileContent = new StreamReader(GetTextFromEmbededResource(EmbeddedFilePath)).ReadToEnd();
		}

		/// <summary>
		/// Gets the content of the raw file.
		/// </summary>
		/// <value>
		/// The content of the raw file.
		/// </value>
		public string RawFileContent { get; private set; }


		/// <summary>
		/// Parses the XML.
		/// </summary>
		public Profile ParseXmlToObject()
		{
			var inputStream = GetTextFromEmbededResource(EmbeddedFilePath);
			XmlReader reader = XmlReader.Create(inputStream);
			var profile = new Profile();

			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element) continue;
				if (reader.Name == "profile")
				{
					profile.PersonName = reader.GetAttribute("personName");
					continue;
				}

				if (reader.Name == "summary")
				{
					profile.Summary = reader.ReadElementContentAsString();
					continue;
				}

				if (reader.Name == "education")
				{
					var education = new Education();
					ReadEducationSection(reader, education);
					profile.Education = education;
				}

				if (reader.Name == "employmentHistory")
				{
					var employmentHistory = new EmploymentHistory();

					ReadEmploymentHistorySection(reader, employmentHistory);
					profile.EmploymentHistory = employmentHistory;
				}
			}
			
			return profile;
		}

		private void ReadEmploymentHistorySection(XmlReader reader, EmploymentHistory employmentHistory)
		{
			var position = new Position();
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "employmentHistory") break;
				if (reader.NodeType != XmlNodeType.Element) continue;
				
				if (reader.Name == "position" && reader.HasAttributes)
				{
					position.Title = reader.GetAttribute("title");
					position.Employer = reader.GetAttribute("employer");
					position.Tenure = reader.GetAttribute("tenure");
					position.IsCurrent = Convert.ToBoolean(reader.GetAttribute("isCurrent"));
					continue;
				}

				if (reader.Name == "role")
				{
					position.Role = reader.ReadElementString();
					continue;
				}

				if (reader.Name == "responsibilities")
				{
					position.Responsibilities = reader.ReadElementContentAsString();
					continue;
				}

				if (reader.Name == "achivements")
				{
					position.Achievements = reader.ReadElementContentAsString();
					continue;
				}

				if (reader.Name == "technologies")
				{
					position.Technologies = reader.ReadElementContentAsString();
				}

				employmentHistory.Add(position);
			}
		}

		/// <summary>
		/// Reads the education section from the profile XML.
		/// </summary>
		/// <param name="reader">The XML reader.</param>
		/// <param name="education">The education.</param>
		private static void ReadEducationSection(XmlReader reader, Education education)
		{
			var school = new School();

			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element) continue;
				if (reader.Name == "employmentHistory") break;
				if (reader.Name == "school" && reader.HasAttributes)
				{
					school.Name = reader.GetAttribute("name");
					CertificateType certificateType;
					Enum.TryParse(reader.GetAttribute("certificateType"), true, out certificateType);
					school.Certificate = certificateType;
					continue;
				}

				if (reader.Name == "headline")
				{
					school.Headline = reader.ReadElementContentAsString();
					continue;
				}

				if (reader.Name == "description")
				{
					school.Description = reader.ReadElementContentAsString();
					continue;
				}

				if (reader.Name == "start" && reader.HasAttributes)
				{
					school.StartMonth = Convert.ToInt32(reader.GetAttribute("month"));
					school.StartYear = Convert.ToInt32(reader.GetAttribute("year"));
					continue;
				}

				if (reader.Name == "end" && reader.HasAttributes)
				{
					school.EndMonth = Convert.ToInt32(reader.GetAttribute("month"));
					school.EndYear = Convert.ToInt32(reader.GetAttribute("year"));
				}

				education.Add(school);
			}
		}

		/// <summary>
		/// Gets the text from embeded resource.
		/// </summary>
		/// <param name="fullyQualifiedFileName">Name of the fully qualified file.</param>
		/// <returns></returns>
		private static Stream GetTextFromEmbededResource(string fullyQualifiedFileName)
		{
			var assembly = Assembly.GetExecutingAssembly();

			var stream = assembly.GetManifestResourceStream(fullyQualifiedFileName);
			if (stream == null) return null;

			return stream;
		}
	}
}
