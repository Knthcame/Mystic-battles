using Models.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PVPService.Services
{
    public class TrainersRepository
    {
        private Database _database = new Database();

        public TrainerModel GetTrainer(string username) 
            => _database.GetTrainers().FirstOrDefault(trainer => trainer.Username == username);

        public List<TrainerModel> GetTrainers() 
            => _database.GetTrainers();

        public void AddTrainer(TrainerModel trainer)
            => _database.AddTrainer(trainer);
    }
}
