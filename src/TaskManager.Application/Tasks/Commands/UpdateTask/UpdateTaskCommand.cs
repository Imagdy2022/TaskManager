using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(int Id, string Title, string? Description, TaskItemStatus Status, DateTime? DueDate)
    : IRequest<TaskItemDto>;
