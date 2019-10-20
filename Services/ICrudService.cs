using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiTutorBEN.Services
{
	public interface ICrudService<T>
	{
		Task<T> FindById(int id);
		Task<IEnumerable<T>> FindAll();
		Task<T> Create(T t);
		Task<T> Update(int id, T t);
		Task<T> DeleteById(int id);
		Task<int> DeleteAll();
	}
}
