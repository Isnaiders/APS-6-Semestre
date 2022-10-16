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
        private Bitmap imagem;
        private Bitmap nova_imagem;
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
                    //model.FilteredImageBit = PassaBaixa(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.PassaAlta:
                    //model.FilteredImageBit = PassaAlta(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Gaussiano:
                    //model.FilteredImageBit = Gaussiano(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Laplaciano:
                    //model.FilteredImageBit = Laplaciano(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Prewitt:
                    //model.FilteredImageBit = Prewitt(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Sobel:
                    //model.FilteredImageBit = Sobel(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Histograma:
                    //model.FilteredImageBit = Histograma(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Equalização:
                    //model.FilteredImageBit = Equalização(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Brilho:
                    //model.FilteredImageBit = Brilho(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Contraste:
                    //model.FilteredImageBit = Contraste(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Negativo:
                    //model.FilteredImageBit = Negativo(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Espelhamento:
                    //model.FilteredImageBit = Espelhamento(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Quantização:
                    model.FilteredImageBit = Quantizacao(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ZoomIn:
                    //model.FilteredImageBit = ZoomIn(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ZoomOut:
                    //model.FilteredImageBit = ZoomOut(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.RotaçãoAnti:
                    //model.FilteredImageBit = RotaçãoAnti(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Rotação:
                    //model.FilteredImageBit = Rotação(model.OriginImageBit);
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

        private Bitmap Quantizacao(Bitmap image)
        {
            try
            {
                //int quan = quan = Convert.ToInt32(valor.Text); // Implementar uma caixa para o usuário digitar o valor da quantização
                int quan = 16;

                if (quan > 256)
                    quan = 256;
                else if (quan <= 0)
                    quan = 1;

                int dist = 256 / quan;

                int h = image.Width;
                int v = image.Height;
                int novo_valorR = 0;
                int novo_valorG = 0;
                int novo_valorB = 0;
                int faixaR = 0;
                int faixaG = 0;
                int faixaB = 0;
                nova_imagem = new Bitmap(image.Width, image.Height);
                int i, u;
                for (i = 0; i < v; i++)
                {
                    for (u = 0; u < h; u++)
                    {
                        faixaR = image.GetPixel(u, i).R / dist;
                        faixaG = image.GetPixel(u, i).G / dist;
                        faixaB = image.GetPixel(u, i).B / dist;
                        int trasn = image.GetPixel(u, i).A;

                        novo_valorR = faixaR * dist + (dist / 2);
                        novo_valorG = faixaG * dist + (dist / 2);
                        novo_valorB = faixaB * dist + (dist / 2);

                        if (novo_valorR > 255)
                            novo_valorR = 255;

                        if (novo_valorG > 255)
                            novo_valorG = 255;

                        if (novo_valorB > 255)
                            novo_valorB = 255;

                        nova_imagem.SetPixel(u, i, Color.FromArgb(trasn, novo_valorR, novo_valorG, novo_valorB));

                    }
                }
            }
            catch { }
            return nova_imagem;
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