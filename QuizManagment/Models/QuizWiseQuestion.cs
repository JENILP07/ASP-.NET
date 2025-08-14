using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class QuizWiseQuestion
{

    public int QuizWiseQuestionsID { get; set; }

    [ForeignKey("Quiz")]
    public int QuizID { get; set; }

    [ForeignKey("Question")]
    public int QuestionID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime Modified { get; set; } = DateTime.UtcNow;

}