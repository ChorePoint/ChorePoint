using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

public class EntityBase
{
    [Column("created_at")] public DateTime CreatedAt { get; set; }

    [Column("updated_at")] public DateTime UpdatedAt { get; set; }
}