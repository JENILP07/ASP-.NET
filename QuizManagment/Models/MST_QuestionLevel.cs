using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class QuestionLevel
{

    public int QuestionLevelID { get; set; }

    [Required]
    [StringLength(100)]
    [Column("QuestionLevel")]  // Keeps the column name in the database as 'QuestionLevel'
    public string Level { get; set; } // Renamed property from 'QuestionLevel' to 'Level'


    public int UserID { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime Modified { get; set; } = DateTime.UtcNow;

}