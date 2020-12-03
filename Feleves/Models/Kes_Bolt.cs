using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Kes_Bolt
    {
        [Key]
        public string Raktar_Id { get; set; }
        [StringLength(100)]
        public string Bolt_Nev { get; set; }
        [StringLength(100)]
        public string Cim { get; set; }
        [StringLength(100)]
        public string Weboldal { get; set; }
        public virtual ICollection<Kes> Kesek { get; set; }
    }
}
