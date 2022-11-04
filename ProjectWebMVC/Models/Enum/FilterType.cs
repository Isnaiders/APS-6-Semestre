<<<<<<< HEAD
﻿namespace ProjectWebMVC.Models.Enum
{
    public enum FilterType : int
    {
        Unknown = 0,
        ConversãoCinza = 1,
        PassaBaixa = 2,
        PassaAlta = 3,
        Gaussiano = 4,
        Laplaciano = 5,
        Prewitt = 6,
        Sobel = 7,
        Histograma = 8,
        Equalização = 9,
        Brilho = 10,
        Contraste = 11,
        Negativo = 12,
        Espelhamento = 13,
        Quantização = 14,
        ZoomIn = 15,
        ZoomOut = 16,
        RotaçãoAnti = 17,
        Rotação = 18
=======
﻿using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Espelhamento Horizontal")]
        MirroringHorizontal,
        [Display(Name = "Espelhamento Vertical")]
        MirroringVertical,
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
>>>>>>> origin/teste
    }
}
