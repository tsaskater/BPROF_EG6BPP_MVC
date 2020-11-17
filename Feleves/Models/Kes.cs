using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Kes
    {
        [Key]
        public string Gyartasi_Cikkszam { get; set; }
        [StringLength(50)]
        public string Gyarto { get; set; }
        [StringLength(50)]
        public string Modell_nev { get; set; }
        [StringLength(50)]
        public string Markolat { get; set; }
        public bool Bevont_Penge { get; set; }
        [Range(0, 500)]
        public int Penge_Hossz { get; set; }
        [StringLength(15)]
        public string Acel { get; set; }
        [Range(0, 2000000)]
        public int Ar{ get; set; }
        public virtual ICollection<Velemeny> Velemenyek { get; set; }
        public string Raktar_Id { get; set; }

        [NotMapped]
        public virtual Kes_Bolt_Keszlet_Info Kes_Bolt_Keszlet_Info { get; set; }
    }
}
