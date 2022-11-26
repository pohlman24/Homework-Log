using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_Log.Models {
    public class Assignment {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string? AssignmentName { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public int MaxPoints { get; set; }

        public int PointsEarned { get; set; }

        [Required]
        public int CourseID { get; set; }
        
        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
        // do i just make the course property a string and only use the courseID to reference the course??? 

    }
}
