using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SHM_ver1.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
