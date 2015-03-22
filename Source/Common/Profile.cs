namespace Common
{
	/// <summary>
	/// Represents the profile of a person
	/// </summary>
	public class Profile
	{
		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		public string PersonName { get; set; }

		/// <summary>
		/// Gets or sets the summary.
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// Gets or sets the schools.
		/// </summary>
		public Education Education { get; set; }

		/// <summary>
		/// Gets or sets the employment history.
		/// </summary>
		public EmploymentHistory EmploymentHistory { get; set; }
	}
}
