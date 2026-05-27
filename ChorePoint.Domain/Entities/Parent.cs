using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

[Table("parents")]
public class Parent
{
    [Key] [Column("id")] public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Required] [Column("email")] public string Email { get; set; } = null!;

    [Required] [Column("password")] public string Password { get; set; } = null!;

    [Column("created_at")] public DateTime? CreatedAt { get; set; }


    public static Parent CreateWithoutPassword(string firstName, string lastName, string email, DateTime now)
    {
        return new Parent
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CreatedAt = now
        };
    }

    public void SetPassword(string password)
    {
        Password = password;
    }
}