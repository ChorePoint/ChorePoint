using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint_API.Models
{
    [Table("parents")]
    public class Parent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = null!;

        [Required]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
