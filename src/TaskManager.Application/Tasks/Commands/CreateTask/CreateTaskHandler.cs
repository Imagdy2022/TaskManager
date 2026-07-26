using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks.Commands.CreateTask;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
{
    private readonly ITaskRepository _taskRepo;
    private readonly IProjectRepository _projectRepo;

    public CreateTaskHandler(ITaskRepository taskRepo, IProjectRepository projectRepo)
    {
        _taskRepo = taskRepo;
        _projectRepo = projectRepo;
    }

    public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await _projectRepo.ExistsAsync(request.ProjectId))
            throw new NotFoundException(nameof(Project), request.ProjectId);

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            DueDate = request.DueDate,
            ProjectId = request.ProjectId
        };

        var created = await _taskRepo.CreateAsync(task);
        return new TaskItemDto(created.Id, created.Title, created.Description, created.Status, created.Status.ToString(), created.DueDate, created.ProjectId);
    }
}
