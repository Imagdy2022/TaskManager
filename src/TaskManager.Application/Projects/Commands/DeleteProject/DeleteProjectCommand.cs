using MediatR;

namespace TaskManager.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest;
