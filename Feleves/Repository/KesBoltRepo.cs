using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class KesBoltRepo : IRepository<Kes_Bolt_Keszlet_Info>
    {
        VelemenyContextDb db = new VelemenyContextDb();

        public void Add(Kes_Bolt_Keszlet_Info kesbolt)
        {
            db.Kes_Bolt.Add(kesbolt);
            db.SaveChanges();
        }

        public void Delete(string kbId)
        {
            Delete(Read(kbId));
        }

        public void Delete(Kes_Bolt_Keszlet_Info kb)
        {
            db.Kes_Bolt.Remove(kb);
            db.SaveChanges();
        }

        public Kes_Bolt_Keszlet_Info Read(string Kbid)
        {
            return db.Kes_Bolt.FirstOrDefault(tmp => tmp.Raktar_Id == Kbid);
        }

        public IQueryable<Kes_Bolt_Keszlet_Info> Read()
        {
            return db.Kes_Bolt.AsQueryable();
        }

        public void SaveAll()
        {
            db.SaveChanges();
        }

        public void Update(string o_KbId, Kes_Bolt_Keszlet_Info N_kb)
        {
            var Old_Kb = Read(o_KbId);

            Old_Kb.Bolt_Nev = N_kb.Bolt_Nev;
            Old_Kb.Cim = N_kb.Cim;
            Old_Kb.Weboldal = N_kb.Weboldal;
            SaveAll();
        }
    }

}
