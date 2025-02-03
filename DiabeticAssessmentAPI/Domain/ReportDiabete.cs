namespace DiabeticAssessmentAPI.Domain
{
    public class ReportDiabete
    {
        public RiskLevel NiveauRisque { get; set; }
        public List<string> Declencheurs { get; set; }
    }

    public enum RiskLevel
    {
        None,
        Borderline,
        InDanger,
        EarlyOnset
    }
}
