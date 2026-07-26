using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Common.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId);
    Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskItemStatus status);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
