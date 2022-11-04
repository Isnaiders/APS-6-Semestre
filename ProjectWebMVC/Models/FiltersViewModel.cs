using System.Drawing;
using ProjectWebMVC.Models.Enum;

namespace ProjectWebMVC.Models
{
    public class FiltersViewModel
    {
        public FiltersViewModel()
        {
        }

<<<<<<< HEAD
        public IFormFile Image { get; set; }
        public string File { get; set; }
        public Bitmap OriginImage { get; set; }
        public Bitmap FilteredImage { get; set; }
=======
        public IFormFile OriginImage { get; set; }
        public string FilteredImageName { get; set; }
        public Bitmap OriginImageBit { get; set; }
        public Bitmap FilteredImageBit { get; set; }
>>>>>>> origin/teste
        public FilterType Type { get; set; }
    }
}
