using AutoMapper;
using Ispit.Todo._28062024.Models.Dbo;
using Ispit.Todo._28062024.Models.ViewModels;

namespace Ispit.Todo._28062024.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}
