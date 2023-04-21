using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles
{
    public class BorrowTransactionProfile : Profile
    {
        public BorrowTransactionProfile()
        {
            CreateMap<BorrowTransaction, BorrowTransactionDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LibraryItemTitle, opt => opt.MapFrom(src => src.LibraryItem.Title));
        }
    }
}
