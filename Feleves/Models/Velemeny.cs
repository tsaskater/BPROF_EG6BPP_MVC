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
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Kes Kes_Termek { get; set; }
        public string Gyartasi_Cikkszam { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Velemeny)
            {
                Velemeny v = obj as Velemeny;
                return this.Velemeny_Id == v.Velemeny_Id &&
                    this.Szerzo == v.Szerzo &&
                    this.Elegedettseg == v.Elegedettseg &&
                    this.VelemenySzovege == v.VelemenySzovege &&
                    this.Kes_Termek == v.Kes_Termek &&
                    this.Gyartasi_Cikkszam == v.Gyartasi_Cikkszam;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
