using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models

{
	public class Person
	{
		// Entity attributes
		[Key]
		public int PersonId { get; set; }

		[DataType(DataType.Text)]
		public string Name { get; set; }

		[DataType(DataType.Text)]
		public string LastName { get; set; }

		public int Semester { get; set; }


		// Navigation attributes
		public int UniversityId { get; set; }
		public virtual University University { get; set; }

		public virtual User User { get; set; }

		public virtual Student Student { get; set; }

		public virtual Tutor Tutor { get; set; }

		public virtual List<Qualification> QualificationsReceived { get; set; } = new List<Qualification>();

		public virtual List<Qualification> QualificationsGiven { get; set; } = new List<Qualification>();

		public virtual List<Suscription> Suscriptions { get; set; } = new List<Suscription>();


		// Methods
		public Person() { }


		public override string ToString()
		{
			return $"Person {{ Id: {PersonId}, Name: {Name}, LastName: {LastName}, Semester: {Semester} }}";
		}

		public string FullName
		{
			get
			{
				return $"{Name} {LastName}";
			}
		}
	}
}