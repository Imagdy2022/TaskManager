using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectHandler : IRequestHandler<GetTasksByProjectQuery, IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repo;

    public GetTasksByProjectHandler(ITaskRepository repo) => _repo = repo;

    public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repo.GetByProjectIdAsync(request.ProjectId);
        return tasks.Select(t => new TaskItemDto(t.Id, t.Title, t.Description, t.Status, t.Status.ToString(), t.DueDate, t.ProjectId));
    }
}
