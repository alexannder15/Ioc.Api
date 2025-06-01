using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Profiles;

public class IocsProfile : Profile
{
    public IocsProfile()
    {
        //Source -> Target
        CreateMap<Ioc, IocReadDto>();
        CreateMap<IocCreateDto, Ioc>();
        CreateMap<IocUpdateDto, Ioc>();
    }
}
