using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> CheckUsernameRegisteredAsync(string username);

        Task<string> LogInAsync(AccountModel account);

        Task<string> SignInAsync(string name, string email, string username, string password);

        void LogOut();

        Task<ParticipantModel> CreateParticipantAsync(string username, bool isAdmin);

        TrainerModel GetTrainer(string username);

        IList<TrainerModel> GetRegisteredTrainers();
    }
}
