using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace MyWebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> objStudentList = _db.Students;
            //var objStudentList = _db.Students.ToList();
            return View(objStudentList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminList(int page = 1, int pageSize = 5)
        {
            // Allow fetching all records when pageSize is large (for search)
            if (pageSize > 100) pageSize = 1000;
            
            var query = _db.Students.OrderBy(s => s.Id);
            var totalRecords = query.Count();
            var objStudentList = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (Request.Headers.ContainsKey("X-Requested-With") && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("AdminList", objStudentList);
            }
            return View("AdminList", objStudentList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student obj, string? fromAdmin)
        {
            // Debug: Check what's being received
            if (!ModelState.IsValid)
            {
                // Show validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    TempData["error"] = error.ErrorMessage;
                }
                return View(obj);
            }
            
            try
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Student created successfully";
                
                // If fromAdmin parameter exists, redirect to Admin panel wrapper
                if (!string.IsNullOrEmpty(fromAdmin) || 
                    (Request.Headers.ContainsKey("Referer") && 
                     Request.Headers["Referer"].ToString().Contains("Admin")))
                {
                    return RedirectToAction("Admin", "LockerRequests");
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error creating student: " + ex.Message;
                return View(obj);
            }
        }
        //GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                TempData["success"] = "Student updated successfully";
                
                // Check if it's from admin panel, redirect to Admin panel wrapper
                if (Request.Headers.ContainsKey("Referer") && 
                    Request.Headers["Referer"].ToString().Contains("Admin"))
                {
                    return RedirectToAction("Admin", "LockerRequests");
                }
                
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Student ID is required");
                }

                var student = await _db.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
                
                // Return updated partial view
                int pageSize = 5;
                var students = await _db.Students.OrderBy(s => s.Id).ToListAsync();
                var totalRecords = students.Count;
                var list = students.Take(pageSize).ToList();
                
                ViewBag.CurrentPage = 1;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                
                return PartialView("AdminList", list);
            }
            catch (Exception ex)
            {
                // Return error details for debugging
                return Content($"Error: {ex.Message}\nStack: {ex.StackTrace}", "text/plain");
            }
        }
    }
}