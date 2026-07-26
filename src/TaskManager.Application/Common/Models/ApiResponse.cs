namespace TaskManager.Application.Common.Models;

public class ApiResponse<TItem>
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public TItem? Data { get; private set; }
    public IDictionary<string, string[]>? Errors { get; private set; }

    private ApiResponse() { }

    public static ApiResponse<TItem> Success(TItem data, string message = "Success") =>
        new() { IsSuccess = true, StatusCode = 200, Message = message, Data = data };

    public static ApiResponse<TItem> NotFound(string message = "Resource not found") =>
        new() { IsSuccess = false, StatusCode = 404, Message = message };

    public static ApiResponse<TItem> BadRequest(string message, IDictionary<string, string[]>? errors = null) =>
        new() { IsSuccess = false, StatusCode = 400, Message = message, Errors = errors };

    public static ApiResponse<TItem> Fail(string message = "An unexpected error occurred") =>
        new() { IsSuccess = false, StatusCode = 500, Message = message };
}
