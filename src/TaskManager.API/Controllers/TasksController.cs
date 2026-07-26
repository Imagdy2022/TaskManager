using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Common.Models;
using TaskManager.Application.DTOs;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Application.Tasks.Commands.DeleteTask;
using TaskManager.Application.Tasks.Commands.UpdateTask;
using TaskManager.Application.Tasks.Commands.UpdateTaskStatus;
using TaskManager.Application.Tasks.Queries.GetTaskById;
using TaskManager.Application.Tasks.Queries.GetTasksByProject;
using TaskManager.Application.Tasks.Queries.GetTasksByStatus;
using TaskManager.Domain.Enums;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<TaskItemDto>>> GetById(int id)
        => Ok(ApiResponse<TaskItemDto>.Success(await _mediator.Send(new GetTaskByIdQuery(id))));

    [HttpGet("by-project/{projectId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TaskItemDto>>>> GetByProject(int projectId)
        => Ok(ApiResponse<IEnumerable<TaskItemDto>>.Success(await _mediator.Send(new GetTasksByProjectQuery(projectId))));

    [HttpGet("by-status/{status}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TaskItemDto>>>> GetByStatus(TaskItemStatus status)
        => Ok(ApiResponse<IEnumerable<TaskItemDto>>.Success(await _mediator.Send(new GetTasksByStatusQuery(status))));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<TaskItemDto>>> Create([FromBody] CreateTaskRequest request)
    {
        var result = await _mediator.Send(new CreateTaskCommand(
            request.Title, request.Description, request.Status, request.DueDate, request.ProjectId));
        var response = ApiResponse<TaskItemDto>.Success(result, "Task created successfully.");
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<TaskItemDto>>> Update(int id, [FromBody] UpdateTaskRequest request)
        => Ok(ApiResponse<TaskItemDto>.Success(await _mediator.Send(new UpdateTaskCommand(id, request.Title, request.Description, request.Status, request.DueDate))));

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ApiResponse<TaskItemDto>>> UpdateStatus(int id, [FromBody] UpdateTaskStatusRequest request)
        => Ok(ApiResponse<TaskItemDto>.Success(await _mediator.Send(new UpdateTaskStatusCommand(id, request.Status))));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<object?>>> Delete(int id)
    {
        await _mediator.Send(new DeleteTaskCommand(id));
        return Ok(ApiResponse<object?>.Success(null, "Task deleted successfully."));
    }
}
