using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _ctx;

    public TaskRepository(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId)
        => await _ctx.Tasks.AsNoTracking().Where(t => t.ProjectId == projectId).ToListAsync();

    public async Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskItemStatus status)
        => await _ctx.Tasks.AsNoTracking().Where(t => t.Status == status).ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id)
        => await _ctx.Tasks.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        _ctx.Tasks.Add(task);
        await _ctx.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        _ctx.Tasks.Update(task);
        await _ctx.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _ctx.Tasks.FindAsync(id);
        if (task is not null)
        {
            _ctx.Tasks.Remove(task);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
        => await _ctx.Tasks.AnyAsync(t => t.Id == id);
}
