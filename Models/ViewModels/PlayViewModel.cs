using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicensePlatesDBFirst.Models.ViewModels
{
    public class PlayViewModel
    {
        public PlayViewModel()
        {
        }

        public PlayViewModel(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; set; }
        public ICollection<Plate> Plates { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<Region> Regions { get; set; }
    }
}