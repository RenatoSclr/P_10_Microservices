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

        [Fact]
        public async Task Should_return_Borderline_When_ContenuNotePatientDTO_Contains_Between_Two_and_Five_Triggers_and_AgePatient_greater_than_thirty()
        {
            var expected = "Borderline";

            //Arrange
            var diabeteReportService = new DiabetesReportService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(1945, 05, 24),
                Genre = "Homme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale" }
            };


            //Act
            var result = diabeteReportService.GetDiabeteReportByPatientId(infoPatientDTO, contenuNoteDTOs);


            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }
    }
}
