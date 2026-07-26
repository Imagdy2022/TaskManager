using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Projects.Commands.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _repo;

    public DeleteProjectHandler(IProjectRepository repo) => _repo = repo;

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        if (!await _repo.ExistsAsync(request.Id))
            throw new NotFoundException(nameof(Domain.Entities.Project), request.Id);

        await _repo.DeleteAsync(request.Id);
    }
}
