//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LicensePlatesDBFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GameCountry
    {
        public GameCountry()
        {
        }

        public GameCountry(int gameId, int countryId)
        {
            GameId = gameId;
            CountryId = countryId;
        }

        public int Id { get; set; }
        public Nullable<int> GameId { get; set; }
        public Nullable<int> CountryId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Game Game { get; set; }
    }
}
