using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Address_Book.Models
{
    public class StateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Enter CountryID...")]
        [ForeignKey("CountryID")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter Name...")]
        [StringLength(100)]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "Please Enter Code...")]
        [StringLength(50)]
        public string? StateCode { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
