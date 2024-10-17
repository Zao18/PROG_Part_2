using Microsoft.AspNetCore.Mvc;
using PROG_Part_2.Models;

namespace PROG_Part_2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult PendingClaims()
        {
            var pendingClaims = ClaimsController._claimsList;
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = ClaimsController._claimsList.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Approved";
            }
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = ClaimsController._claimsList.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("PendingClaims");
        }
    }
}
