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

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Three_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Homme()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesReportService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(2004, 06, 18),
                Genre = "Homme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il fume depuis peu" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière " +
                "Il se plaint également de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé" }
            };


            //Act
            var result = diabeteReportService.GetDiabeteReportByPatientId(infoPatientDTO, contenuNoteDTOs);


            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Four_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Femme()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesReportService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(2004, 06, 18),
                Genre = "Femme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il fume depuis peu" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière " +
                "Il se plaint également de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé, Vertiges" }
            };


            //Act
            var result = diabeteReportService.GetDiabeteReportByPatientId(infoPatientDTO, contenuNoteDTOs);


            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Six_Or_Seven_Triggers_and_AgePatient_Greater_than_thirty()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesReportService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(1945, 06, 18),
                Genre = "Femme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il fume depuis peu" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière " +
                "Il se plaint également de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé, Vertiges, Anticorps, microalbumine" }
            };


            //Act
            var result = diabeteReportService.GetDiabeteReportByPatientId(infoPatientDTO, contenuNoteDTOs);


            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }
    }
}
