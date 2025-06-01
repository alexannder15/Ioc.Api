using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Application.Services;

public class IocService(IRepository<Ioc> repository, IMapper mapper) : IIocService
{
    public async Task<IEnumerable<IocReadDto>> GetAllAsync()
    {
        var items = await repository.GetAllAsync();

        return mapper.Map<IEnumerable<IocReadDto>>(items.OrderByDescending(x => x.Id));
    }

    public async Task<IocReadDto?> GetByIdAsync(int id)
    {
        var item = await repository.GetByIdAsync(id);

        return mapper.Map<IocReadDto>(item);
    }

    public async Task AddAsync(IocCreateDto ioc)
    {
        var item = mapper.Map<Ioc>(ioc);

        await repository.AddAsync(item);
    }

    public async Task UpdateAsync(int id, IocUpdateDto ioc)
    {
        var existingIoc =
            await repository.GetByIdAsync(id)
            ?? throw new IocNotFoundException($"Ioc with id {id} not found.");

        mapper.Map(ioc, existingIoc);

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
