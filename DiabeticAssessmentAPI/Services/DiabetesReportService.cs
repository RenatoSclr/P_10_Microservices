using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabetesReportService 
    {
        public string GetDiabeteReportByPatientId(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs)
        {
            int count = contenuNoteDTOs.Count(note => Declencheurs.Liste.Any(d =>
            note.Contenu.Contains(d, StringComparison.OrdinalIgnoreCase)));

            if (count == 0)
            {
                return "None";
            }

            return "À implémenter";
        }
    }
}
