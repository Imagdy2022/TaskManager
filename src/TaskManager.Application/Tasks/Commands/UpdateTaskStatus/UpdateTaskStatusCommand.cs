using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands.UpdateTaskStatus;

public record UpdateTaskStatusCommand(int Id, TaskItemStatus Status) : IRequest<TaskItemDto>;
