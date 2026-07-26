using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public record TaskItemDto(int Id, string Title, string? Description, TaskItemStatus Status, string StatusLabel, DateTime? DueDate, int ProjectId);

public record CreateTaskRequest(string Title, string? Description, TaskItemStatus Status, DateTime? DueDate, int ProjectId);

public record UpdateTaskRequest(string Title, string? Description, TaskItemStatus Status, DateTime? DueDate);

public record UpdateTaskStatusRequest(TaskItemStatus Status);
