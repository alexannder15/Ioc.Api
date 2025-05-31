using Domain.Models;

namespace Application.Interfaces;

public interface IUserContextService
{
    Task<User> GetCurrentUser();
}
