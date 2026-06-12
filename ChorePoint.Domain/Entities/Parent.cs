using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

[Table("parents")]
public class Parent : EntityBase
{
    [Key] [Column("parent_id")] public int ParentId { get; set; }

    [MaxLength(100)]
    [Column("first_name")]
    public string FirstName { get; set; }

    [MaxLength(100)] [Column("last_name")] public string LastName { get; set; }

    [Column("email")] public string Email { get; set; }

    [Column("password")] public string Password { get; set; }


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