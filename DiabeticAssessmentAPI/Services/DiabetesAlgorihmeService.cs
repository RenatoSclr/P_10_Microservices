﻿using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Services.IServices;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabetesAlgorihmeService : IDiabetesAlgorihmeService
    {
        public ReportDiabete GetDiabeteRisk(InfoPatient infoPatient, List<ContenuNotePatient> contenuNoteDTOs)
        {
            var age = CalculateAge(infoPatient.DateNaissance);
            var triggers = GetTriggers(contenuNoteDTOs);
            var count = triggers.Count();
            RiskLevel resultRisk;
            if (age >= 30)
            {
                resultRisk = EvaluateRiskForAgeGreaterThanThirty(count);
            }
            else
            {
                resultRisk = EvaluateRiskForAgeLowerThanThirty(age, infoPatient.Genre, count);
            }     
            
            return new ReportDiabete
            {
                NiveauRisque = resultRisk,
                Declencheurs = triggers.ToList()
            };
        }

        private int CalculateAge(DateTime dateNaissance)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateNaissance.Year;
            if (dateNaissance > now.AddYears(-age))
                age--;
            return age;
        }

        private IEnumerable<string> GetTriggers(List<ContenuNotePatient> contenuNoteDTOs)
        {
            return Declencheurs.Liste
                .Where(declencheur => contenuNoteDTOs.Any(note =>
                    note.Contenu.Contains(declencheur, StringComparison.OrdinalIgnoreCase) ||
                    note.Contenu.Contains(RemoveTrailingS(declencheur), StringComparison.OrdinalIgnoreCase)))
                .Distinct();
        }

        private string RemoveTrailingS(string input)
        {
            if (input.EndsWith("s", StringComparison.OrdinalIgnoreCase) && input.Length > 1)
                return input.Substring(0, input.Length - 1);
            return input;
        }

        private RiskLevel EvaluateRiskForAgeGreaterThanThirty(int count)
        {
            if (count >= 8) 
                return RiskLevel.EarlyOnset;

            if (count == 6 || count == 7) 
                return RiskLevel.InDanger;

            if (count >= 2 && count <= 5) 
                return RiskLevel.Borderline;

            return RiskLevel.None;
        }

        private RiskLevel EvaluateRiskForAgeLowerThanThirty(int age, string genre, int declencheurCount)
        {
            bool isMale = genre.Equals("Masculin", StringComparison.OrdinalIgnoreCase);
            bool isFemale = genre.Equals("Féminin", StringComparison.OrdinalIgnoreCase);

            if (isMale)
            {
                if (declencheurCount >= 5)
                    return RiskLevel.EarlyOnset;

                if (declencheurCount == 3 || declencheurCount == 4)
                    return RiskLevel.InDanger;
            }

            if (isFemale)
            {
                if (declencheurCount >= 7)
                    return RiskLevel.EarlyOnset;

                if (declencheurCount >= 4 && declencheurCount <= 6)
                    return RiskLevel.InDanger;
            }

            return RiskLevel.None;
        }
    }
}
