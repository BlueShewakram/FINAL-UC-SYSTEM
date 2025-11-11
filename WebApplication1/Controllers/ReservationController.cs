using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReservationController : Controller
    {
        public readonly ApplicationDbContext _db;
        public ReservationController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Ensure the Reservations table contains the status columns. This runs a conditional ALTER TABLE
        // the first time it's needed. It requires the DB user to have ALTER permissions.
        private void EnsureReservationStatusColumns()
        {
            try
            {
                var sql = @"IF COL_LENGTH('Reservations','Approved') IS NULL BEGIN ALTER TABLE [Reservations] ADD [Approved] bit NULL END;"
                        + "IF COL_LENGTH('Reservations','Approver') IS NULL BEGIN ALTER TABLE [Reservations] ADD [Approver] nvarchar(256) NULL END;"
                        + "IF COL_LENGTH('Reservations','ApprovalDate') IS NULL BEGIN ALTER TABLE [Reservations] ADD [ApprovalDate] datetime2 NULL END;";
                _db.Database.ExecuteSqlRaw(sql);
            }
            catch
            {
                // If this fails (insufficient permissions), we'll let the normal EF flow continue so the app
                // doesn't crash; admin actions will still work in-memory for the current request.
            }
        }

        public IActionResult ResUI(int page = 1)
        {
            // ensure status columns exist before querying so EF won't throw missing column errors
            EnsureReservationStatusColumns();
            int pageSize = 5; // Limit to 5 rows per page
            var reservations = _db.Reservations
                                   .OrderBy(r => r.Id)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            int totalRecords = _db.Reservations.Count();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return View(reservations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Reservation obj)
        {
            if (ModelState.IsValid)
            {
                // Ensure the reservation status columns exist
                EnsureReservationStatusColumns();
                
                // Set the reservation to pending status (not approved yet)
                obj.Approved = null;  // null = Pending status
                obj.Approver = null;
                obj.ApprovalDate = null;
                
                _db.Reservations.Add(obj);
                _db.SaveChanges();
                
                TempData["success"] = "Reservation created successfully and is pending approval";
                return RedirectToAction("ResUI");
            }
            
            // Show validation errors if model is invalid
            TempData["error"] = "Please fill in all required fields correctly";
            return View(obj);
        }
        
        // Admin list with approve/delete actions
        [Authorize(Roles = "Admin")]
        public IActionResult AdminList(int page = 1)
        {
            // ensure the status columns exist before querying
            EnsureReservationStatusColumns();
            int pageSize = 5;
            var query = _db.Reservations.OrderBy(r => r.Id);
            var totalRecords = query.Count();
            var reservations = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // If request is AJAX, return partial view without layout
            if (Request.Headers.ContainsKey("X-Requested-With") && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("AdminList", reservations);
            }
            return View(reservations);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id, bool approved)
        {
            EnsureReservationStatusColumns();
            var item = _db.Reservations.Find(id);
            if (item == null) 
            {
                TempData["error"] = "Reservation not found";
                return RedirectToAction("AdminList");
            }
            
            item.Approved = approved;
            item.ApprovalDate = DateTime.UtcNow;
            item.Approver = User?.Identity?.Name ?? "Admin";
            
            try
            {
                _db.Reservations.Update(item);
                _db.SaveChanges();
                
                TempData["success"] = approved ? "Reservation approved successfully" : "Reservation rejected successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating reservation: " + ex.Message;
            }
            
            return RedirectToAction("AdminList");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Reject(int id)
        {
            EnsureReservationStatusColumns();
            var item = _db.Reservations.Find(id);
            if (item == null) 
            {
                TempData["error"] = "Reservation not found";
                return RedirectToAction("AdminList");
            }
            
            item.Approved = false;
            item.ApprovalDate = DateTime.UtcNow;
            item.Approver = User?.Identity?.Name ?? "Admin";
            
            try
            {
                _db.Reservations.Update(item);
                _db.SaveChanges();
                
                TempData["success"] = "Reservation rejected successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error rejecting reservation: " + ex.Message;
            }
            
            return RedirectToAction("AdminList");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            EnsureReservationStatusColumns();
            var item = _db.Reservations.Find(id);
            if (item == null) 
            {
                TempData["error"] = "Reservation not found";
                return RedirectToAction("AdminList");
            }
            
            try
            {
                _db.Reservations.Remove(item);
                _db.SaveChanges();
                
                TempData["success"] = "Reservation deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error deleting reservation: " + ex.Message;
            }
            
            return RedirectToAction("AdminList");
        }
        
        // One-time action to reset all existing reservations to pending
        [Authorize(Roles = "Admin")]
        public IActionResult ResetAllToPending()
        {
            EnsureReservationStatusColumns();
            
            // Get all reservations
            var allReservations = _db.Reservations.ToList();
            
            // Set them all to pending
            foreach (var reservation in allReservations)
            {
                reservation.Approved = null;
                reservation.Approver = null;
                reservation.ApprovalDate = null;
            }
            
            _db.SaveChanges();
            TempData["success"] = $"Successfully reset {allReservations.Count} reservations to pending status";
            return RedirectToAction("AdminList");
        }
        
    }
}
