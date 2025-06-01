using Domain.Models.Identity;

namespace Application.Interfaces;

public interface IUserContextService
{
    Task<User> GetCurrentUser();
}
