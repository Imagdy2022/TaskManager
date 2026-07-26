using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _ctx;

    public ProjectRepository(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Project>> GetAllAsync()
        => await _ctx.Projects.AsNoTracking().OrderByDescending(p => p.CreatedAt).ToListAsync();

    public async Task<Project?> GetByIdAsync(int id)
        => await _ctx.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Project> CreateAsync(Project project)
    {
        _ctx.Projects.Add(project);
        await _ctx.SaveChangesAsync();
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        _ctx.Projects.Update(project);
        await _ctx.SaveChangesAsync();
        return project;
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _ctx.Projects.FindAsync(id);
        if (project is not null)
        {
            _ctx.Projects.Remove(project);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
        => await _ctx.Projects.AnyAsync(p => p.Id == id);
}
