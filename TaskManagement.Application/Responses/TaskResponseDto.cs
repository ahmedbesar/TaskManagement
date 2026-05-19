namespace TaskManagement.Application.Responses;

public readonly record struct TaskResponseDto(
    int Id,
    string Title,
    string Description,
    string Status,
    DateTime? DueDate,
    string Priority,
    int ProjectId,
    DateTime CreatedAt
);
