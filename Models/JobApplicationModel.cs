using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SHM_ver1.Models
{
    public class JobApplicationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int SideHustleId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int UserId { get; set; }   // za sada fake (npr. 1)

        public string Status { get; set; } = "Pending";
        // Pending | Accepted | Rejected
    }
}

