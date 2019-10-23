using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
    public class Suscription
    {
        [Key]
        public int SuscriptionId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        
        
        public int PersonId { get; set; }
        public virtual Person Person {get; set;}
    

        public int PlanId {get; set;}
        public virtual Plan Plan {get; set;}



        public override string ToString()
        {
            return $"Suscription {{ Id: {SuscriptionId}, StartTime: {StartTime}, EndTime {EndTime} }}";
        }
    }
}