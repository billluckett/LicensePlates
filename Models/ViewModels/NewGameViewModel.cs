using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicensePlatesDBFirst.Models.ViewModels
{
    public class NewGameViewModel
    {
        public NewGameViewModel()
        {
        }

        public NewGameViewModel(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; set; }
        public List<Activeness> Actives { get; set; }
    }
}