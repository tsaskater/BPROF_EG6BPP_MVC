using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class VelemenyRepo:IRepository<Velemeny>
    {
        VelemenyContextDb db = new VelemenyContextDb();

        public void Add(Velemeny review)
        {
            db.Velemenyek.Add(review);
            db.SaveChanges();
        }

        public void Delete(string VId)
        {
            Delete(Read(VId));
        }

        public void Delete(Velemeny velemeny)
        {
            db.Velemenyek.Remove(velemeny);
            db.SaveChanges();
        }

        public Velemeny Read(string Vid)
        {
            return db.Velemenyek.FirstOrDefault(tmp => tmp.Velemeny_Id == Vid);
        }

        public IQueryable<Velemeny> Read()
        {
            return db.Velemenyek.AsQueryable();
        }

        public void SaveAll()
        {
            db.SaveChanges();
        }

        public void Update(string o_Vid, Velemeny N_review)
        {
            var Old_Velemeny = Read(o_Vid);

            Old_Velemeny.Szerzo = N_review.Szerzo;
            Old_Velemeny.Elegedettseg = N_review.Elegedettseg;

            SaveAll();
        }
    }
}
