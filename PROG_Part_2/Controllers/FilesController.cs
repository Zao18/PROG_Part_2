using Microsoft.AspNetCore.Mvc;
using PROG_Part_2.Models;
using PROG_Part_2.Services;

namespace PROG_Part_2.Controllers
{
    public class FilesController : Controller
    {
        private readonly AzureFileShareService _fileShareService;
        public FilesController(AzureFileShareService fileShareService)
        {
            _fileShareService = fileShareService;
        }

        public async Task<IActionResult> Index()
        {
            List<Claims> files;
            try
            {
                files = await _fileShareService.ListFileAsync("uploads");
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Failed to load files :{ex.Message}";
                files = new List<Claims>();
            }
            return View(files);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please select a file to upload");
                return await Index();
            }
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    string directoryName = "uploads";
                    string fileName = file.FileName;
                    await _fileShareService.UploadFileAsync(directoryName, fileName, stream);
                }
                TempData["Message"] = $"File '{file.FileName}' upload successfully";
            }
            catch (Exception e)
            {
                TempData["Message"] = $"File upload failed: {e.Message}";
            }
            return RedirectToAction("Index");
        }  
    }
}
