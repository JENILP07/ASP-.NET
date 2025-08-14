using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AddressBook.Models
{
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [Required]
        [StringLength(100)]
        public string? CountryName { get; set; }
        [Required]
        [StringLength(50)]
        public string? CountryCode { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}