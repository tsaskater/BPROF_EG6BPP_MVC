using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Velemeny
    {
        [Key]
        public string Velemeny_Id { get; set; }
        [StringLength(30)]
        public string Szerzo { get; set; }
        [Range(1, 10)]
        public int Elegedettseg { get; set; }
        [StringLength(1024)]
        public string VelemenySzovege { get; set; }
        [NotMapped]
        public virtual Kes Kes_Termek { get; set; }
        public string Gyartasi_Cikkszam { get; set; }
    }
}
