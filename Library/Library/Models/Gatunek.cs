using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Gatunek")]
    public class Gatunek
    {
        public int GatunekId { get; set; }
        [Index(IsUnique = true)]
        public string Nazwa { get; set; }
    }
}