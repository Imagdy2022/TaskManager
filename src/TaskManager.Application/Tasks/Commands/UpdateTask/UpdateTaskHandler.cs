using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskItemDto>
{
    private readonly ITaskRepository _repo;

    public UpdateTaskHandler(ITaskRepository repo) => _repo = repo;

    public async Task<TaskItemDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _repo.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Entities.TaskItem), request.Id);

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.DueDate = request.DueDate;

        var updated = await _repo.UpdateAsync(task);
        return new TaskItemDto(updated.Id, updated.Title, updated.Description, updated.Status, updated.Status.ToString(), updated.DueDate, updated.ProjectId);
    }
}
