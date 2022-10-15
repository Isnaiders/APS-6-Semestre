using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Bitmap imagem;
        public int[] Vcin = new int[256];
        public int[] Vcin2 = new int[256];

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult Filters()
        {
            var model = new FiltersViewModel();
            ViewData["Title"] = "Filters";
            return View(model);
        }

        [HttpPost]
        public IActionResult Filters(FiltersViewModel model)
        {
            if(model.OriginImage == null)
                return View(model);

            switch (model.Type)
            {
                case Models.Enum.FilterType.Unknown:
                    break;
                case Models.Enum.FilterType.ConversãoCinza:
                    model.FilteredImage = GrayConvertion(model.OriginImage);
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

            return View(model);
        }

        //public Bitmap GetImage()
        //{
        //    WebImage fileName = WebImage
        //    return Image.FromFile(fileName);
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
                    int trasn = imagem.GetPixel(x, y).A;
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