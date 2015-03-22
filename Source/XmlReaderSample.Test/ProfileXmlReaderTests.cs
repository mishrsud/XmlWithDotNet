using Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlReaderSample.Test
{
	[TestClass]
	public class ProfileXmlReaderTests
	{
		public TestContext TestContext { get; set; }
		private static ProfileXmlReader _profileXmlReader;
		private static Profile _profile;

		[ClassInitialize]
		public static void Initialize(TestContext testContext)
		{
			_profileXmlReader = new ProfileXmlReader();
			_profile = _profileXmlReader.ParseXmlToObject();
		}

		[TestMethod, TestCategory("Integration")]
		public void XmlReaderWriter_CanReadContentsOfEmbeddedFile()
		{
			_profileXmlReader.RawFileContent.Should().NotBeNull("because the file exists");
		}

		[TestMethod, TestCategory("Integration")]
		public void Reader_SetsPersonName_Correctly()
		{
			_profile.PersonName.Should().Be("Sudhanshu Mishra", "because that's what we set it to");
		}

		[TestMethod, TestCategory("Integration")]
		public void Reader_SetsSummary_Correctly()
		{
			_profile.Summary.Should().NotBeNull("because it is not empty or null");
		}

		[TestMethod, TestCategory("Integration")]
		public void Reader_SetsEducationSectionCorrectly()
		{
			_profile.Education.Should().NotBeNull()
				.And
				.HaveCount(2);
		}

		[TestMethod, TestCategory("Integration")]
		public void Reader_Sets_EmploymentSectionCorrectly()
		{
			_profile.EmploymentHistory.Should().NotBeNull()
				.And
				.HaveCount(2);
		}
	}
}
