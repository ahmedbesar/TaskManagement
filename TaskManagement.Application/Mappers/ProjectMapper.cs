using TaskManagement.Application.Commands.Projects;
using TaskManagement.Application.Responses;
using TaskManagement.Core.Entities;

namespace TaskManagement.Application.Mappers;

public static class ProjectMapper
{
    public static ProjectResponseDto ToResponseDto(Project project) => new ProjectResponseDto(
        Id: project.Id,
        Name: project.Name,
        Description: project.Description,
        CreatedAt: project.CreatedAt,
        Tasks: project.Tasks?.Select(TaskMapper.ToResponseDto).ToList() ?? []
    );

    public static List<ProjectResponseDto> ToResponseDtoList(IEnumerable<Project> projects) =>
        projects.Select(ToResponseDto).ToList();

    public static Project ToEntity(CreateProjectCommand cmd) => 
        Project.Create(cmd.Name, cmd.Description, cmd.UserId);
}
