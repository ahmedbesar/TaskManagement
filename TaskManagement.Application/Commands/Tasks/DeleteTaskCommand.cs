using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Tasks;

public class DeleteTaskCommand : IRequest<Result>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
