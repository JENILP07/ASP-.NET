using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int QuestionID { get; set; }



    [Required]
    public string QuestionText { get; set; }

    [Required]
    [ForeignKey("QuestionLevel")]
    public int QuestionLevelID { get; set; }

    [Required]
    [StringLength(100)]
    public string OptionA { get; set; }

    [Required]
    [StringLength(100)]
    public string OptionB { get; set; }

    [Required]
    [StringLength(100)]
    public string OptionC { get; set; }

    [Required]
    [StringLength(100)]
    public string OptionD { get; set; }

    [Required]
    [StringLength(100)]
    public string CorrectOption { get; set; }

    [Required]
    public int QuestionMarks { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    [ForeignKey("User")]
    public int UserID { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime Modified { get; set; } = DateTime.UtcNow;

}