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
		public University University { get; set; }

		public User User { get; set; }

		public Student Student { get; set; }

		public Tutor Tutor { get; set; }

		public List<Qualification> QualificationsReceived { get; set; } = new List<Qualification>();

		public List<Qualification> QualificationsGiven { get; set; } = new List<Qualification>();

		public List<Suscription> Suscriptions { get; set; } = new List<Suscription>();


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