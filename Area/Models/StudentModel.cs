using System.ComponentModel.DataAnnotations;

namespace Area.Models
{
    public class StudentModel
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? MobileNo { get; set; }

        public StudentModel() { }

    }
}
