using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ITutoringOfferService : ICrudService<TutoringOffer>
	{
		void DeleteAll();
	}
}