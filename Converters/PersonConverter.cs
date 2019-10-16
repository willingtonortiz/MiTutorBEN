using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class PersonConverter : IConverter<Person, PersonDTO>
	{
		public Person FromDto(PersonDTO dto)
		{
			Person person = new Person
			{
				PersonId = dto.PersonId,
				Name = dto.Name,
				LastName = dto.Lastname,
				Semester = dto.Semester
			};

			return person;
		}

		public PersonDTO FromEntity(Person entity)
		{
			PersonDTO personDTO = new PersonDTO
			{
				PersonId = entity.PersonId,
				Name = entity.Name,
				Lastname = entity.LastName,
				Semester = entity.Semester
			};

			return personDTO;
		}
	}
}