using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, UpdateUserDTO>();

        }
    }
}
