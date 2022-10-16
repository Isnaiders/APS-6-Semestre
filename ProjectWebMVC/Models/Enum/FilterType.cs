using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType
    {
        [Display(Name = "Não Selecionado")]
        Unknown,
        [Display(Name = "Conversão Cinza")]
        ConversãoCinza,
        [Display(Name = "Passa Baixa")]
        PassaBaixa,
        [Display(Name = "Passa Alta")]
        PassaAlta,
        [Display(Name = "Gaussiano")]
        Gaussiano,
        [Display(Name = "Laplaciano")]
        Laplaciano,
        [Display(Name = "Prewitt Hx")]
        PrewittHx,
        [Display(Name = "Prewitt Hy Hx")]
        PrewittHyHx,
        [Display(Name = "Sobel Hx")]
        SobelHx,
        [Display(Name = "Sobel Hy")]
        SobelHy,
        [Display(Name = "Histograma")]
        Histograma,
        [Display(Name = "Equalização")]
        Equalização,
        [Display(Name = "Brilho")]
        Brilho,
        [Display(Name = "Contraste")]
        Contraste,
        [Display(Name = "Negativo")]
        Negativo,
        [Display(Name = "Espelhamento")]
        Espelhamento,
        [Display(Name = "Quantização")]
        Quantização,
        [Display(Name = "ZoomIn")]
        ZoomIn,
        [Display(Name = "ZoomOut")]
        ZoomOut,
        [Display(Name = "Rotação Anti-horária")]
        RotaçãoAnti,
        [Display(Name = "Rotação no Sentido Horário")]
        Rotação
    }
}
