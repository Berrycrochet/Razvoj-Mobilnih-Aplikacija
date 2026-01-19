using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SHM_ver1.Models
{
    public class UserSideHustleViewModel
    {

        public int ApplicationId { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public Color StatusColor =>
            Status switch
            {
                "Accepted" => Colors.Green,
                "Rejected" => Colors.Red,
                "Pending" => Colors.Goldenrod,
                _ => Colors.Gray
            };
        public DateTime AppliedAt { get; set; }
        public decimal Pay { get; set; }
        public string PayDisplay => $"{Pay} KM";
    }
}
