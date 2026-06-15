using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

public class EntityBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}