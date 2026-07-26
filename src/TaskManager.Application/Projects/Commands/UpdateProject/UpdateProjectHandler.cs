using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _repo;

    public UpdateProjectHandler(IProjectRepository repo) => _repo = repo;

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.Project), request.Id);

        project.Name = request.Name;
        project.Description = request.Description;

        var updated = await _repo.UpdateAsync(project);
        return new ProjectDto(updated.Id, updated.Name, updated.Description, updated.CreatedAt);
    }
}
