
using AutoMapper;
using LHShop.Data;
using LHShop.ViewModels;

namespace LHShop.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, Khachhang>();
/*                .ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
                .ReverseMap();*/
        }
    }
}
