using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;
using ProjectWebMVC.Models.Enum;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private string serverPath;
        private readonly ILogger<HomeController> _logger;
        private Bitmap nova_imagem;
        private Bitmap nova_imagem1;
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

        public IActionResult AboutUs()
        {
            ViewData["Title"] = "Sobre Nós";
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
            if (model.Type == Models.Enum.FilterType.Unknown)
            {
                ModelState.AddModelError("Type", "Nenhum filtro foi selecionado");
                ViewBag.Warning = true;
                return View(model);
            }
            if (model.OriginImage == null && model.OriginImageName == null)
            {
                ModelState.AddModelError("OriginImage", "Nenhuma imagem foi selecionada");
                ViewBag.Warning = true;
                return View(model);
            }

            string savePath = serverPath + "\\images\\";
            string fileName = string.IsNullOrEmpty(model.OriginImageName) ? model.OriginImage?.FileName : model.OriginImageName; ;
            ViewBag.Success = false;
            ViewBag.Warning = false;

            if (model.OriginImage == null && !string.IsNullOrEmpty(fileName))
            {
                using (var stream = System.IO.File.OpenRead(savePath + fileName))
                {
                    model.OriginImageBit = new Bitmap(stream.Name, true);
                }
            }

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            if (model.OriginImageBit == null)
            {
                using (var stream = System.IO.File.Create(savePath + fileName))
                {
                    try
                    {
                        model.OriginImageName = fileName;
                        model.OriginImage.CopyToAsync(stream);
                        model.OriginImageBit = new Bitmap(stream);
                    }
                    catch (Exception)
                    {
                        ViewBag.Warning = true;
                        return View(model);
                    }
                }
            }

            switch (model.Type)
            {
                case Models.Enum.FilterType.Unknown:
                    return View(model);
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
                    model.FilteredImageBit = Quantization(model.OriginImageBit, model.FiltersConfig);
                    break;
                case Models.Enum.FilterType.AntiClockwiseRotation:
                    model.FilteredImageBit = AntiClockwiseRotation(model.OriginImageBit, model.FiltersConfig);
                    break;
                case Models.Enum.FilterType.ClockwiseRotation:
                    model.FilteredImageBit = ClockwiseRotation(model.OriginImageBit, model.FiltersConfig);
                    break;
                case Models.Enum.FilterType.SobelHx:
                    model.FilteredImageBit = SobelHx(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.SobelHy:
                    model.FilteredImageBit = SobelHy(model.OriginImageBit);
                    break;
                case Models.Enum.FilterType.ZoomIn:
                    model.FilteredImageBit = ZoomIn(model.OriginImageBit, model.FiltersConfig);
                    break;
                case Models.Enum.FilterType.ZoomOut:
                    model.FilteredImageBit = ZoomOut(model.OriginImageBit, model.FiltersConfig);
                    break;
                default:
                    break;
            }

            //--< Output as .Jpg >--
            //string savePath = serverPath + "\\images\\";
            var filteredImageName = Guid.NewGuid() + "_Filtered_" + fileName;
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
            ViewBag.Success = true;

            return View(model);
        }

        public IActionResult FiltersConfig(string type, string originImageName, string filteredImageName)
        {
            var model = new FiltersViewModel();
            model.Type = (FilterType)Enum.Parse(typeof(FilterType), type);
            model.OriginImageName = !string.IsNullOrEmpty(originImageName) ? originImageName : "no-image.jpg";
            model.FilteredImageName = !string.IsNullOrEmpty(filteredImageName) ? filteredImageName : "no-image.jpg";
            return View(model);
        }

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

        private Bitmap AntiClockwiseRotation(Bitmap image, FiltersConfigViewModel filtersConfig)
        {
            try
            {
                int i = 0;
                int u = 0;

                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(v, h);


                while (i < v)
                {
                    while (u < h)
                    {

                        nova_imagem.SetPixel(i, h - u - 1, image.GetPixel(u, i));
                        u = u + 1;

                    }
                    u = 0;
                    i = i + 1;
                }

                //MessageBox.Show("Salve a imagem para continuar rotando ela");

            }

            catch { }

            return nova_imagem;
        }

        private Bitmap ClockwiseRotation(Bitmap image, FiltersConfigViewModel filtersConfig)
        {
            try
            {
                int i = 0;
                int u = 0;

                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(v, h);


                while (u < h)
                {
                    while (i < v)
                    {

                        nova_imagem.SetPixel(v - i - 1, u, image.GetPixel(u, i));
                        i = i + 1;

                    }
                    i = 0;
                    u = u + 1;
                }

                //MessageBox.Show("Salve a imagem para continuar rotando ela");
            }

            catch { }

            return nova_imagem;
        }

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

        private Bitmap Quantization(Bitmap image, FiltersConfigViewModel filtersConfig)
        {
            try
            {
                //int quan = quan = Convert.ToInt32(valor.Text); // Implementar uma caixa para o usuário digitar o valor da quantização
                int quan = filtersConfig.Quantization;

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

        private Bitmap ZoomIn(Bitmap image, FiltersConfigViewModel filtersConfig)
        {
            try
            {
                int i = 0;
                int u = 0;
                int i2 = 0;
                int u2 = 0;
                int mediaR = 0;
                int mediaG = 0;
                int mediaB = 0;
                int mediaA = 0;

                int h = image.Width;
                int v = image.Height;
                nova_imagem = new Bitmap(2 * h, 2 * v);


                while (i < v)
                {
                    while (u < h)
                    {

                        nova_imagem.SetPixel(u2, i2, image.GetPixel(u, i));

                        u = u + 1;
                        u2 = u2 + 2;
                    }
                    u = 0;
                    u2 = 0;
                    i = i + 1;
                    i2 = i2 + 2;
                }


                u = 0;
                i = 0;
                while (i < 2 * v)
                {
                    while (u < 2 * h)
                    {
                        if (u % 2 == 1)
                        {
                            if (u + 1 >= 2 * h)
                            {
                                mediaR = (nova_imagem.GetPixel(u - 1, i).R);
                                mediaG = (nova_imagem.GetPixel(u - 1, i).G);
                                mediaB = (nova_imagem.GetPixel(u - 1, i).B);
                                mediaA = (nova_imagem.GetPixel(u - 1, i).A);
                            }
                            else
                            {
                                mediaR = (nova_imagem.GetPixel(u - 1, i).R + nova_imagem.GetPixel(u + 1, i).R) / 2;
                                mediaG = (nova_imagem.GetPixel(u - 1, i).G + nova_imagem.GetPixel(u + 1, i).G) / 2;
                                mediaB = (nova_imagem.GetPixel(u - 1, i).B + nova_imagem.GetPixel(u + 1, i).B) / 2;
                                mediaA = (nova_imagem.GetPixel(u - 1, i).A + nova_imagem.GetPixel(u + 1, i).A) / 2;
                            }
                            nova_imagem.SetPixel(u, i, Color.FromArgb(mediaA, mediaR, mediaG, mediaB));
                        }

                        u = u + 1;
                    }
                    u = 0;
                    i = i + 2;
                }



                u = 0;
                i = 0;
                while (u < 2 * h)
                {
                    while (i < 2 * v)
                    {
                        if (i % 2 == 1)
                        {
                            if (i + 1 >= 2 * v)
                            {
                                mediaR = (nova_imagem.GetPixel(u, i - 1).R);
                                mediaG = (nova_imagem.GetPixel(u, i - 1).G);
                                mediaB = (nova_imagem.GetPixel(u, i - 1).B);
                                mediaA = (nova_imagem.GetPixel(u, i - 1).A);
                            }
                            else
                            {
                                mediaR = (nova_imagem.GetPixel(u, i - 1).R + nova_imagem.GetPixel(u, i + 1).R) / 2;
                                mediaG = (nova_imagem.GetPixel(u, i - 1).G + nova_imagem.GetPixel(u, i + 1).G) / 2;
                                mediaB = (nova_imagem.GetPixel(u, i - 1).B + nova_imagem.GetPixel(u, i + 1).B) / 2;
                                mediaA = (nova_imagem.GetPixel(u, i - 1).A + nova_imagem.GetPixel(u, i + 1).A) / 2;
                            }
                            nova_imagem.SetPixel(u, i, Color.FromArgb(mediaA, mediaR, mediaG, mediaB));
                        }

                        i = i + 1;
                    }
                    i = 0;
                    u = u + 1;
                }
            }
            catch { }
            return nova_imagem;
        }

        private Bitmap ZoomOut(Bitmap image, FiltersConfigViewModel filtersConfig)
        {
            try
            {
                int h = image.Width;
                int v = image.Height;
                int i = 0;
                int u = 0;

                int acumulado_i = 0;
                int acumulado_u = 0;

                int i2 = 0;
                int u2 = 0;
                int a = 0;
                float mediaR = 0;
                float mediaG = 0;
                float mediaB = 0;
                float mediaA = 0;

                int hori = filtersConfig.ZoomX;
                int verti = filtersConfig.ZoomY;

                if (hori <= h)
                {
                    if (verti <= v)
                    {


                        nova_imagem = new Bitmap(hori, verti);

                        int fator_x = h / hori;
                        int fator_y = v / verti;

                        while (i < verti)
                        {


                            while (u < hori)
                            {


                                while (i2 < fator_y)
                                {
                                    if (acumulado_i + i2 <= v)
                                    {
                                        mediaR = mediaR + image.GetPixel(acumulado_u, acumulado_i + i2).R;
                                        mediaG = mediaG + image.GetPixel(acumulado_u, acumulado_i + i2).G;
                                        mediaB = mediaB + image.GetPixel(acumulado_u, acumulado_i + i2).B;
                                        mediaA = mediaA + image.GetPixel(acumulado_u, acumulado_i + i2).A;
                                        a++;

                                    }

                                    i2 = i2 + 1;

                                }
                                while (u2 < fator_x)
                                {
                                    if (acumulado_u + u2 <= h)
                                    {
                                        mediaR = mediaR + image.GetPixel(acumulado_u + u2, acumulado_i).R;
                                        mediaG = mediaG + image.GetPixel(acumulado_u + u2, acumulado_i).G;
                                        mediaB = mediaB + image.GetPixel(acumulado_u + u2, acumulado_i).B;
                                        mediaA = mediaA + image.GetPixel(acumulado_u + u2, acumulado_i).A;
                                        a++;
                                    }

                                    u2 = u2 + 1;
                                }
                                mediaA = mediaA / a;
                                mediaR = mediaR / a;
                                mediaG = mediaG / a;
                                mediaB = mediaB / a;


                                nova_imagem.SetPixel(u, i, Color.FromArgb(Convert.ToInt32(mediaA), Convert.ToInt32(mediaR), Convert.ToInt32(mediaG), Convert.ToInt32(mediaB)));
                                mediaA = 0;
                                mediaR = 0;
                                mediaG = 0;
                                mediaB = 0;
                                i2 = 0;
                                u2 = 0;
                                a = 0;
                                u = u + 1;
                                acumulado_u = acumulado_u + fator_x;
                            }
                            u = 0;
                            acumulado_u = 0;


                            i = i + 1;
                            acumulado_i = acumulado_i + fator_y;
                        }
                    }

                    else
                    {
                        //MessageBox.Show("Tamanho vertical maior que imagem original");
                    }
                }
                else
                {
                    //MessageBox.Show("Tamanho horizontal maior que imagem original");
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