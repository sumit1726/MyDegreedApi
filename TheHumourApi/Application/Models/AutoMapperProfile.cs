using Application.Core.Models;
using Application.Models.DTOs;
using AutoMapper;

namespace Application.Models;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Humour, HumourDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.HumourId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(s => s.HumourText));
        CreateMap<PagedHumour, PagedHumourDto>();
    }
}