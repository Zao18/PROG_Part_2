using Microsoft.AspNetCore.Mvc;
using PROG_Part_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace PROG_Part_2.Controllers
{
    public class ClaimsController : Controller
    {
        private static List<Claims> _claimsList = new List<Claims>();

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View("ClaimView", new Claims());
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claims model)
        {
            if (ModelState.IsValid)
            {
                _claimsList.Add(model); 
                return RedirectToAction("Index"); 
            }
            return View("ClaimView", model); 
        }
        public IActionResult Index()
        {
            return View(_claimsList); 
        }
    }
}

