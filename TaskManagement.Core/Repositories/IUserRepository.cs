using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Repositories;

public interface IUserRepository : IAsyncRepository<AppUser>
{
    Task<AppUser?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
