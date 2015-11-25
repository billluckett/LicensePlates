using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicensePlatesDBFirst.Models
{
    public class Activeness
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public Activeness()
        {
        }

        public Activeness(Country country)
        {
            CountryId = country.Id;
            Name = country.Name;
            Active = false;
        }
    }
}