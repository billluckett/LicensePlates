﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LicensePlatesDBFirstEntities : DbContext
    {
        public LicensePlatesDBFirstEntities()
            : base("name=LicensePlatesDBFirstEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameCountry> GameCountries { get; set; }
        public virtual DbSet<GamePlate> GamePlates { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Plate> Plates { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<LicensePlatesDBFirst.Models.Activeness> Activenesses { get; set; }
    }
}
