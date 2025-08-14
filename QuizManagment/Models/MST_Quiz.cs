using QuizManagment.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Quiz
{
    [Key]
    public int QuizID { get; set; }

    [Required, StringLength(100)]
    public string QuizName { get; set; }

    [Required]
    public int TotalQuestions { get; set; }

    [Required]
    [FutureDate]
    public DateTime QuizDate { get; set; }

    [Required, ForeignKey("User")]
    public int UserID { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;


}
