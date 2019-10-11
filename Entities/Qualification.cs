using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Entities
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
		public Person Adresser { get; set; }


		public int AdresseeId { get; set; }
		public Person Adressee { get; set; }


		public int TutoringSessionId { get; set; }
		public TutoringSession TutoringSession { get; set; }


		public Qualification() { }
	}
}