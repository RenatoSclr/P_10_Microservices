using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services;
using Moq;
using Xunit;

namespace DiabeticAssessmentAPITests.UnitTests
{
    public class DiabetesReportServiceTests
    {
        [Fact]
        public async Task Should_return_None_When_ContenuNotePatientDTO_Contains_No_Triggers()
        {
            var expected = "None";

            //Arrange
            var diabeteReportService = new DiabetesReportService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO();

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            { 
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé" },
                new ContenuNotePatientDTO { Contenu = "NO PROBLEMO" }
            };


            //Act
            var result = diabeteReportService.GetDiabeteReportByPatientId(infoPatientDTO, contenuNoteDTOs);


            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }
    }
}
