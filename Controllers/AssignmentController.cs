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
			IEnumerable<Assignment> objAssignmentList = _db.Assignments;
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
			/*if (ModelState.IsValid)
			{
				*//*course.Assignments.Add(assignment);*//*
				_db.Assignments.Add(assignment);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}*/
			_db.Assignments.Add(assignment);
			_db.SaveChanges();
			//Course/Details/ID
			return RedirectToAction("Index","Course");
		}


		//GET
		public ActionResult Delete(int? id)
		{
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
		public IActionResult DeletePOST(int? id)
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
			return RedirectToAction("Index", "Course");
		}
	}
}
