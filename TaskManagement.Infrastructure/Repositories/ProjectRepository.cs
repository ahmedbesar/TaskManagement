using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Repositories;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Project>> GetByUserIdAsync(int userId)
    {
        return await _dbContext.Projects
            .Where(p => p.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project?> GetByIdWithTasksAsync(int id)
    {
        return await _dbContext.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
