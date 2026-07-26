using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands.DeleteTask;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _repo;

    public DeleteTaskHandler(ITaskRepository repo) => _repo = repo;

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await _repo.ExistsAsync(request.Id))
            throw new NotFoundException(nameof(Domain.Entities.TaskItem), request.Id);

        await _repo.DeleteAsync(request.Id);
    }
}
