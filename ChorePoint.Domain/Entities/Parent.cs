namespace ChorePoint.Domain.Entities;

public class Parent : EntityBase
{
    public int ParentId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ParentSettings ParentSettings { get; set; } = null!;
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public static Parent CreateWithoutPassword(string firstName, string lastName, string email)
    {
        return new Parent
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }

    public void SetPassword(string password)
    {
        Password = password;
    }
}
