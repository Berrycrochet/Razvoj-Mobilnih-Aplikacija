using SQLite;
using SHM_ver1.Models;
using System.IO;

namespace SHM_ver1.Services
{
    public class DatabaseService
    {
        private readonly SQLiteConnection _db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "shm.db");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<UserModel>();
        }

        public void AddUser(UserModel user)
        {
            _db.Insert(user);
        }

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
    }
}
