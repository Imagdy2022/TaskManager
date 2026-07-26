using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Queries.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto>
{
    private readonly ITaskRepository _repo;

    public GetTaskByIdHandler(ITaskRepository repo) => _repo = repo;

    public async Task<TaskItemDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.TaskItem), request.Id);

        return new TaskItemDto(task.Id, task.Title, task.Description, task.Status, task.Status.ToString(), task.DueDate, task.ProjectId);
    }
}
