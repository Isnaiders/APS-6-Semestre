using System.Drawing;
using ProjectWebMVC.Models.Enum;

namespace ProjectWebMVC.Models
{
    public class FiltersViewModel
    {
        public FiltersViewModel()
        {
        }

        public IFormFile OriginImage { get; set; }
        public string OriginImageName { get; set; }
        public string FilteredImageName { get; set; }
        public Bitmap OriginImageBit { get; set; }
        public Bitmap FilteredImageBit { get; set; }
        public FilterType Type { get; set; }
    }
}
