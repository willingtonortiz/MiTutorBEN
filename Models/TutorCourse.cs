namespace MiTutorBEN.Models
{
	public class TutorCourse
	{
		public int TutorId { get; set; }
		public virtual Tutor Tutor { get; set; }

		public int CourseId { get; set; }
		public virtual Course Course { get; set; }

	}
}