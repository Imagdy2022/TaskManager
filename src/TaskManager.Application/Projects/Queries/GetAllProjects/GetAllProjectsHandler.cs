using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepository _repo;

    public GetAllProjectsHandler(IProjectRepository repo) => _repo = repo;

    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repo.GetAllAsync();
        return projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description, p.CreatedAt));
    }
}
