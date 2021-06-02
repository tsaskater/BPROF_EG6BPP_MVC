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
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Velemeny> Velemenyek { get; set; }
        public string Raktar_Id { get; set; }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Kes_Bolt Kes_Bolt_Keszlet_Info { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Kes)
            {
                Kes k = obj as Kes;
                return this.Gyartasi_Cikkszam == k.Gyartasi_Cikkszam &&
                    this.Gyarto == k.Gyarto &&
                    this.Modell_nev == k.Modell_nev &&
                    this.Markolat == k.Markolat &&
                    this.Bevont_Penge == k.Bevont_Penge &&
                    this.Acel == k.Acel &&
                    this.Ar == k.Ar &&
                    this.Velemenyek == k.Velemenyek &&
                    this.Raktar_Id == k.Raktar_Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
