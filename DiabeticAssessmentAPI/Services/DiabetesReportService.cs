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

            int count = contenuNoteDTOs.Count(note => Declencheurs.Liste.Any(d =>
            note.Contenu.Contains(d, StringComparison.OrdinalIgnoreCase)));

            if (count == 0)
                return "None";

            if (age >= 30 && 2 <= count && count <= 5)
                return "Borderline";

            return "À implémenter";
        }
    }
}
