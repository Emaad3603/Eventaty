using AutoMapper;
using EventReservation.DAL.Models;
using EventReservation.PL.ViewModels;
namespace EventReservation.PL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EventsViewModel,Events>().ReverseMap();
        }
    }
}
