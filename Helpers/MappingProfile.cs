using AutoMapper;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.Models;

namespace MiTutorBEN.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TutoringOffer,TutoringOfferRequest>();
            CreateMap<TutoringOfferRequest,TutoringOffer>();
        }
    }
}