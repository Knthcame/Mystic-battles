using Models.Classes;
using SQLite;
using System;
using System.Collections.Generic;
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
            => database.Table<LeaderboardModel>().ToList();

        public List<AccountModel> GetAccounts()
            => database.Table<AccountModel>().ToList();

        public List<TrainerModel> GetTrainers()
            => database.Table<TrainerModel>().ToList();

        public bool AddObject(object obj)
            => database.Insert(obj) > 0;

        public bool UpdateObject(object obj)
            => database.Update(obj) > 0;
    }
}
