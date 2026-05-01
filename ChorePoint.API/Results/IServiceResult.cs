using ChorePoint.API.Enums;

namespace ChorePoint.API.Results
{
    public interface IServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public ServiceResultCode? Code { get; set; }
    }
}
