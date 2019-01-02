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
    }
}
