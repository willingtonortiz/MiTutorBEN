using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.DTOs.Input
{
    public class CreateTutoringSessionStudent
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TutoringSessionId { get; set; }


    }
}