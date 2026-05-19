namespace TaskManagement.Core.Entities;

public abstract class EntityBase
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
}
