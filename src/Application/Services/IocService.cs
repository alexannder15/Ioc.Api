using Application.Exceptions;
using Application.Interfaces;
using Application.Mappings;
using Domain.Dtos;
using Domain.Models;

namespace Application.Services;

public class IocService(IRepository<Ioc> repository) : IIocService
{
    public async Task<IEnumerable<IocReadDto>> GetAllAsync()
    {
        var items = await repository.GetAllAsync();

        return items.Select(x => x.ToReadDto()).OrderBy(x => x.Id);
    }

    public async Task<IocReadDto?> GetByIdAsync(int id)
    {
        var item = await repository.GetByIdAsync(id);

        return item?.ToReadDto();
    }

    public async Task AddAsync(IocCreateDto ioc)
    {
        var item = ioc.ToModel();

        await repository.AddAsync(item);
    }

    public async Task UpdateAsync(int id, IocUpdateDto ioc)
    {
        var existingIoc =
            await repository.GetByIdAsync(id)
            ?? throw new IocNotFoundException($"Ioc with id {id} not found.");

        existingIoc.Sha1 = ioc.Sha1;
        existingIoc.Sha256 = ioc.Sha256;
        existingIoc.Md5 = ioc.Md5;
        existingIoc.Mcafee = ioc.Mcafee;
        existingIoc.Engines = ioc.Engines;

        await repository.UpdateAsync(existingIoc);
    }

    public async Task DeleteAsync(int id)
    {
        var ioc =
            await repository.GetByIdAsync(id)
            ?? throw new IocNotFoundException($"Ioc with id {id} not found.");

        await repository.DeleteAsync(ioc);
    }
}
