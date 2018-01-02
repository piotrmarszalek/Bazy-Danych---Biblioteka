using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    [Table("Ksiazka")]
    public class Ksiazka
    {
        public Ksiazka()
        {
            this.Autorzy = new HashSet<Autor>();
        }
        public int KsiazkaId { get; set; }
        [Index(IsUnique = true)]
        [Required]
        public string Nazwa { get; set; }
        public virtual ICollection <Autor> Autorzy { get; set; }
        [Required]
        public int WydawnictwoId { get; set; }
        [Required]
        public virtual Wydawnictwo Wydawnictwo { get; set; }
        [Required]
        public int GatunekId { get; set; }
        [Required]
        public virtual Gatunek Gatunek { get; set; }
    }
}