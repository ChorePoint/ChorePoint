namespace ChorePoint.Infrastructure.Options;

public class DatabaseOptions
{
    public const string ConfigurationSectionName = "Database";

    public bool EnableSensitiveLogging { get; set; }
    public bool SeedTestData { get; set; }
}
