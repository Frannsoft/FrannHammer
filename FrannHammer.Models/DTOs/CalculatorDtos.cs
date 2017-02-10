using System;

namespace FrannHammer.Models.DTOs
{
    [Obsolete]
    public class CompletedCalculationResponseDto
    {
        public string CalculatedValueName { get; set; }
        public string CharacterName { get; set; }
        public string MoveName { get; set; }
        public double CalculatedValue { get; set; }
    }
}