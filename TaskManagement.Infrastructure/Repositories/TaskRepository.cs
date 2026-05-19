using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Repositories;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : RepositoryBase<TaskItem>, ITaskRepository
{
    public TaskRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<TaskItem>> GetByProjectIdAsync(int projectId)
    {
        return await _dbContext.Tasks
            .Where(t => t.ProjectId == projectId)
            .AsNoTracking()
            .ToListAsync();
    }
}
