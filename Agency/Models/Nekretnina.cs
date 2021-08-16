using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agency.Models
{
    public class Nekretnina
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Ovo polje je obavezno i moze sadrzati maksimum 40 karaktera!")]
        public string Mesto { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "Ovo polje je obavezno i moze sadrzati maksimum 5 karaktera!")]
        public string AgencijskaOznaka { get; set; }
        [Required]
        [Range(1900, 2018)]
        public int GodinaIzgradnje { get; set; }
        [Required]
        [Range(2.1, double.MaxValue)]
        public decimal Kvadratura { get; set; }
        [Required]
        [Range(0.1, 100000.0)]
        public decimal Cena { get; set; }
        [Required]
        public virtual int AgentId { get; set; }
        public virtual Agent Agent { get; set; }
    }
}
