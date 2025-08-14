using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Address_Book.Models
{
    public class CityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Please Enter StateID...")]
        [ForeignKey("StateId")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Enter Name...")]
        [StringLength(100)]
        public string? CityName { get; set; }

        [StringLength(50)]
        public string? STDCode { get; set; }

        [StringLength(6)]
        public string? PinCode { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public virtual StateModel? State { get; set; }
    }
}
