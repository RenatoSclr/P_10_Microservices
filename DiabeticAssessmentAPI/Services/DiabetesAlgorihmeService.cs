using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabetesAlgorihmeService : IDiabetesAlgorihmeService
    {
        public string GetDiabeteReportByPatientId(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs)
        {
            int age = CalculateAge(infoPatient.DateNaissance);
            int count = CountTriggers(contenuNoteDTOs);

            if (age >= 30)
                return EvaluateRiskForAgeGreaterThanThirty(count);

            return EvaluateRiskForAgeLowerThanThirty(age, infoPatient.Genre, count);
        }

        private int CalculateAge(DateTime dateNaissance)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateNaissance.Year;
            if (dateNaissance > now.AddYears(-age))
                age--;
            return age;
        }

        private int CountTriggers(List<ContenuNotePatientDTO> contenuNoteDTOs)
        {
            return Declencheurs.Liste
                .Where(declencheur => contenuNoteDTOs
                    .Any(note => note.Contenu.Contains(declencheur, StringComparison.OrdinalIgnoreCase)))
                .Distinct()
                .Count();
        }

        private string EvaluateRiskForAgeGreaterThanThirty(int count)
        {
            if (count >= 8) 
                return "EarlyOnset";

            if (count == 6 || count == 7) 
                return "InDanger";

            if (count >= 2 && count <= 5) 
                return "Borderline";

            return "None";
        }

        private string EvaluateRiskForAgeLowerThanThirty(int age, string genre, int declencheurCount)
        {
            bool isMale = genre.Equals("Homme", StringComparison.OrdinalIgnoreCase);
            bool isFemale = genre.Equals("Femme", StringComparison.OrdinalIgnoreCase);

            if (isMale)
            {
                if (declencheurCount >= 5) 
                    return "EarlyOnset";

                if (declencheurCount == 3 || declencheurCount == 4) 
                    return "InDanger";
            }

            if (isFemale)
            {
                if (declencheurCount >= 7)
                    return "EarlyOnset";

                if (declencheurCount >= 4 && declencheurCount <= 6) 
                    return "InDanger";
            }

            return "None";
        }
    }
}
