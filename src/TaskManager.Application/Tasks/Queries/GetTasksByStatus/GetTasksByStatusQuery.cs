using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Queries.GetTasksByStatus;

public record GetTasksByStatusQuery(TaskItemStatus Status) : IRequest<IEnumerable<TaskItemDto>>;
