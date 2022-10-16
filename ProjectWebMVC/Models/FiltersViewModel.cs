using System.Drawing;
using ProjectWebMVC.Models.Enum;

namespace ProjectWebMVC.Models
{
    public class FiltersViewModel
    {
        public FiltersViewModel()
        {
        }

        public IFormFile Image { get; set; }
        public Bitmap OriginImage { get; set; }
        public Bitmap FilteredImage { get; set; }
        public FilterType Type { get; set; }
    }
}
