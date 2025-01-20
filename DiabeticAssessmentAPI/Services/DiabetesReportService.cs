using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabetesReportService 
    {
        public string GetDiabeteReportByPatientId(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs)
        {
            DateTime dateNow = DateTime.Now;
            int age = dateNow.Year - infoPatient.DateNaissance.Year;
            if (infoPatient.DateNaissance > dateNow.AddYears(-age))
                age--;

            // Trouver les déclencheurs uniques dans toutes les notes
            var declencheursTrouves = Declencheurs.Liste
                .Where(d => contenuNoteDTOs.Any(note =>
                    note.Contenu.Contains(d, StringComparison.OrdinalIgnoreCase)))
                .Distinct()
                .ToList();

            int count = declencheursTrouves.Count;


            if (age >= 30 && 2 <= count && count <= 5)
                return "Borderline";

            if (age >= 30 && (count == 6 || count == 7))
                return "InDanger";
            
            if (age < 30 && infoPatient.Genre == "Homme" && (count == 3 || count == 4))
                return "InDanger";

            if (age < 30 && infoPatient.Genre == "Femme" && (count == 4 || count == 5 || count == 6))
                return "InDanger";

            if (age < 30 && count >=5 && infoPatient.Genre == "Homme")
                return "EarlyOnset";

            if (age < 30 && count >= 7 && infoPatient.Genre == "Femme")
                return "EarlyOnset";

            if (age >= 30 && count >= 8)
                return "EarlyOnset";

            return "None";
        }
    }
}
