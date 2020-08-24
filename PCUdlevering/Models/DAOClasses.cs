using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCUdlevering.Models.DAOClasses
{
    public class DAOComputer
    {
        public string PcID { get; set; }
        public DateTime? LendDate { get; set; } = null;
        public bool IsLent { get; set; } = false;
        public bool IsHandedIn { get; set; } = false;
        public DateTime? HandInDate { get; set; } = null;

        public DAOComputer(string pcID, DateTime? lendDate, bool isLent, bool isHandedIn, DateTime? handInDate)
        {
            this.PcID = pcID;
            this.LendDate = lendDate;
            this.IsLent = isLent;
            this.IsHandedIn = isHandedIn;
            this.HandInDate = handInDate;
        }
    }
    
}