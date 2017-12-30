using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Nazwa { get; set; }
        public virtual ICollection <Autor> Autorzy { get; set; }
        public int WydawnictwoId { get; set; }
        public virtual Wydawnictwo Wydawnictwo { get; set; }
        public int GatunekId { get; set; }
        public virtual Gatunek Gatunek { get; set; }
    }
}