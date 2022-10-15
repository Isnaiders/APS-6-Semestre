using Microsoft.AspNetCore.Mvc;

namespace ProjectWebMVC.Controllers
{
    public class ImageController : Controller
    {
        private string serverPath;

        public ImageController(IWebHostEnvironment system)
        {
            serverPath = system.WebRootPath;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image)
        {
            string savePath = serverPath + "\\images\\";

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            using (var stream = System.IO.File.Create(savePath + image.FileName))
                image.CopyToAsync(stream);

            return RedirectToAction("Upload");
        }
    }
}
