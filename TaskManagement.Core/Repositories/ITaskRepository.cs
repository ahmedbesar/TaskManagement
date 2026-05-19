using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Repositories;

public interface ITaskRepository : IAsyncRepository<TaskItem>
{
    Task<IReadOnlyList<TaskItem>> GetByProjectIdAsync(int projectId);
}