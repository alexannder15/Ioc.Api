using Domain.Models.Identity;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}
