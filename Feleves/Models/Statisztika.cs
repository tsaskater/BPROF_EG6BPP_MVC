using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public abstract class Statisztika 
    {
        public class Legalis
        {
            public Kes_Bolt Bolt { get; set;}
            public Kes Termek { get; set; }
            public override bool Equals(object obj)
            {
                if (obj is Legalis)
                {
                    Legalis l = obj as Legalis;
                    return this.Bolt == l.Bolt &&
                        this.Termek == l.Termek;
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
        //public Legalis LegalisanHordhatoKes { get; set; }
        /*-----*/


    }
}
