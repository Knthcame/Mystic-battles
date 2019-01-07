using Models.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace PVPService.Services
{
    public class Database
    {
        private readonly SQLiteConnection _database;

        private readonly BlobsManager _blobsManager = new BlobsManager();

        public Database()
        {
            _database = InitializeDatabase();
        }
        private SQLiteConnection InitializeDatabase()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            var db = new SQLiteConnection(path);
            db.CreateTable<LeaderboardModel>();
            db.CreateTable<AccountModel>();
            db.CreateTable<TrainerModel>();
            db.CreateTable<MatchModel>();
            return db;
        }

        public List<LeaderboardModel> GetLeaderboards()
            => _database.Table<LeaderboardModel>().ToList();

        public List<AccountModel> GetAccounts()
            => _database.Table<AccountModel>().ToList();

        public List<TrainerModel> GetTrainers()
            => _database.Table<TrainerModel>().ToList();

        public List<MatchModel> GetMatches()
            =>_database.Table<MatchModel>().ToList();

        public bool AddLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard = _blobsManager.BlobLeaderboard(leaderboard);
            return AddObject(leaderboard);
        }

        public bool AddAccount(AccountModel account)
            => AddObject(account);

        public bool AddTrainer(TrainerModel trainer)
            => AddObject(trainer);

        public bool AddMatch(MatchModel match)
        {
            match = _blobsManager.BlobMatch(match);
            return AddObject(match);
        }

        public bool UpdateLeaderboard(LeaderboardModel leaderboard)
        {
            leaderboard = _blobsManager.BlobLeaderboard(leaderboard);
            return UpdateObject(leaderboard);
        }

        public bool UpdateTrainer(TrainerModel trainer)
            => UpdateObject(trainer);

        public bool UpdateAccount(AccountModel account)
            => UpdateObject(account);

        public bool UpdateMatch(MatchModel match)
        {
            match = _blobsManager.BlobMatch(match);
            return UpdateObject(match);
        }

        public bool DeleteTrainer(TrainerModel trainer)
            => DeleteObject(trainer);

        public bool DeleteAccount(AccountModel account)
            => DeleteObject(account);

        public bool DeleteMatch(MatchModel match)
            => DeleteObject(match);

        public bool DeleteLeaderboard(LeaderboardModel leaderboard)
            => DeleteObject(leaderboard);

        private bool AddObject(object obj)
            => _database.Insert(obj) > 0;

        private bool UpdateObject(object obj)
            => _database.Update(obj) > 0;

        private bool DeleteObject(object obj)
            => _database.Delete(obj) > 0;
    }
}
