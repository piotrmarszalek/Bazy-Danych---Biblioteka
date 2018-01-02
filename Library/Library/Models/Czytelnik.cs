using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Czytelnik")]
    public class Czytelnik
    {
        public int CzytelnikId { get; set; }
        [Index("ImieNazwiskoMiasto", 1, IsUnique = true)]
        [Required]
        public string Imie { get; set; }
        [Index("ImieNazwiskoMiasto", 2, IsUnique = true)]
        [Required]
        public string Nazwisko { get; set; }
        [Index("ImieNazwiskoMiasto", 3, IsUnique = true)]
        [Required]
        public int MiastoId { get; set; }
        public virtual Miasto Miasto { get; set; }
    }
}