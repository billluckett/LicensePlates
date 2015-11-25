using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicensePlatesDBFirst.Models.ViewModels
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Possible { get; set; }

        public HistoryViewModel() { }
    }
}