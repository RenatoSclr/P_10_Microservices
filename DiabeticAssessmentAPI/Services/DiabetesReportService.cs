using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;

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

            if (count == 0)
                return "None";

            if (age >= 30 && 2 <= count && count <= 5)
                return "Borderline";

            if (age < 30 && count == 3 && infoPatient.Genre == "Homme")
                return "InDanger";
            return "À implémenter";
        }
    }
}
