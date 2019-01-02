using Models.Classes;
using Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> CheckUsernameRegisteredAsync(string username);

        Task<LogInResponseCode> LogInAsync(AccountModel account);

        Task<SignInResponseCode> SignInAsync(AccountModel account);

        void LogOut();

        Task<ParticipantModel> CreateParticipantAsync(string username, bool isAdmin);

        Task<TrainerModel> GetTrainer(string username);

        Task<IList<TrainerModel>> GetRegisteredTrainers();
    }
}
