using System.Threading.Tasks;
using MiTutorBEN.Models;
using System.Collections.Generic;

namespace MiTutorBEN.Services
{
    public interface IQualificationService : ICrudService<Qualification>
    {

        Task<IEnumerable<Qualification>> FindAllQualificationsByTutor(int tutorId);
    }
}