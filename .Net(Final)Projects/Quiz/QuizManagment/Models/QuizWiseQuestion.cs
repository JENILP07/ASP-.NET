using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class QuizWiseQuestion
{

    public int QuizWiseQuestionsID { get; set; }

    [Required]
    [ForeignKey("Quiz")]
    public int QuizID { get; set; }

    [Required]
    [ForeignKey("Question")]
    public int QuestionID { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserID { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime Modified { get; set; } = DateTime.UtcNow;

    // Navigation properties

}