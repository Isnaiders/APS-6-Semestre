using System.ComponentModel.DataAnnotations;

namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType
    {
        [Display(Name = "Não Selecionado")]
        Unknown,
        [Display(Name = "ConversãoCinza")]
        ConversãoCinza,
        [Display(Name = "PassaBaixa")]
        PassaBaixa,
        [Display(Name = "PassaAlta")]
        PassaAlta,
        [Display(Name = "Gaussiano")]
        Gaussiano,
        [Display(Name = "Laplaciano")]
        Laplaciano,
        [Display(Name = "Prewitt")]
        Prewitt,
        [Display(Name = "Sobel")]
        Sobel,
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
        [Display(Name = "RotaçãoAnti")]
        RotaçãoAnti,
        [Display(Name = "Rotação")]
        Rotação
    }
}
