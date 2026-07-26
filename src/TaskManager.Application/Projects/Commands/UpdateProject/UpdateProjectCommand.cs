using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(int Id, string Name, string? Description) : IRequest<ProjectDto>;
