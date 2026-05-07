using ChorePoint_API.Enums;

namespace ChorePoint_API.Models.Requests
{
    public class CreateChoreRequest
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int KidId { get; set; }
        public ChoreFrequency Frequency { get; set; }
        public DayOfWeek DueDate { get; set; }
        public int Points { get; set; }
        public string? Description { get; set; }
    }
}
