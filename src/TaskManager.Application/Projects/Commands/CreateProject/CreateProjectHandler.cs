using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Projects.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _repo;

    public CreateProjectHandler(IProjectRepository repo) => _repo = repo;

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project { Name = request.Name, Description = request.Description };
        var created = await _repo.CreateAsync(project);
        return new ProjectDto(created.Id, created.Name, created.Description, created.CreatedAt);
    }
}
