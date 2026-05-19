using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Repositories;

public interface IProjectRepository : IAsyncRepository<Project>
{
    Task<IReadOnlyList<Project>> GetByUserIdAsync(int userId);
    Task<Project?> GetByIdWithTasksAsync(int id);
}
