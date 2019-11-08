using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ITutoringOfferService : ICrudService<TutoringOffer>
	{
		Task<IEnumerable<TutoringOffer>> FindByUniversityIdAndCourseId(int universityId, int courseId);
		Task<TutoringOffer> FindWithSessions(int tutoringOfferId);
		void Save();
	}
}