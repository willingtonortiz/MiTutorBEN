using System;
using System.Collections.Generic;

namespace MiTutorBEN.DTOs.Responses
{
    public class TutoringSessionResponse
    {
        public int TutoringSessionId{ get; set;}
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StudentCount {get; set;}
        public string Description {get; set;}
        public double Price {get; set;}
        public int TutorId {get; set;}
       

        public List<String> Topics {get; set;} = new List<string>();
        public List<PersonDTO> Students {get; set;} = new List<PersonDTO>();

    }
}