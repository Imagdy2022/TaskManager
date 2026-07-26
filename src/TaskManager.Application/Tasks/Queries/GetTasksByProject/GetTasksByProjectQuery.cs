using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Queries.GetTasksByProject;

public record GetTasksByProjectQuery(int ProjectId) : IRequest<IEnumerable<TaskItemDto>>;
