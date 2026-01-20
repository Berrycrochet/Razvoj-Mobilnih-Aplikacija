using Side_Hustle_Manager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Side_Hustle_Manager.Services

    {
    // ---------------------- Moja baza (korisnici) ----------------------
    public class UserDatabaseService
    {
        private readonly SQLiteConnection _db;

        public UserDatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "shm.db");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<UserModel>();
        }

        public void AddUser(UserModel user) => _db.Insert(user);

        public UserModel GetUser(string username, string password)
        {
            return _db.Table<UserModel>()
                      .FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool UserExists(string username)
        {
            return _db.Table<UserModel>()
                      .Any(u => u.Username == username);
        }
        public async Task<UserModel> GetUserAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                return _db.Table<UserModel>()
                          .FirstOrDefault(u => u.Username == username && u.Password == password);
            });
        }
    }

    // ---------------------- Prijateljičina baza (SideHustle & Applications) ----------------------
    public class SideHustleDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public SideHustleDatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SideHustleModel>().Wait();
            _database.CreateTableAsync<JobApplicationModel>().Wait();
        }

        // ---------------- SIDE HUSTLES ----------------
        public Task<List<SideHustleModel>> GetSideHustlesAsync() => _database.Table<SideHustleModel>().ToListAsync();

        public Task<int> SaveSideHustleAsync(SideHustleModel sideHustle) =>
            sideHustle.Id != 0 ? _database.UpdateAsync(sideHustle) : _database.InsertAsync(sideHustle);

        public Task<int> DeleteSideHustleAsync(SideHustleModel sideHustle) => _database.DeleteAsync(sideHustle);

        // ---------------- APPLICATIONS ----------------
        public Task<int> ApplyToJobAsync(JobApplicationModel application)
        {
            application.AppliedAt = DateTime.Now;
            application.Status = "Pending";
            return _database.InsertAsync(application);
        }

        public Task<List<JobApplicationModel>> GetApplicationsForJobAsync(int sideHustleId) =>
            _database.Table<JobApplicationModel>().Where(a => a.SideHustleId == sideHustleId).ToListAsync();

        public Task<List<JobApplicationModel>> GetApplicationsForUserAsync(string userId) =>
            _database.Table<JobApplicationModel>().Where(a => a.UserId == userId).ToListAsync();

        public Task<int> SaveApplicationAsync(JobApplicationModel application) => _database.UpdateAsync(application);

        public async Task<List<UserSideHustleViewModel>> GetUserSideHustlesAsync(string userId)
        {
            var applications = await _database.Table<JobApplicationModel>()
                                              .Where(a => a.UserId == userId)
                                              .ToListAsync();

            var result = new List<UserSideHustleViewModel>();

            foreach (var app in applications)
            {
                var job = await _database.Table<SideHustleModel>()
                                         .Where(j => j.Id == app.SideHustleId)
                                         .FirstOrDefaultAsync();
                if (job == null) continue;

                result.Add(new UserSideHustleViewModel
                {
                    ApplicationId = app.Id,
                    Title = job.Title,
                    Description = job.Description,
                    Category = job.Category,
                    Pay = job.Pay,
                    Status = app.Status,
                    AppliedAt = app.AppliedAt
                });
            }

            return result;
        }
    }
}


