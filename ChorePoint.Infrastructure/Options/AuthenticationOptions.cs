namespace ChorePoint.Infrastructure.Options;

public class AuthenticationOptions
{
    public const string ConfigurationSectionName = "Authentication";

    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public double JwtDuration { get; set; }
    public double JwtKidDuration { get; set; }
    public int KidLoginCodeLength { get; set; }
}
