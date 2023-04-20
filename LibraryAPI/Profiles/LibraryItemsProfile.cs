using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles
{
    public class LibraryItemsProfile : Profile
    {
        public LibraryItemsProfile()
        {
            CreateMap<LibraryItem, LibraryItemDTO>();
            CreateMap<LibraryItemDTO, LibraryItem>();
            CreateMap<LibraryItemBasicDTO, LibraryItem>();
            CreateMap<LibraryItem, LibraryItemBasicDTO>();

        }
    }
}
