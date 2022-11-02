using ProjectWebMVC.Models.Enum;

namespace ProjectWebMVC.Models
{
	public class FiltersConfigViewModel
	{
		public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public EnumRotation Rotation { get; set; }
        public int Quantization { get; set; }
        public int ZoomX { get; set; }
        public int ZoomY { get; set; }
    }
}