using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Homework_Log.Models {
	public class Course {
		[Key]
		public int ID{ get; set; }

		[Required]
		[DisplayName("Course Name")]
		public string? CourseName{ get; set; }

		[DisplayName("Display Order")]
		public int? DisplayOrder { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		
		public virtual ICollection<Assignment>? Assignments { get; set; }

		// one to many relations. Order = assignment | Customer = course   course can have multiple assigments but an assignemnt can only have one course
		//

	}
}
