using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
    public interface IUniversityService : ICrudService<University>
    {
        University FindByName(string name);
        void DeleteAll();
    }
}