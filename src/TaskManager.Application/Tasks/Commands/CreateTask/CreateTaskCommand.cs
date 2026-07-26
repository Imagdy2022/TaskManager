using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Title, string? Description, TaskItemStatus Status, DateTime? DueDate, int ProjectId)
    : IRequest<TaskItemDto>;
