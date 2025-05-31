using Domain.Models;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}
