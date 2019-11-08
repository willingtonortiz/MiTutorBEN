using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.DTOs.Input
{
	public class TutoringSessionStudentResponse
	{
        public int StudentId {get;set;}
        public int TutoringSessionId {get;set;}
	}
}