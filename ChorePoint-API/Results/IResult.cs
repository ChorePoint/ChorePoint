using ChorePoint_API.Enums;

namespace ChorePoint_API.Results
{
    public interface IServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ServiceResultCode? Code { get; set; }
    }
}
