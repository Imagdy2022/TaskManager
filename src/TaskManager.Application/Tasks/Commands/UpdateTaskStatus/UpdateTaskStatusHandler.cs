using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, TaskItemDto>
{
    private readonly ITaskRepository _repo;

    public UpdateTaskStatusHandler(ITaskRepository repo) => _repo = repo;

    public async Task<TaskItemDto> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _repo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.TaskItem), request.Id);

        task.Status = request.Status;

        var updated = await _repo.UpdateAsync(task);
        return new TaskItemDto(updated.Id, updated.Title, updated.Description, updated.Status, updated.Status.ToString(), updated.DueDate, updated.ProjectId);
    }
}
