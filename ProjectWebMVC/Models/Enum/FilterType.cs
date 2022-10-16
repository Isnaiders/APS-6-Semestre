using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType
    {
        [Display(Name = "Não Selecionado")]
        Unknown,
        [Display(Name = "Brilho")]
        Brilho,
        [Display(Name = "Contraste")]
        Contraste,
        [Display(Name = "Conversão Cinza")]
        ConversãoCinza,
        [Display(Name = "Equalização")]
        Equalização,
        [Display(Name = "Espelhamento")]
        Espelhamento,
        [Display(Name = "Gaussiano")]
        Gaussiano,
        [Display(Name = "Histograma")]
        Histograma,
        [Display(Name = "Laplaciano")]
        Laplaciano,
        [Display(Name = "Negativo")]
        Negativo,
        [Display(Name = "Passa Alta")]
        PassaAlta,
        [Display(Name = "Passa Baixa")]
        PassaBaixa,
        [Display(Name = "Prewitt")]
        Prewitt,
        [Display(Name = "Quantização")]
        Quantização,
        [Display(Name = "Rotação Anti-horária")]
        RotaçãoAnti,
        [Display(Name = "Rotação no Sentido Horário")]
        Rotação,
        [Display(Name = "Sobel")]
        Sobel,
        [Display(Name = "ZoomIn")]
        ZoomIn,
        [Display(Name = "ZoomOut")]
        ZoomOut
    }
}
