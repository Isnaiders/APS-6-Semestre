using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType : int
    {
        [Display(Name = "Não Selecionado")]
        Unknown,
        [Display(Name = "Brilho")]
        Shine,
        [Display(Name = "Contraste")]
        Contrast,
        [Display(Name = "Conversão Cinza")]
        GrayConvertion,
        [Display(Name = "Equalização")]
        Equalization,
        [Display(Name = "Espelhamento Horizontal")]
        MirroringHorizontal,
        [Display(Name = "Espelhamento Vertical")]
        MirroringVertical,
        [Display(Name = "Gaussiano")]
        Gaussiano,
        [Display(Name = "Laplaciano")]
        Laplaciano,
        [Display(Name = "Negativo")]
        Negative,
        [Display(Name = "Passa Alta")]
        HighPass,
        [Display(Name = "Prewitt Hx")]
        PrewittHx,
        [Display(Name = "Prewitt Hy Hx")]
        PrewittHyHx,
        [Display(Name = "Quantização")]
        Quantization,
        [Display(Name = "Rotação Anti-horária")]
        AntiClockwiseRotation,
        [Display(Name = "Rotação no Sentido Horário")]
        ClockwiseRotation,
        [Display(Name = "Sobel Hx")]
        SobelHx,
        [Display(Name = "Sobel Hy")]
        SobelHy,
        [Display(Name = "ZoomIn")]
        ZoomIn,
        [Display(Name = "ZoomOut")]
        ZoomOut
    }
}
