using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agency.Models
{
    public class Agent
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Ovo polje je obavezno i moze sadrzati maksimum 50 karaktera!")]
        public string ImeiPrezime { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Ovo polje je obavezno i mora sadrzati tacno 4 karaktera!")]
        public string Licenca { get; set; }
        [Required]
        [Range(1951, 1995)]
        public int GodinaRodjenja { get; set; }
        [Required]
        [Range(0, 50)]
        public int BrojProdatihNekretnina { get; set; }
    }
}