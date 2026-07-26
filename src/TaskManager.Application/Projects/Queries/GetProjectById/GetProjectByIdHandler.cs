using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailDto>
{
    private readonly IProjectRepository _repo;

    public GetProjectByIdHandler(IProjectRepository repo) => _repo = repo;

    public async Task<ProjectDetailDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Project), request.Id);

        var tasks = project.Tasks.Select(t => new TaskItemDto(
            t.Id, t.Title, t.Description, t.Status, t.Status.ToString(), t.DueDate, t.ProjectId));

        return new ProjectDetailDto(project.Id, project.Name, project.Description, project.CreatedAt, tasks);
    }
}
