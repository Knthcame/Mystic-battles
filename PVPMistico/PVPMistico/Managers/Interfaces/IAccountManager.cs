using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IAccountManager
    {
        bool CheckUsernameRegistered(string username);

        Task<string> LogInAsync(AccountModel account);

        Task<string> SignInAsync(string name, string email, string username, string password);

        void LogOut();

        ParticipantModel CreateParticipant(string username);

        TrainerModel GetTrainer(string username);

        IList<TrainerModel> GetRegisteredTrainers();
    }
}
