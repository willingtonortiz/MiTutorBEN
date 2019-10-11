using System.Collections.Generic;

namespace MiTutorBEN.Services
{
	public interface ICrudService<T>
	{
		T FindById(int id);
		IEnumerable<T> FindAll();
		T Create(T t);
		T Update(T t);
		T DeleteById(int id);
	}
}
