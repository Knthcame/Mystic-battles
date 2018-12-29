using Models.Classes;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IAccountManager
    {
        bool CheckUsernameRegistered(string username);

        Task<string> LogInAsync(string username, string password);

        Task<string> SignInAsync(string name, string email, string username, string password);

        void LogOut();

        ParticipantModel CreateParticipant(string id);

        TrainerModel GetTrainer(string id);
    }
}
