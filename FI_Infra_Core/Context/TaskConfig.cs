using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI_Infra_Core;

public class TaskConfig : IEntityTypeConfiguration<FI_Domain.Task>
{
    public void Configure(EntityTypeBuilder<FI_Domain.Task> builder)
    {
        builder.HasKey(e => e.TaskId);

        builder.ToTable("Task");
    }
}
