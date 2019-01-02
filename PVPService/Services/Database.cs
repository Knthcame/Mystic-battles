using Models.Classes;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PVPService.Services
{
    public class Database
    {
        private readonly SQLiteConnection database;

        public Database()
        {
            database = InitializeDatabase();
        }
        private SQLiteConnection InitializeDatabase()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(path);
            db.CreateTable<LeaderboardModel>();
            db.CreateTable<AccountModel>();
            db.CreateTable<TrainerModel>();
            return db;
        }

        public List<LeaderboardModel> GetLeaderboards()
            => DeblobLeaderboards(database.Table<LeaderboardModel>().ToList());

        private List<LeaderboardModel> DeblobLeaderboards(List<LeaderboardModel> list)
        {
            foreach(LeaderboardModel leaderboard in list)
            {
                leaderboard.Participants = DeblobLeaderboard(leaderboard).Participants;
            }
            return list;
        }

        public List<AccountModel> GetAccounts()
            => database.Table<AccountModel>().ToList();

        public List<TrainerModel> GetTrainers()
            => database.Table<TrainerModel>().ToList();

        public bool AddLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard = BlobLeaderboard(leaderboard);
            return AddObject(leaderboard);
        }

        public bool AddAccount(AccountModel account)
            => AddObject(account);

        public bool AddTrainer(TrainerModel trainer)
            => AddObject(trainer);

        public bool UpdateLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard = BlobLeaderboard(leaderboard);
            return UpdateObject(leaderboard);
        }

        public bool UpdateTrainer(TrainerModel trainer)
            => UpdateObject(trainer);

        public bool UpdateAccount(AccountModel account)
            => UpdateObject(account);

        private bool AddObject(object obj)
            => database.Insert(obj) > 0;

        private bool UpdateObject(object obj)
            => database.Update(obj) > 0;

        private LeaderboardModel BlobLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard.ParticipantsBlobbed = JsonConvert.SerializeObject(leaderboard.Participants);
            return leaderboard;
        }

        private LeaderboardModel DeblobLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard.Participants = JsonConvert.DeserializeObject<ObservableCollection<ParticipantModel>>(leaderboard.ParticipantsBlobbed);
            return leaderboard;
        }
    }
}
