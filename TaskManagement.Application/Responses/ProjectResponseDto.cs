namespace TaskManagement.Application.Responses;

public readonly record struct ProjectResponseDto(
    int Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    List<TaskResponseDto> Tasks
);
