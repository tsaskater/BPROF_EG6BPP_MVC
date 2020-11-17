using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class KesRepo : IRepository<Kes>
    {
        VelemenyContextDb db = new VelemenyContextDb();

        public void Add(Kes kes)
        {
            db.Kesek.Add(kes);
            db.SaveChanges();
        }

        public void Delete(string kId)
        {
            Delete(Read(kId));
        }

        public void Delete(Kes kes)
        {
            db.Kesek.Remove(kes);
            db.SaveChanges();
        }

        public Kes Read(string Kid)
        {
            return db.Kesek.FirstOrDefault(tmp => tmp.Gyartasi_Cikkszam == Kid);
        }

        public IQueryable<Kes> Read()
        {
            return db.Kesek.AsQueryable();
        }

        public void SaveAll()
        {
            db.SaveChanges();
        }

        public void Update(string o_KId, Kes N_kes)
        {
            var Old_Kes = Read(o_KId);

            Old_Kes.Gyarto = N_kes.Gyarto;
            Old_Kes.Modell_nev = N_kes.Modell_nev;
            Old_Kes.Markolat = N_kes.Markolat;
            Old_Kes.Bevont_Penge= N_kes.Bevont_Penge;
            Old_Kes.Penge_Hossz= N_kes.Penge_Hossz;
            Old_Kes.Acel = N_kes.Acel;
            Old_Kes.Ar = N_kes.Ar;
            SaveAll();
        }
    }
}
