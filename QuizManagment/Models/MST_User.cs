using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    [Required]
    [StringLength(100)]
    public string UserName { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string Mobile { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; } = false;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}