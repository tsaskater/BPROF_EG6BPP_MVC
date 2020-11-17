using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Kes_Bolt_Keszlet_Info
    {
        [Key]
        public string Raktar_Id { get; set; }
        [StringLength(100)]
        public string Bolt_Nev { get; set; }
        [Range(0, 300)]
        public int Darab_Szam { get; set; }
        [Range(0, 2000000)]
        public int Ar { get; set; }
        public virtual ICollection<Kes> Kesek { get; set; }
    }
}
