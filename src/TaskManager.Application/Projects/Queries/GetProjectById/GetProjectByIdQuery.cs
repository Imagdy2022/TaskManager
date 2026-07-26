using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(int Id) : IRequest<ProjectDetailDto>;
