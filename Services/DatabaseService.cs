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

        // ---------------- SIDE HUSTLES ----------------

        public Task<List<SideHustleModel>> GetSideHustlesAsync()
        {
            return _database.Table<SideHustleModel>().ToListAsync();
        }

        public Task<int> SaveSideHustleAsync(SideHustleModel sideHustle)
        {
            if (sideHustle.Id != 0)
                return _database.UpdateAsync(sideHustle);
            else
                return _database.InsertAsync(sideHustle);
        }

        public Task<int> DeleteSideHustleAsync(SideHustleModel sideHustle)
        {
            return _database.DeleteAsync(sideHustle);
        }

        // ---------------- APPLICATIONS ----------------

        public Task<int> ApplyToJobAsync(JobApplicationModel application)
        {
            application.AppliedAt = DateTime.Now;
            application.Status = "Pending";
            return _database.InsertAsync(application);
        }

        public Task<List<JobApplicationModel>> GetApplicationsForJobAsync(int sideHustleId)
        {
            return _database.Table<JobApplicationModel>()
                .Where(a => a.SideHustleId == sideHustleId)
                .ToListAsync();
        }

        public Task<List<JobApplicationModel>> GetApplicationsForUserAsync(string userId)
        {

            return _database.Table<JobApplicationModel>()
                .Where(a => a.UserId == userId)
                .ToListAsync();

        }

        public Task<int> SaveApplicationAsync(JobApplicationModel application)
        {
            return _database.UpdateAsync(application);
        }

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
