namespace TaskManagement.Application.Wrappers;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];

    public static ApiResponse<T> Success(T data, string? message = null) =>
        new() { IsSuccess = true, Data = data, Message = message };

    public static ApiResponse<T> Failure(IEnumerable<string> errors, string? message = null) =>
        new() { IsSuccess = false, Errors = errors, Message = message };

    public static ApiResponse<T> Failure(string error) =>
        new() { IsSuccess = false, Errors = [error] };
}
