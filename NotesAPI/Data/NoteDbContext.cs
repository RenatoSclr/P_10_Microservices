using MongoDB.Driver;
using NotesAPI.Domain;

namespace NotesAPI.Data
{
    public class NoteDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public NoteDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration.GetConnectionString("DatabaseName");

            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Note> Note =>
            _mongoDatabase.GetCollection<Note>("NoteList");
    }
}
