using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Miasto")]
    public class Miasto
    {
        public int MiastoId { get; set; }
        [Index(IsUnique = true)]
        public string Nazwa { get; set; }
    }
}