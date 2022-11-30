using Homework_Log.Data;
using Homework_Log.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework_Log.Controllers {
	public class CourseController : Controller {

		// gives access to read from DI container 
		private readonly ApplicationDbContext _db;

		// init DI container
		public CourseController(ApplicationDbContext db) {
			_db = db;
		}

		public IActionResult Index() {
			IEnumerable<Course> objCourseList = _db.Courses.Include(n => n.Assignments.OrderBy(o=>o.DueDate));
			//if(objCourseList contains any assingments with due date sooner than 1 day ){
			foreach (var course in objCourseList) 
			{
				foreach(var assignment in course.Assignments)
                {
					
					if((assignment.DueDate - DateTime.Now).TotalHours <= 36)
                    {
						ViewBag.AlertMsg = "Assignment Due Very Soon";
						break;
					}
					else if((assignment.DueDate - DateTime.Now).TotalHours <= 48){
						ViewBag.AlertMsg = "Assignment Due Soon";
						continue;
					}
					/*else if((ViewBag.AlertMsg != "Assignment Due Soon" || ViewBag.AlertMsg != "Assignment Due Very Soon") && (assignment.DueDate - DateTime.Now).TotalDays >= 3)
                    {
						ViewBag.AlertMsg = "";
					}*/
                }
			}
			return View(objCourseList);
			// return View( _db.Courses.ToList()); -- other option
		}

		//GET
		public IActionResult Create() {
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Course course) {
			if (ModelState.IsValid)
			{
				course.DisplayOrder = 1;
				_db.Add(course);
				_db.SaveChanges();
				return RedirectToAction(nameof(Index));
				TempData["success"] = "Course successfully Added";
			}
			TempData["fail"] = "Error Adding Course";
			return View(course);
		}

		//GET
		public IActionResult Edit(int? id) {
			if (id == null || id == 0) {
				return NotFound();
			}
			var courseFromDb = _db.Courses.Find(id);

			if (courseFromDb == null) {
				return NotFound();
			}

			return View(courseFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Course obj) {
			// model state is going to compare the object you are trying to access to the model class and see if all required fields are valid 
			if (ModelState.IsValid) {
				obj.DisplayOrder = 1;
				_db.Courses.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Course successfully changed";
				return RedirectToAction("Index");
			}
			TempData["fail"] = "Error changing course";
			return View(obj);
		}


		//GET
		public ActionResult Delete(int? id) {
			if (id == null || id == 0) {
				return NotFound();
            }
			var CourseFromDb = _db.Courses.Find(id);

			if (CourseFromDb == null) {
				return NotFound();
			}

			return View(CourseFromDb);
		}

		//POST 
		[HttpPost]
		public IActionResult DeletePOST(int? id) {
			// if i delete a course, need to make sure all the assignments linked to course will be removed
			var obj = _db.Courses.Find(id);
			
			if(obj == null) {
				TempData["fail"] = "Error deleting course";
				NotFound();
            }

			_db.Courses.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Course successfully deleted";
			return RedirectToAction("Index");
			
		}

		// GET
		public IActionResult Details(int id)
		{
			Course CourseFromDb = _db.Courses.Find(id);
			_db.Entry(CourseFromDb).Collection(p => p.Assignments).Query().OrderBy(p => p.DueDate).Load();
			return View(CourseFromDb);
		}

	}
}
