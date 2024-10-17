using Microsoft.AspNetCore.Mvc;
using PROG_Part_2.Models;

namespace PROG_Part_2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult PendingClaims()
        {
            // Access the static _claimsList from ClaimsController
            var pendingClaims = ClaimsController._claimsList;
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = ClaimsController._claimsList.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                ClaimsController._claimsList.Remove(claim); // Approve claim by removing from the list
            }
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = ClaimsController._claimsList.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                ClaimsController._claimsList.Remove(claim); // Reject claim by removing from the list
            }
            return RedirectToAction("PendingClaims");
        }
    }
}
