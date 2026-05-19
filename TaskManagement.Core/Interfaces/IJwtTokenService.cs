using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(AppUser user);
}
