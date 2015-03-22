using Common;

namespace XmlWriterSample.Test
{
	internal class ProfileObjectMother
	{
		internal static Profile Create()
		{
			var profile = new Profile()
			{
				PersonName = "Sudhanshu Mishra",
				Summary = "Sudhanshu is a pragmatic, passionate and result oriented developer par excellence."
			};

			var school1 = new School()
			{
				Name = "Visveswaraiah Technological University",
				Certificate = CertificateType.Degree,
				Headline = "Bachelor of Engineering",
				Description = "Major in Telecommunication",
				StartMonth = 7,
				StartYear = 1999,
				EndMonth = 5,
				EndYear = 2003
			};

			var school2 = new School()
			{
				Name = "St. Xavier's",
				Certificate = CertificateType.HighSchool,
				Headline = "High School",
				Description = "Physics, Chemistry, Matchs and English",
				StartMonth = 4,
				StartYear = 1996,
				EndMonth = 4,
				EndYear = 1997
			};

			Education education = new Education() { school1, school2 };

			profile.Education = education;

			var position1 = new Position()
			{
				Title = "Senior Developer",
				IsCurrent = true,
				Tenure = "2011 - 2015",
				Employer = "Saxo Bank A/S",
				Role = "Senior Developer, Lead",
				Responsibilities = "Leading the Open API team in India",
				Achievements = "Super cool",
				Technologies = ".NET, C#, ASP.NET MVC, Web API, MSSQL 2014"
			};

			EmploymentHistory employmentHistory = new EmploymentHistory() { position1 };
			profile.EmploymentHistory = employmentHistory;

			return profile;
		}
	}
}
