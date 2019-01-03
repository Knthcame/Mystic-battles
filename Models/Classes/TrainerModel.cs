using SQLite;

namespace Models.Classes
{
    [Table(nameof(TrainerModel))]
    public class TrainerModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }

        public int Level { get; set; }

        public TrainerModel() { }

        public TrainerModel(string username, int level)
        {
            Username = username;
            Level = level;
        }
    }
}
