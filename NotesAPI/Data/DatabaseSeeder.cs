using MongoDB.Driver;
using NotesAPI.Domain;

namespace NotesAPI.Data
{
    public class DatabaseSeeder
    {
        private readonly NoteDbContext _context;

        public DatabaseSeeder(NoteDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if ((await _context.Note.CountDocumentsAsync(FilterDefinition<Note>.Empty)) == 0)
            {
                var notes = new List<Note>
                {
                    new Note
                    {
                        PatientId = new Guid("ccc9e063-c800-43d5-924a-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("157d6514-89de-431b-924b-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("157d6514-89de-431b-924b-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("415b6bbb-bd43-4d04-924c-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il fume depuis peu",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("415b6bbb-bd43-4d04-924c-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière Il se plaint également " +
                        "de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également " +
                        "d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"),
                        Contenu = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"),
                        Contenu = " Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé",
                        DateCreatiom = DateTime.UtcNow
                    },
                    new Note
                    {
                        PatientId = new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"),
                        Contenu = "Taille, Poids, Cholestérol, Vertige et Réaction",
                        DateCreatiom = DateTime.UtcNow
                    }
                };

                await _context.Note.InsertManyAsync(notes);
                Console.WriteLine("Notes seeded successfully.");
            }
            else
            {
                Console.WriteLine("Notes collection already contains data.");
            }
        }
    }
}
