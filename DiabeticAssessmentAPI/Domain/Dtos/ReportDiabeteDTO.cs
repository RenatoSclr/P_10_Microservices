﻿namespace DiabeticAssessmentAPI.Domain.Dtos
{
    public class ReportDiabeteDTO
    {
        public string NiveauRisque { get; set; }
        public List<string> Declencheurs { get; set; }
    }
}
