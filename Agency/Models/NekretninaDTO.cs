using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agency.Models
{
    public class NekretninaDTO
    {
        public int Id { get; set; }
        public string Mesto { get; set; }
        public string AgencijskaOznaka { get; set; }
        public int GodinaIzgradnje { get; set; }
        public decimal Kvadratura { get; set; }
        public decimal Cena { get; set; }
        public string AgentImeiPrezime { get; set; }
    }
}