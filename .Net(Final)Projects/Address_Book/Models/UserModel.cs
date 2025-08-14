using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Name...")]
        [StringLength(100)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number...")]
        [StringLength(100)]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Email Account...")]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}

