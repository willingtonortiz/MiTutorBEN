using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ITutoringSessionStudentService : ICrudService<StudentTutoringSession>
	{

		Task <IEnumerable<StudentTutoringSession>> findSessionsByUserId(int userId);

	}
}