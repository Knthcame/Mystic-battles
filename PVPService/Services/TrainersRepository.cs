using Models.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PVPService.Services
{
    public static class TrainersRepository
    {
        private static List<TrainerModel> _trainers = new List<TrainerModel>
        {
            new TrainerModel
            {
                Level = 40,
                Username = "Originals"
            },
            new TrainerModel
            {
                Level = 39,
                Username = "No originals"
            }
        };

        public static TrainerModel GetTrainer(string username) 
            => _trainers.FirstOrDefault(trainer => trainer.Username == username);

        public static List<TrainerModel> GetTrainers() 
            => _trainers;

        public static void AddTrainer(TrainerModel trainer)
            => _trainers.Add(trainer);
    }
}
