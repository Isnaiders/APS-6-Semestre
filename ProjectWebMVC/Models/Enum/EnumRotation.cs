using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum EnumRotation : int
    {
        [Display(Name = "Não Selecionado")]
        Zero,
        [Display(Name = "90°")]
        Ninety,
        [Display(Name = "180°")]
        OneHundredEighty,
        [Display(Name = "270°")]
        TwoHundredSeventy
    }
}
