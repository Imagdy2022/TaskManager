using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Common.Models;
using TaskManager.Application.DTOs;
using TaskManager.Application.Projects.Commands.CreateProject;
using TaskManager.Application.Projects.Commands.DeleteProject;
using TaskManager.Application.Projects.Commands.UpdateProject;
using TaskManager.Application.Projects.Queries.GetAllProjects;
using TaskManager.Application.Projects.Queries.GetProjectById;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetAll()
        => Ok(ApiResponse<IEnumerable<ProjectDto>>.Success(await _mediator.Send(new GetAllProjectsQuery())));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProjectDetailDto>>> GetById(int id)
        => Ok(ApiResponse<ProjectDetailDto>.Success(await _mediator.Send(new GetProjectByIdQuery(id))));

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> Create([FromBody] CreateProjectRequest request)
    {
        var result = await _mediator.Send(new CreateProjectCommand(request.Name, request.Description));
        var response = ApiResponse<ProjectDto>.Success(result, "Project created successfully.");
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> Update(int id, [FromBody] UpdateProjectRequest request)
        => Ok(ApiResponse<ProjectDto>.Success(await _mediator.Send(new UpdateProjectCommand(id, request.Name, request.Description))));

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<object?>>> Delete(int id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));
        return Ok(ApiResponse<object?>.Success(null, "Project deleted successfully."));
    }
}
