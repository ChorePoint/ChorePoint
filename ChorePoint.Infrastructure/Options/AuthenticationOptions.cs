namespace ChorePoint.Infrastructure.Options;

public class AuthenticationOptions
{
    public const string ConfigurationSectionName = "Authentication";

    public string JwtKey { get; set; } = string.Empty;
    public string JwtIssuer { get; set; } = string.Empty;
    public string JwtAudience { get; set; } = string.Empty;
    public double JwtDuration { get; set; }
    public double JwtKidDuration { get; set; }
    public int KidLoginCodeLength { get; set; }
}
