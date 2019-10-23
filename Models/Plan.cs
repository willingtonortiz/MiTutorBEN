using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int Duration { get; set; }


        public virtual List<Suscription> Suscriptions {get; set;} = new List<Suscription>();


        public override string ToString()
        {
            return $"Plan {{ Id: {PlanId}, Name: {Name}, Duration {Duration} }}";
        }
    }
}