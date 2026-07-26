namespace TaskManager.Application.DTOs;

public record ProjectDto(int Id, string Name, string? Description, DateTime CreatedAt);

public record ProjectDetailDto(int Id, string Name, string? Description, DateTime CreatedAt, IEnumerable<TaskItemDto> Tasks);

public record CreateProjectRequest(string Name, string? Description);

public record UpdateProjectRequest(string Name, string? Description);
