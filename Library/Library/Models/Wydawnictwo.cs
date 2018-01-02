using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Wydawnictwo")]
    public class Wydawnictwo
    {
        public int WydawnictwoId { get; set; }
        [Index(IsUnique = true)]
        [Required]
        public string Nazwa { get; set; }
    }
}