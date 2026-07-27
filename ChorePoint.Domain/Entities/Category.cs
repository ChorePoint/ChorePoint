using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class Category : EntityBase
{
    public int CategoryId { get; set; }
    public int ParentId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public CategoryRole Role { get; set; }

    public Parent Parent { get; set; } = new();
}
