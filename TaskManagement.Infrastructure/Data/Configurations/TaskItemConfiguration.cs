using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Core.Consts;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Data.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(TaskConstants.TitleMaxLength);

        builder.Property(t => t.Description)
            .HasMaxLength(TaskConstants.DescriptionMaxLength);

        builder.Property(t => t.Status)
            .HasConversion<string>();

        builder.Property(t => t.Priority)
            .HasConversion<string>();
    }
}
