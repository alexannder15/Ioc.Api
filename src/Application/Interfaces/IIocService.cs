using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IIocService
{
    Task<IEnumerable<IocReadDto>> GetAllAsync();
    Task<IocReadDto?> GetByIdAsync(int id);
    Task AddAsync(IocCreateDto ioc);
    Task UpdateAsync(int id, IocUpdateDto ioc);
    Task DeleteAsync(int id);
}
