using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    [Table("Autor")]
    public class Autor
    {
        public Autor()
        {
            this.Ksiazki = new HashSet<Ksiazka>();
        }
        public int AutorId { get; set; }
        [Index("ImieNazwisko", 1, IsUnique = true)]
        [Required]
        public string Imie { get; set; }
        [Index("ImieNazwisko", 2, IsUnique = true)]
        [Required]
        public string Nazwisko { get; set; }
        public virtual ICollection <Ksiazka> Ksiazki { get; set; }
    }
}