using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Queries.GetAllProjects;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>;
