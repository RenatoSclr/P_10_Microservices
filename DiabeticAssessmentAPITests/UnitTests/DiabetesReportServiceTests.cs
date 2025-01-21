using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services;
using Moq;
using Xunit;

namespace DiabeticAssessmentAPITests.UnitTests
{
    public class DiabetesReportServiceTests
    {
        [Fact]
        public async Task Should_return_None_When_ContenuNotePatientDTO_Contains_Max_One_Triggers()
        {
            var expected = "None";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO();

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            { 
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé" },
                new ContenuNotePatientDTO { Contenu = "NO PROBLEMO" }
            };

            //Act
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_Borderline_When_ContenuNotePatientDTO_Contains_Between_Two_and_Five_Triggers_and_AgePatient_greater_than_thirty()
        {
            var expected = "Borderline";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
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
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Three_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Homme()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
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
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Four_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Femme()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
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
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_InDanger_When_ContenuNotePatientDTO_Contains_Six_Or_Seven_Triggers_and_AgePatient_Greater_than_thirty()
        {
            var expected = "InDanger";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
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
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_EarlyOnset_When_ContenuNotePatientDTO_Contains_Five_Or_More_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Homme()
        {
            var expected = "EarlyOnset";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(2002, 06, 28),
                Genre = "Homme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé" },
                new ContenuNotePatientDTO { Contenu = "Taille, Poids, Cholestérol, Vertige et Réaction" },
            };

            //Act
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_EarlyOnset_When_ContenuNotePatientDTO_Contains_Seven_Or_More_Triggers_and_AgePatient_Less_than_thirty_And_Genre_is_Femme()
        {
            var expected = "EarlyOnset";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(2002, 06, 28),
                Genre = "Femme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé" },
                new ContenuNotePatientDTO { Contenu = "Taille, Poids, Cholestérol, Vertige, Réaction, fumeur" },
            };

            //Act
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }

        [Fact]
        public async Task Should_return_EarlyOnset_When_ContenuNotePatientDTO_Contains_Eight_Or_More_Triggers_and_AgePatient_Greater_than_thirty()
        {
            var expected = "EarlyOnset";

            //Arrange
            var diabeteReportService = new DiabetesAlgorihmeService();
            InfoPatientDTO infoPatientDTO = new InfoPatientDTO
            {
                DateNaissance = new DateTime(1945, 06, 28),
                Genre = "Femme"
            };

            List<ContenuNotePatientDTO> contenuNoteDTOs = new List<ContenuNotePatientDTO>
            {
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
                new ContenuNotePatientDTO { Contenu = "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé" },
                new ContenuNotePatientDTO { Contenu = "Taille, Poids, Cholestérol, Vertige, Réaction, fumeur, microalbumine" },
            };

            //Act
            var result = diabeteReportService.GetDiabeteRisk(infoPatientDTO, contenuNoteDTOs);

            //Assert
            Assert.Equal(result.ToLower(), expected.ToLower());
        }
    }
}
