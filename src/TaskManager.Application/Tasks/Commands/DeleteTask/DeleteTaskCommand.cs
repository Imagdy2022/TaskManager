using MediatR;

namespace TaskManager.Application.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(int Id) : IRequest;
