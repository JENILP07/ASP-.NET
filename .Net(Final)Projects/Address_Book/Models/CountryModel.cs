using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter Name...")]
        [StringLength(100)]
        public string? CountryName { get; set; }

        [Required(ErrorMessage = "Please Enter Code...")]
        [StringLength(50)]
        public string? CountryCode { get; set; }

        [Required(ErrorMessage = "Please Enter UserID...")]
        [ForeignKey("UserModel")]
        public int UserID { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
