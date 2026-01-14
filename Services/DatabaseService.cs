using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SHM_ver1.Models;


namespace SHM_ver1.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<SideHustleModel>().Wait();
            _database.CreateTableAsync<JobApplicationModel>().Wait();
        }

        public Task<List<SideHustleModel>> GetSideHustlesAsync() {
            return _database.Table<SideHustleModel>().ToListAsync();


        }

        public Task<int> AddSideHustleAsync(SideHustleModel sideHustle)
        {
            return _database.InsertAsync(sideHustle);
        }

        public Task<int> DeleteSideHustleAsync(SideHustleModel sideHustle)
        {
            return _database.DeleteAsync(sideHustle);
        }

        public Task<int> SaveSideHustleAsync(SideHustleModel sideHustle)
        {
            if (sideHustle.Id != 0)
                return _database.UpdateAsync(sideHustle);
            else
                return _database.InsertAsync(sideHustle);
        }

        public Task<int> ApplyToJobAsync(JobApplicationModel application)
        {
            return _database.InsertAsync(application);
        }

        public Task<List<JobApplicationModel>> GetApplicationsForJobAsync(int jobId)
        {
            return _database.Table<JobApplicationModel>().
                Where(a => a.SideHustleId == jobId)
                .ToListAsync();
        }

        public Task<int> UpdateApplicationStatusAsync(JobApplicationModel application, string status)
        {
            application.Status = status;
            return _database.UpdateAsync(application);
        }

        public Task<List<JobApplicationModel>> GetApplicationsAsync()
        {
            return _database.Table<JobApplicationModel>().ToListAsync();
        }

        public Task<int> SaveApplicationAsync(JobApplicationModel app)
        {
            if (app.Id != 0)
                return _database.UpdateAsync(app);
            else
                return _database.InsertAsync(app);
        }



    }
}
