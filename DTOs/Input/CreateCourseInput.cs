using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.DTOs.Input
{
	public class CreateCourseInput
	{
		[Required]
		public int UniversityId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Name { get; set; }
	}
}