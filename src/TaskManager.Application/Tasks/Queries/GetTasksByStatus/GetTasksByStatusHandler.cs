using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Queries.GetTasksByStatus;

public class GetTasksByStatusHandler : IRequestHandler<GetTasksByStatusQuery, IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repo;

    public GetTasksByStatusHandler(ITaskRepository repo) => _repo = repo;

    public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByStatusQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repo.GetByStatusAsync(request.Status);
        return tasks.Select(t => new TaskItemDto(t.Id, t.Title, t.Description, t.Status, t.Status.ToString(), t.DueDate, t.ProjectId));
    }
}
