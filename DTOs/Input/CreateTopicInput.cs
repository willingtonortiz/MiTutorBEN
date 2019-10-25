using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.DTOs.Input
{
	public class CreateTopicInput
	{
        [Required]
		public int CourseId { get; set; }
        [Required]
        [DataType(DataType.Text)]
		public string Name { get; set; }
	}
}