﻿namespace KuroganeHammer.Data.Api.DTOs
{
    public class CompletedCalculationResponseDto
    {
        public string CalculatedValueName { get; set;}
        public string CharacterName { get; set; }
        public string MoveName { get; set; }
        public double CalculatedValue { get; set; }
    }
}