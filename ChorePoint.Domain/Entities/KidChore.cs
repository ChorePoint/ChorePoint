using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

public class KidChore : EntityBase
{
    public int KidId { get; set; }
    public int ChoreId { get; set; }
    
    public DayOfWeek? DueDay { get; set; }
    public bool IsVisible { get; set; }
}