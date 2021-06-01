using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Kes> Kesek { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Kes_Bolt)
            {
                Kes_Bolt kb = obj as Kes_Bolt;
                return this.Raktar_Id == kb.Raktar_Id &&
                    this.Bolt_Nev == kb.Bolt_Nev &&
                    this.Cim == kb.Cim &&
                    this.Weboldal == kb.Weboldal &&
                    this.Kesek == kb.Kesek;
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
