using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ITutoringSessionService : ICrudService<TutoringSession>
	{

		void SaveAsync();
	}
}