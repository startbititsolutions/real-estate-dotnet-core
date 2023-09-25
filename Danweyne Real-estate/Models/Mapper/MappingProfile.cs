using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.View_Model;

namespace Danweyne_Real_estate.Models.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
      
            CreateMap<UserRegistrationModel, ApplicationUser>()
                .ForMember(u => u.UserName  , opt => opt.MapFrom(x => x.Email));
        }
    }
}
