using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectWebMVC.Models;
using ProjectWebMVC.Models.Enum;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private string serverPath;
        private readonly ILogger<HomeController> _logger;
        public Bitmap imagem;
        public int[] Vcin = new int[256];
        public int[] Vcin2 = new int[256];

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment system)
        {
            _logger = logger;
            serverPath = system.WebRootPath;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult Filters()
        {
            var model = new FiltersViewModel();
            ViewData["Title"] = "Filtragem";

            return View(model);
        }

        [HttpPost]
        public IActionResult Filters(FiltersViewModel model)
        {
            if (model.OriginImage == null)
            {
                ModelState.AddModelError("File", "Nenhuma imagem foi selecionada");
                return View(model);
            }

            string savePath = serverPath + "\\images\\";

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            using (var stream = System.IO.File.Create(savePath + model.OriginImage.FileName))
            {
                try
                {
                    model.OriginImage.CopyToAsync(stream);
                    model.OriginImageBit = new Bitmap(stream);
                }
                catch (Exception)
                {
                    return View(model);
                }
            }

            switch (model.Type)
            {
                case Models.Enum.FilterType.Unknown:
                    break;
                case Models.Enum.FilterType.ConversãoCinza:
                    model.FilteredImageBit = GrayConvertion(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.PassaBaixa:
                    break;
                case Models.Enum.FilterType.PassaAlta:
                    break;
                case Models.Enum.FilterType.Gaussiano:
                    break;
                case Models.Enum.FilterType.Laplaciano:
                    break;
                case Models.Enum.FilterType.Prewitt:
                    break;
                case Models.Enum.FilterType.Sobel:
                    break;
                case Models.Enum.FilterType.Histograma:
                    break;
                case Models.Enum.FilterType.Equalização:
                    break;
                case Models.Enum.FilterType.Brilho:
                    break;
                case Models.Enum.FilterType.Contraste:
                    break;
                case Models.Enum.FilterType.Negativo:
                    break;
                case Models.Enum.FilterType.Espelhamento:
                    break;
                case Models.Enum.FilterType.Quantização:
                    break;
                case Models.Enum.FilterType.ZoomIn:
                    break;
                case Models.Enum.FilterType.ZoomOut:
                    break;
                case Models.Enum.FilterType.RotaçãoAnti:
                    break;
                case Models.Enum.FilterType.Rotação:
                    break;
                default:
                    break;
            }

            //--< Output as .Jpg >--
            var filteredImageName = "Filtered_" + model.OriginImage.FileName;
            var outputPath = savePath + filteredImageName;
            using (var output = System.IO.File.Open(outputPath, FileMode.Create))
            {
                const long quality = 50L;
                //< setup jpg >
                var qualityParamId = Encoder.Quality;

                var encoderParameters = new EncoderParameters(1);

                encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                //</ setup jpg >

                //< save Bitmap as Jpg >
                var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                model.FilteredImageBit.Save(output, codec, encoderParameters);
                //</ save Bitmap as Jpg >
            }
            //--< Output as .Jpg >--

            model.FilteredImageName = filteredImageName;

            return View(model);
        }

        //[HttpPost]
        //public IActionResult GetImage(FiltersViewModel model)
        //{
        //    string savePath = serverPath + "\\images\\";

        //    if (!Directory.Exists(savePath))
        //        Directory.CreateDirectory(savePath);

        //    using (var stream = System.IO.File.Create(savePath + model.OriginImage.FileName))
        //    {
        //        model.OriginImage.CopyToAsync(stream);
        //        model.OriginImage.CopyToAsync(stream);
        //    }

        //    //--< Upload Form >--
        //    string srcImage_Path = "wwwroot/images/me.jpg";
        //    string resizeImage_Path = "wwwroot/images/mizinho.jpg";
        //    int new_Size = 500;
        //    Image_resize(srcImage_Path, resizeImage_Path, new_Size);
        //    //--</ Upload Form >--

        //    return RedirectToAction("Filters", model);
        //}

        private Bitmap GrayConvertion(Bitmap image)
        {
            Bitmap grayScale = new Bitmap(image.Width, image.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
            {
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    Color c = image.GetPixel(x, y);

                    Int32 gs = (Int32)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    Vcin2[gs]++;
                    int trasn = image.GetPixel(x, y).A;
                    grayScale.SetPixel(x, y, Color.FromArgb(trasn, gs, gs, gs));
                }
            }

            return grayScale;
        }

        public IActionResult AboutUs()
        {
            ViewData["Title"] = "Sobre Nós";
            return View();
        }

        public IActionResult HowToUse()
        {
            IEnumerable<Program> model = new List<Program>();
            ViewData["Title"] = "Como Usar";
            return View(model);
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Política de Privacidade";
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}