using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType
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
        [Display(Name = "Espelhamento")]
        Mirroring,
        [Display(Name = "Gaussiano")]
        Gaussiano,
        [Display(Name = "Histograma")]
        Histogram,
        [Display(Name = "Laplaciano")]
        Laplaciano,
        [Display(Name = "Negativo")]
        Negative,
        [Display(Name = "Passa Alta")]
        HighPass,
        [Display(Name = "Passa Baixa")]
        LowPass,
        [Display(Name = "Prewitt Hx")]
        PrewittHx,
        [Display(Name = "Prewitt Hy Hx")]
        PrewittHyHx,
        [Display(Name = "Quantização")]
        Quantization,
        [Display(Name = "Rotação Anti-horária")]
        AntiClockwiseRotation,
        [Display(Name = "Rotação no Sentido Horário")]
        ClockwiseRotationion,
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
