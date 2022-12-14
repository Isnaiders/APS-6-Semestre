<<<<<<< HEAD
﻿using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;
=======
﻿using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
>>>>>>> origin/teste
using static System.Net.Mime.MediaTypeNames;

namespace ProjectWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private string serverPath;
        private readonly ILogger<HomeController> _logger;
<<<<<<< HEAD
        public Bitmap imagem;
=======
        private Bitmap nova_imagem;
        private Bitmap nova_imagem1;
>>>>>>> origin/teste
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
<<<<<<< HEAD
        }

        public IActionResult Filters()
        {
            var model = new FiltersViewModel();
            ViewData["Title"] = "Filters";
            return View(model);
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

        [HttpPost]
        public IActionResult Filters(FiltersViewModel model)
        {
            string savePath = serverPath + "\\images\\";

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            using (var stream = System.IO.File.Create(savePath + model.Image.FileName))
            {
                model.Image.CopyToAsync(stream);
                model.OriginImage = new Bitmap(stream);
            }

            if (model.OriginImage == null)
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
=======
>>>>>>> origin/teste
        }

        public IActionResult AboutUs()
        {
            ViewData["Title"] = "Sobre Nós";
            return View();
        }

        public IActionResult Filters()
        {
<<<<<<< HEAD
            IEnumerable<Program> model = new List<Program>();
            ViewData["Title"] = "Como Usar";
=======
            var model = new FiltersViewModel();
            ViewData["Title"] = "Filtragem";

>>>>>>> origin/teste
            return View(model);
        }

        [HttpPost]
        public IActionResult Filters(FiltersViewModel model)
        {
<<<<<<< HEAD
            ViewData["Title"] = "Política de Privacidade";
            return View();
=======
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
                case Models.Enum.FilterType.Shine:
                    //model.FilteredImageBit = Brilho(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Contrast:
                    //model.FilteredImageBit = Contraste(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.GrayConvertion:
                    model.FilteredImageBit = GrayConvertion(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Equalization:
                    model.FilteredImageBit = Equalization(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.MirroringHorizontal:
                    model.FilteredImageBit = MirroringHorizontal(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.MirroringVertical:
                    model.FilteredImageBit = MirroringVertical(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Gaussiano:
                    model.FilteredImageBit = Gaussiano(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Histogram:
                    //model.FilteredImageBit = Histograma(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Laplaciano:
                    model.FilteredImageBit = Laplaciano(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Negative:
                    model.FilteredImageBit = Negative(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.HighPass:
                    model.FilteredImageBit = HighPass(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.LowPass:
                    model.FilteredImageBit = LowPass(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.PrewittHx:
                    model.FilteredImageBit = PrewittHx(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.PrewittHyHx:
                    model.FilteredImageBit = PrewittHyHx(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.Quantization:
                    model.FilteredImageBit = Quantization(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.AntiClockwiseRotation:
                    //model.FilteredImageBit = RotaçãoAnti(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ClockwiseRotationion:
                    //model.FilteredImageBit = Rotação(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.SobelHx:
                    model.FilteredImageBit = SobelHx(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.SobelHy:
                    model.FilteredImageBit = SobelHy(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ZoomIn:
                    //model.FilteredImageBit = ZoomIn(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ZoomOut:
                    //model.FilteredImageBit = ZoomOut(model.OriginImageBit);
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
>>>>>>> origin/teste
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

        //private Bitmap Shine(Bitmap image)
        //{
        //    try
        //    {
        //        int briR = Convert.ToInt32(toolStripMenuItem8.Text);
        //        int briG = Convert.ToInt32(toolStripMenuItem9.Text);
        //        int briB = Convert.ToInt32(toolStripMenuItem10.Text);

        //        if (briR > 255)
        //        {
        //            briR = 255;
        //            toolStripMenuItem8.Text = "255";
        //        }
        //        else if (briR < -255)
        //        {
        //            briR = -255;
        //            toolStripMenuItem8.Text = "-255";
        //        }

        //        if (briG > 255)
        //        {
        //            briG = 255;
        //            toolStripMenuItem9.Text = "255";
        //        }
        //        else if (briG < -255)
        //        {
        //            briG = -255;
        //            toolStripMenuItem9.Text = "-255";
        //        }

        //        if (briB > 255)
        //        {
        //            briB = 255;
        //            toolStripMenuItem10.Text = "255";
        //        }
        //        else if (briB < -255)
        //        {
        //            briB = -255;
        //            toolStripMenuItem10.Text = "-255";
        //        }


        //        int h = image.Width;
        //        int v = image.Height;
        //        int faixaR = 0;
        //        int faixaG = 0;
        //        int faixaB = 0;
        //        nova_imagem = new Bitmap(image.Width, image.Height);
        //        int i, u;
        //        for (i = 0; i < v; i++)
        //        {
        //            for (u = 0; u < h; u++)
        //            {
        //                faixaR = image.GetPixel(u, i).R + briR;
        //                if (faixaR > 255)
        //                    faixaR = 255;
        //                else if (faixaR < 0)
        //                    faixaR = 0;

        //                faixaG = image.GetPixel(u, i).G + briG;
        //                if (faixaG > 255)
        //                    faixaG = 255;
        //                else if (faixaG < 0)
        //                    faixaG = 0;

        //                faixaB = image.GetPixel(u, i).B + briB;
        //                if (faixaB > 255)
        //                    faixaB = 255;
        //                else if (faixaB < 0)
        //                    faixaB = 0;

        //                int trasn = image.GetPixel(u, i).A;

        //                nova_imagem.SetPixel(u, i, Color.FromArgb(trasn, faixaR, faixaG, faixaB));

        //            }
        //        }

        //        return nova_imagem;
        //    }
        //    catch { }
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

        private Bitmap Equalization(Bitmap image)
        {
            try
            {
                int h = 0, v = 0, i = 0;

                h = image.Width;
                v = image.Height;
                nova_imagem = GrayConvertion(image);

                double a = 255.0 / (Convert.ToDouble(h) * Convert.ToDouble(v));
                int[] Vcin_cumR = new int[256];
                int[] Vcin_cumG = new int[256];
                int[] Vcin_cumB = new int[256];

                int[] VcinR = new int[256];
                int[] VcinG = new int[256];
                int[] VcinB = new int[256];

                int x = 0, y = 0;
                for (y = 0; y < image.Height; y++)
                    for (x = 0; x < image.Width; x++)
                    {
                        Color c = image.GetPixel(x, y);
                        VcinR[c.R]++;
                        VcinG[c.G]++;
                        VcinB[c.B]++;
                    }

                Vcin_cumR[0] = Convert.ToInt32(a * Convert.ToDouble(VcinR[0]));
                for (i = 1; i <= 255; i++)
                {
                    Vcin_cumR[i] = Vcin_cumR[i - 1] + Convert.ToInt32(a * Convert.ToDouble(VcinR[i]));
                    if (Vcin_cumR[i] > 255)
                        Vcin_cumR[i] = 255;
                    else if (Vcin_cumR[i] < 0)
                        Vcin_cumR[i] = 0;
                }

                Vcin_cumG[0] = Convert.ToInt32(a * Convert.ToDouble(VcinG[0]));
                for (i = 1; i <= 255; i++)
                {
                    Vcin_cumG[i] = Vcin_cumG[i - 1] + Convert.ToInt32(a * Convert.ToDouble(VcinG[i]));
                    if (Vcin_cumG[i] > 255)
                        Vcin_cumG[i] = 255;
                    else if (Vcin_cumG[i] < 0)
                        Vcin_cumG[i] = 0;
                }


                Vcin_cumB[0] = Convert.ToInt32(a * Convert.ToDouble(VcinB[0]));
                for (i = 1; i <= 255; i++)
                {
                    Vcin_cumB[i] = Vcin_cumB[i - 1] + Convert.ToInt32(a * Convert.ToDouble(VcinB[i]));
                    if (Vcin_cumB[i] > 255)
                        Vcin_cumB[i] = 255;
                    else if (Vcin_cumB[i] < 0)
                        Vcin_cumB[i] = 0;
                }


                nova_imagem1 = new Bitmap(h, v);

                for (x = 0; x < h; x++)
                {
                    for (y = 0; y < v; y++)
                    {
                        int trasn = image.GetPixel(x, y).A;
                        int auxR = image.GetPixel(x, y).R;
                        int auxG = image.GetPixel(x, y).G;
                        int auxB = image.GetPixel(x, y).B;
                        int r = Vcin_cumR[auxR];
                        int g = Vcin_cumG[auxG];
                        int b = Vcin_cumB[auxB];
                        nova_imagem1.SetPixel(x, y, Color.FromArgb(trasn, r, g, b));
                    }
                }
                //pictureBox2.Image = nova_imagem1;

                //GrayConvertion(nova_imagem1);
                //Form3 newForm3 = new Form3();
                //newForm3.Vcin1 = Vcin2;
                //newForm3.Show();
            }
            catch { }

            return nova_imagem1;
        }

        private Bitmap MirroringHorizontal(Bitmap image)
        {
            int h = image.Width;
            int v = image.Height;
            nova_imagem = new Bitmap(image.Width, image.Height);
            int i, u;
            for (i = 0; i < v; i++)
            {
                for (u = 0; u < h; u++)
                {

                    nova_imagem.SetPixel(h - u - 1, i, image.GetPixel(u, i));

                }
            }

            return nova_imagem;
        }

        private Bitmap MirroringVertical(Bitmap image)
        {
            int h = image.Width;
            int v = image.Height;
            nova_imagem = new Bitmap(image.Width, image.Height);
            int i, u;
            for (u = 0; u < h; u++)
            {
                for (i = 0; i < v; i++)
                {

                    nova_imagem.SetPixel(u, v - i - 1, image.GetPixel(u, i));

                }
            }

            return nova_imagem;
        }

        private Bitmap Gaussiano(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);


                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 0.0625 +
                                     image.GetPixel(u, i - 1).R * 0.125 +
                                     image.GetPixel(u + 1, i - 1).R * 0.0625 +
                                     image.GetPixel(u - 1, i).R * 0.125 +
                                     image.GetPixel(u, i).R * 0.25 +
                                     image.GetPixel(u + 1, i).R * 0.125 +
                                     image.GetPixel(u - 1, i + 1).R * 0.0625 +
                                     image.GetPixel(u, i + 1).R * 0.125 +
                                     image.GetPixel(u + 1, i + 1).R * 0.0625;

                            colorG = image.GetPixel(u - 1, i - 1).G * 0.0625 +
                                    image.GetPixel(u, i - 1).G * 0.125 +
                                    image.GetPixel(u + 1, i - 1).G * 0.0625 +
                                    image.GetPixel(u - 1, i).G * 0.125 +
                                    image.GetPixel(u, i).G * 0.25 +
                                    image.GetPixel(u + 1, i).G * 0.125 +
                                    image.GetPixel(u - 1, i + 1).G * 0.0625 +
                                    image.GetPixel(u, i + 1).G * 0.125 +
                                    image.GetPixel(u + 1, i + 1).G * 0.0625;

                            colorB = image.GetPixel(u - 1, i - 1).B * 0.0625 +
                                   image.GetPixel(u, i - 1).B * 0.125 +
                                   image.GetPixel(u + 1, i - 1).B * 0.0625 +
                                   image.GetPixel(u - 1, i).B * 0.125 +
                                   image.GetPixel(u, i).B * 0.25 +
                                   image.GetPixel(u + 1, i).B * 0.125 +
                                   image.GetPixel(u - 1, i + 1).B * 0.0625 +
                                   image.GetPixel(u, i + 1).B * 0.125 +
                                   image.GetPixel(u + 1, i + 1).B * 0.0625;

                            if (colorB > 255)
                                colorB = 255;
                            else if (colorB < 0)
                                colorB = 0;


                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;


                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }

                        u = u + 1;

                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }

            return nova_imagem;
        }

        private Bitmap Laplaciano(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);

                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 0 +
                                     image.GetPixel(u, i - 1).R * -1 +
                                     image.GetPixel(u + 1, i - 1).R * 0 +
                                     image.GetPixel(u - 1, i).R * -1 +
                                     image.GetPixel(u, i).R * 4 +
                                     image.GetPixel(u + 1, i).R * -1 +
                                     image.GetPixel(u - 1, i + 1).R * 0 +
                                     image.GetPixel(u, i + 1).R * -1 +
                                     image.GetPixel(u + 1, i + 1).R * 0;

                            colorG = image.GetPixel(u - 1, i - 1).G * 0 +
                                     image.GetPixel(u, i - 1).G * -1 +
                                     image.GetPixel(u + 1, i - 1).G * 0 +
                                     image.GetPixel(u - 1, i).G * -1 +
                                     image.GetPixel(u, i).G * 4 +
                                     image.GetPixel(u + 1, i).G * -1 +
                                     image.GetPixel(u - 1, i + 1).G * 0 +
                                     image.GetPixel(u, i + 1).G * -1 +
                                     image.GetPixel(u + 1, i + 1).G * 0;

                            colorB = image.GetPixel(u - 1, i - 1).B * 0 +
                                     image.GetPixel(u, i - 1).B * -1 +
                                     image.GetPixel(u + 1, i - 1).B * 0 +
                                     image.GetPixel(u - 1, i).B * -1 +
                                     image.GetPixel(u, i).B * 4 +
                                     image.GetPixel(u + 1, i).B * -1 +
                                     image.GetPixel(u - 1, i + 1).B * 0 +
                                     image.GetPixel(u, i + 1).B * -1 +
                                     image.GetPixel(u + 1, i + 1).B * 0;

                            if (colorB > 255)
                                colorB = 255;
                            else if (colorB < 0)
                                colorB = 0;


                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;


                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }
                        u = u + 1;
                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }

            return nova_imagem;
        }

        private Bitmap Negative(Bitmap image)
        {
            try
            {
                int h = image.Width;
                int v = image.Height;
                int faixaR = 0;
                int faixaG = 0;
                int faixaB = 0;
                nova_imagem = new Bitmap(image.Width, image.Height);
                int i, u;
                for (i = 0; i < v; i++)
                {
                    for (u = 0; u < h; u++)
                    {
                        faixaR = 255 - image.GetPixel(u, i).R;

                        faixaG = 255 - image.GetPixel(u, i).G;

                        faixaB = 255 - image.GetPixel(u, i).B;

                        int trasn = image.GetPixel(u, i).A;

                        nova_imagem.SetPixel(u, i, Color.FromArgb(trasn, faixaR, faixaG, faixaB));
                    }
                }
            }
            catch { }

            return nova_imagem;
        }

        private Bitmap HighPass(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);

                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * -1 +
                                     image.GetPixel(u, i - 1).R * -1 +
                                     image.GetPixel(u + 1, i - 1).R * -1 +
                                     image.GetPixel(u - 1, i).R * -1 +
                                     image.GetPixel(u, i).R * 8 +
                                     image.GetPixel(u + 1, i).R * -1 +
                                     image.GetPixel(u - 1, i + 1).R * -1 +
                                     image.GetPixel(u, i + 1).R * -1 +
                                     image.GetPixel(u + 1, i + 1).R * -1;

                            colorG = image.GetPixel(u - 1, i - 1).G * -1 +
                                    image.GetPixel(u, i - 1).G * -1 +
                                    image.GetPixel(u + 1, i - 1).G * -1 +
                                    image.GetPixel(u - 1, i).G * -1 +
                                    image.GetPixel(u, i).G * 8 +
                                    image.GetPixel(u + 1, i).G * -1 +
                                    image.GetPixel(u - 1, i + 1).G * -1 +
                                    image.GetPixel(u, i + 1).G * -1 +
                                    image.GetPixel(u + 1, i + 1).G * -1;

                            colorB = image.GetPixel(u - 1, i - 1).B * -1 +
                                   image.GetPixel(u, i - 1).B * -1 +
                                   image.GetPixel(u + 1, i - 1).B * -1 +
                                   image.GetPixel(u - 1, i).B * -1 +
                                   image.GetPixel(u, i).B * 8 +
                                   image.GetPixel(u + 1, i).B * -1 +
                                   image.GetPixel(u - 1, i + 1).B * -1 +
                                   image.GetPixel(u, i + 1).B * -1 +
                                   image.GetPixel(u + 1, i + 1).B * -1;

                            if (colorB > 255)
                                colorB = 255;
                            else if (colorB < 0)
                                colorB = 0;


                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;

                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }

                        u = u + 1;

                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }
            return nova_imagem;
        }

        private Bitmap LowPass(Bitmap image)
        {
            try
            {

            }
            catch (Exception)
            {
            }
            return nova_imagem;
        }

        private Bitmap PrewittHx(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);


                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 1 +
                                     image.GetPixel(u, i - 1).R * 0 +
                                     image.GetPixel(u + 1, i - 1).R * -1 +
                                     image.GetPixel(u - 1, i).R * 1 +
                                     image.GetPixel(u, i).R * 0 +
                                     image.GetPixel(u + 1, i).R * -1 +
                                     image.GetPixel(u - 1, i + 1).R * 1 +
                                     image.GetPixel(u, i + 1).R * 0 +
                                     image.GetPixel(u + 1, i + 1).R * -1;

                            colorG = image.GetPixel(u - 1, i - 1).G * 1 +
                                    image.GetPixel(u, i - 1).G * 0 +
                                    image.GetPixel(u + 1, i - 1).G * -1 +
                                    image.GetPixel(u - 1, i).G * 1 +
                                    image.GetPixel(u, i).G * 0 +
                                    image.GetPixel(u + 1, i).G * -1 +
                                    image.GetPixel(u - 1, i + 1).G * 1 +
                                    image.GetPixel(u, i + 1).G * 0 +
                                    image.GetPixel(u + 1, i + 1).G * -1;

                            colorB = image.GetPixel(u - 1, i - 1).B * 1 +
                                   image.GetPixel(u, i - 1).B * 0 +
                                   image.GetPixel(u + 1, i - 1).B * -1 +
                                   image.GetPixel(u - 1, i).B * 1 +
                                   image.GetPixel(u, i).B * 0 +
                                   image.GetPixel(u + 1, i).B * -1 +
                                   image.GetPixel(u - 1, i + 1).B * 1 +
                                   image.GetPixel(u, i + 1).B * 0 +
                                   image.GetPixel(u + 1, i + 1).B * -1;

                            colorB = colorB + 127;
                            colorR = colorR + 127;
                            colorG = colorG + 127;

                            if (colorB > 255)
                            {
                                colorB = 255;
                            }
                            else if (colorB < 0)
                                colorB = 0;

                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;


                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }

                        u = u + 1;

                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }

            return nova_imagem;
        }

        private Bitmap PrewittHyHx(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);


                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 1 +
                                     image.GetPixel(u, i - 1).R * 1 +
                                     image.GetPixel(u + 1, i - 1).R * 1 +
                                     image.GetPixel(u - 1, i).R * 0 +
                                     image.GetPixel(u, i).R * 0 +
                                     image.GetPixel(u + 1, i).R * 0 +
                                     image.GetPixel(u - 1, i + 1).R * -1 +
                                     image.GetPixel(u, i + 1).R * -1 +
                                     image.GetPixel(u + 1, i + 1).R * -1;

                            colorG = image.GetPixel(u - 1, i - 1).G * 1 +
                                    image.GetPixel(u, i - 1).G * 1 +
                                    image.GetPixel(u + 1, i - 1).G * 1 +
                                    image.GetPixel(u - 1, i).G * 0 +
                                    image.GetPixel(u, i).G * 0 +
                                    image.GetPixel(u + 1, i).G * 0 +
                                    image.GetPixel(u - 1, i + 1).G * -1 +
                                    image.GetPixel(u, i + 1).G * -1 +
                                    image.GetPixel(u + 1, i + 1).G * -1;

                            colorB = image.GetPixel(u - 1, i - 1).B * 1 +
                                   image.GetPixel(u, i - 1).B * 1 +
                                   image.GetPixel(u + 1, i - 1).B * 1 +
                                   image.GetPixel(u - 1, i).B * 0 +
                                   image.GetPixel(u, i).B * 0 +
                                   image.GetPixel(u + 1, i).B * 0 +
                                   image.GetPixel(u - 1, i + 1).B * -1 +
                                   image.GetPixel(u, i + 1).B * -1 +
                                   image.GetPixel(u + 1, i + 1).B * -1;

                            colorB = colorB + 127;
                            colorR = colorR + 127;
                            colorG = colorG + 127;




                            if (colorB > 255)
                            {
                                colorB = 255;
                            }
                            else if (colorB < 0)
                                colorB = 0;


                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;

                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }
                        u = u + 1;
                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }

            return nova_imagem;
        }

        private Bitmap Quantization(Bitmap image)
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

        private Bitmap SobelHx(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);


                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 1 +
                                     image.GetPixel(u, i - 1).R * 0 +
                                     image.GetPixel(u + 1, i - 1).R * -1 +
                                     image.GetPixel(u - 1, i).R * 2 +
                                     image.GetPixel(u, i).R * 0 +
                                     image.GetPixel(u + 1, i).R * -2 +
                                     image.GetPixel(u - 1, i + 1).R * 1 +
                                     image.GetPixel(u, i + 1).R * 0 +
                                     image.GetPixel(u + 1, i + 1).R * -1;

                            colorG = image.GetPixel(u - 1, i - 1).G * 1 +
                                    image.GetPixel(u, i - 1).G * 0 +
                                    image.GetPixel(u + 1, i - 1).G * -1 +
                                    image.GetPixel(u - 1, i).G * 2 +
                                    image.GetPixel(u, i).G * 0 +
                                    image.GetPixel(u + 1, i).G * -2 +
                                    image.GetPixel(u - 1, i + 1).G * 1 +
                                    image.GetPixel(u, i + 1).G * 0 +
                                    image.GetPixel(u + 1, i + 1).G * -1;

                            colorB = image.GetPixel(u - 1, i - 1).B * 1 +
                                   image.GetPixel(u, i - 1).B * 0 +
                                   image.GetPixel(u + 1, i - 1).B * -1 +
                                   image.GetPixel(u - 1, i).B * 2 +
                                   image.GetPixel(u, i).B * 0 +
                                   image.GetPixel(u + 1, i).B * -2 +
                                   image.GetPixel(u - 1, i + 1).B * 1 +
                                   image.GetPixel(u, i + 1).B * 0 +
                                   image.GetPixel(u + 1, i + 1).B * -1;

                            colorB = colorB + 127;
                            colorR = colorR + 127;
                            colorG = colorG + 127;

                            if (colorB > 255)
                            {
                                colorB = 255;
                            }
                            else if (colorB < 0)
                                colorB = 0;

                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;

                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }
                        u = u + 1;
                    }
                    u = 0;
                    i = i + 1;
                }
            }

            catch { }
            return nova_imagem;
        }

        private Bitmap SobelHy(Bitmap image)
        {
            try
            {
                int i = 0;
                int u = 0;
                double colorR = 0;
                double colorG = 0;
                double colorB = 0;
                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(h, v);

                while (i < v)
                {
                    while (u < h)
                    {
                        if (u == 0 || i == 0 || u == h - 1 || i == v - 1)
                        {
                            nova_imagem.SetPixel(u, i, image.GetPixel(u, i));
                        }
                        else
                        {
                            colorR = image.GetPixel(u - 1, i - 1).R * 1 +
                                     image.GetPixel(u, i - 1).R * 2 +
                                     image.GetPixel(u + 1, i - 1).R * 1 +
                                     image.GetPixel(u - 1, i).R * 0 +
                                     image.GetPixel(u, i).R * 0 +
                                     image.GetPixel(u + 1, i).R * 0 +
                                     image.GetPixel(u - 1, i + 1).R * -1 +
                                     image.GetPixel(u, i + 1).R * -2 +
                                     image.GetPixel(u + 1, i + 1).R * -1;

                            colorG = image.GetPixel(u - 1, i - 1).G * 1 +
                                    image.GetPixel(u, i - 1).G * 2 +
                                    image.GetPixel(u + 1, i - 1).G * 1 +
                                    image.GetPixel(u - 1, i).G * 0 +
                                    image.GetPixel(u, i).G * 0 +
                                    image.GetPixel(u + 1, i).G * 0 +
                                    image.GetPixel(u - 1, i + 1).G * -1 +
                                    image.GetPixel(u, i + 1).G * -2 +
                                    image.GetPixel(u + 1, i + 1).G * -1;

                            colorB = image.GetPixel(u - 1, i - 1).B * 1 +
                                   image.GetPixel(u, i - 1).B * 2 +
                                   image.GetPixel(u + 1, i - 1).B * 1 +
                                   image.GetPixel(u - 1, i).B * 0 +
                                   image.GetPixel(u, i).B * 0 +
                                   image.GetPixel(u + 1, i).B * 0 +
                                   image.GetPixel(u - 1, i + 1).B * -1 +
                                   image.GetPixel(u, i + 1).B * -2 +
                                   image.GetPixel(u + 1, i + 1).B * -1;

                            colorB = colorB + 127;
                            colorR = colorR + 127;
                            colorG = colorG + 127;

                            if (colorB > 255)
                            {
                                colorB = 255;
                            }
                            else if (colorB < 0)
                                colorB = 0;

                            if (colorR > 255)
                                colorR = 255;
                            else if (colorR < 0)
                                colorR = 0;

                            if (colorG > 255)
                                colorG = 255;
                            else if (colorG < 0)
                                colorG = 0;

                            nova_imagem.SetPixel(u, i, Color.FromArgb(image.GetPixel(u, i).A, Convert.ToInt32(colorR), Convert.ToInt32(colorG), Convert.ToInt32(colorB)));
                        }
                        u = u + 1;
                    }
                    u = 0;
                    i = i + 1;
                }
            }
            catch { }

            return nova_imagem;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}