using Domain.Dtos;

namespace Application.Mappings;

public static class IocMappings
{
    public static IocReadDto ToReadDto(this Domain.Models.Ioc ioc)
    {
        return new IocReadDto
        {
            Id = ioc.Id,
            Sha256 = ioc.Sha256,
            Sha1 = ioc.Sha1,
            Md5 = ioc.Md5,
            Mcafee = ioc.Mcafee,
            Engines = ioc.Engines,
        };
    }

    public static Domain.Models.Ioc ToModel(this IocCreateDto iocCreateDto)
    {
        return new Domain.Models.Ioc
        {
            Sha256 = iocCreateDto.Sha256,
            Sha1 = iocCreateDto.Sha1,
            Md5 = iocCreateDto.Md5,
            Mcafee = iocCreateDto.Mcafee,
            Engines = iocCreateDto.Engines,
        };
    }

    public static Domain.Models.Ioc ToModel(this IocUpdateDto iocUpdateDto)
    {
        return new Domain.Models.Ioc
        {
            Sha256 = iocUpdateDto.Sha256,
            Sha1 = iocUpdateDto.Sha1,
            Md5 = iocUpdateDto.Md5,
            Mcafee = iocUpdateDto.Mcafee,
            Engines = iocUpdateDto.Engines,
        };
    }
}
