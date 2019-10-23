using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
	public class Qualification
	{
		[Key]
		public int QualificationId { get; set; }
		public int Rate { get; set; }

		[DataType(DataType.Text)]
		public string Comment { get; set; }

		[DataType(DataType.Text)]
		public string AdresserRole { get; set; }


		public int AdresserId { get; set; }
		public virtual Person Adresser { get; set; }


		public int AdresseeId { get; set; }
		public virtual Person Adressee { get; set; }


		public int TutoringSessionId { get; set; }
		public virtual TutoringSession TutoringSession { get; set; }


		public Qualification() { }
	}
}