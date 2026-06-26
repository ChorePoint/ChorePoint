using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class Category : EntityBase
{
    public int CategoryId { get; set; }
    public int ParentId { get; set; }
    
    public string Name { get; set; }
    public string Icon { get; set; }
    public CategoryRole Role { get; set; }
    
    public Parent Parent  { get; set; }
}