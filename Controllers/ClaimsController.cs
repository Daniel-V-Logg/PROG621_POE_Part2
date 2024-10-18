using Microsoft.AspNetCore.Mvc;
using Contract_Monthly_ClaimSystem__CMCS_.Models;
using System.IO;
using System.Linq;

namespace Contract_Monthly_ClaimSystem__CMCS_.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claims/Submit
        public IActionResult Submit()
        {
            return View();
        }

        // POST: Claims/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ClaimViewModel claimViewModel)
        {
            if (ModelState.IsValid)
            {
                // Handle the uploaded document
                if (claimViewModel.UploadFile != null && claimViewModel.UploadFile.Length > 0)
                {
                    // Create a unique file name
                    var uniqueFileName = Path.GetFileNameWithoutExtension(claimViewModel.UploadFile.FileName)
                                         + "_" + Guid.NewGuid()
                                         + Path.GetExtension(claimViewModel.UploadFile.FileName);

                    // Define the upload path
                    var filePath = Path.Combine("wwwroot/uploads", uniqueFileName);

                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // Save the uploaded file to the directory
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        claimViewModel.UploadFile.CopyTo(stream);
                    }

                    // Assign the file name to the claim model
                    claimViewModel.DocumentFile = uniqueFileName;
                }

                // Create a new claim from the view model
                var claim = new Claim
                {
                    LecturerName = claimViewModel.LecturerName,
                    HoursWorked = claimViewModel.HoursWorked,
                    HourlyRate = claimViewModel.HourlyRate,
                    Notes = claimViewModel.Notes,
                    DocumentFileName = claimViewModel.DocumentFile,
                    ClaimStatus = "Pending" // Set initial claim status
                };

                // Add the claim to the database
                _context.Claims.Add(claim);
                _context.SaveChanges();

                TempData["Message"] = "Claim submitted successfully!";
                return RedirectToAction("Verify"); // Redirect after submission to Verify view
            }

            // If there are validation errors, return to the same view
            return View(claimViewModel);
        }

        // POST: Claims/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.ClaimStatus = "Approved";
                _context.SaveChanges();

                TempData["Message"] = "Claim approved successfully!";
                return RedirectToAction("Verify"); // Redirect back to the verification view
            }

            TempData["Message"] = "Claim not found!";
            return NotFound(); // Return a not found response if the claim does not exist
        }

        // POST: Claims/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.ClaimStatus = "Rejected";
                _context.SaveChanges();

                TempData["Message"] = "Claim rejected successfully!";
                return RedirectToAction("Verify"); // Redirect back to the verification view
            }

            TempData["Message"] = "Claim not found!";
            return NotFound(); // Return a not found response if the claim does not exist
        }

        // GET: Claims/Verify
        public IActionResult Verify()
        {
            var pendingClaims = _context.Claims
                .Where(c => c.ClaimStatus == "Pending")
                .ToList(); // Fetch pending claims

            return View(pendingClaims); // Pass the list to the view
        }

        // GET: Claims/Index
        public IActionResult Index()
        {
            var allClaims = _context.Claims.ToList(); // Fetch all claims from the database
            return View(allClaims); // Pass the list to the view
        }
    }
}
