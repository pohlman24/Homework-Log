using Homework_Log.Data;
using Homework_Log.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_Log.Controllers
{
	public class AssignmentController : Controller
	{

		// gives access to read from DI container 
		private readonly ApplicationDbContext _db;

		// init DI container
		public AssignmentController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Assignment> objAssignmentList = _db.Assignments.OrderBy(a => a.DueDate);
			return View(objAssignmentList);
		}


		//GET
		public IActionResult Create(int CourseId)
		{
			Course courseDb = _db.Courses.Find(CourseId);
			ViewBag.Course = courseDb;
			ViewBag.CourseName = courseDb.CourseName;
			ViewBag.CourseId = courseDb.ID;
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("AssignmentName, DueDate, CourseID, MaxPoints")] Assignment assignment)
		{
			Course course = _db.Courses.Find(assignment.CourseID);
			assignment.Course = course;
			assignment.CourseName = course.CourseName;
			int courseID = assignment.CourseID;
			/*if (ModelState.IsValid)
			{
				*//*course.Assignments.Add(assignment);*//*
				_db.Assignments.Add(assignment);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}*/
			_db.Assignments.Add(assignment);
			_db.SaveChanges();
			return RedirectToAction("Details", "Course", new { id = courseID });
		}


		//GET
		public ActionResult Delete(int? id, int courseID)
		{
			ViewBag.CourseName = _db.Courses.Find(courseID).CourseName;
			ViewBag.CourseID = courseID;
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var AssignmentFromDb = _db.Assignments.Find(id);

			if (AssignmentFromDb == null)
			{
				return NotFound();
			}

			return View(AssignmentFromDb);
		}

		//POST 
		[HttpPost]
		public IActionResult DeletePOST(int? id, int courseID)
		{
			var obj = _db.Assignments.Find(id);

			if (obj == null)
			{
				TempData["fail"] = "Error deleting course";
				NotFound();
			}

			_db.Assignments.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Assignment successfully deleted";
			return RedirectToAction("Details", "Course", new { id = courseID });
		}


		//GET
		public IActionResult Edit(int? id, int courseID)
		{
			ViewBag.CourseID = courseID;
			ViewBag.CourseName = _db.Courses.Find(courseID).CourseName;
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var assignmentFromDb = _db.Assignments.Find(id);

			if (assignmentFromDb == null)
			{
				return NotFound();
			}

			return View(assignmentFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Assignment obj, int courseID)
		{
			// model state is going to compare the object you are trying to access to the model class and see if all required fields are valid 
			/*if (ModelState.IsValid)
			{*/
				_db.Assignments.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Assignment successfully updated";
			return RedirectToAction("Details", "Course", new { id = courseID });
			
			/*TempData["fail"] = "Error changing course";
			return View(obj);*/
		}

	}
}
