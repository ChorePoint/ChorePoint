using ChorePoint.API.Enums;

namespace ChorePoint.API.Results
{
    public class ServiceResult(bool success, string? errorMessage = null, ServiceResultCode? code = null) : IServiceResult
    {
        public bool Success { get; set; } = success;
        public string? ErrorMessage { get; set; } = errorMessage;
        public ServiceResultCode? Code { get; set; } = code;
    }
}
