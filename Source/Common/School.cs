namespace Common
{
	/// <summary>
	/// Represents the details of a particular school
	/// </summary>
	public class School
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the certificate.
		/// </summary>
		public CertificateType Certificate { get; set; }

		/// <summary>
		/// Gets or sets the headline.
		/// </summary>
		public string Headline { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the start month.
		/// </summary>
		public int StartMonth { get; set; }

		/// <summary>
		/// Gets or sets the start year.
		/// </summary>
		public int StartYear { get; set; }

		/// <summary>
		/// Gets or sets the end month.
		/// </summary>
		public int EndMonth { get; set; }

		/// <summary>
		/// Gets or sets the end year.
		/// </summary>
		public int EndYear { get; set; }
	}
}
