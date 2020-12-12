using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Models.Statisztika;

namespace Logic
{
    public class NemCRUDLogic
    {
        IRepository<Kes_Bolt> KesBoltRepo;
        IRepository<Kes> KesRepo;
        IRepository<Velemeny> VelemenyRepo;

        public NemCRUDLogic(IRepository<Kes_Bolt> KesBoltRepo, IRepository<Kes> KesRepo, IRepository<Velemeny> VelemenyRepo)
        {
            this.KesBoltRepo = KesBoltRepo;
            this.KesRepo = KesRepo;
            this.VelemenyRepo = VelemenyRepo;
        }
        //Listázzuk ki a magyarországon hordható késeket és boltjaikat (penge hossz 80 miliméter alatt)
        public IQueryable<Legalis> Legalis()
        {
            /*List<Kes_Bolt> kesboltok = KesBoltRepo.Read().ToList();
            List<Kes> kesek = KesRepo.Read().ToList();*/
            var q1 =
                (from x in KesBoltRepo.Read().ToList()
                 join y in KesRepo.Read().ToList() on x.Raktar_Id equals y.Raktar_Id
                 where y.Penge_Hossz <= 80
                 select new Legalis { Termek = y, Bolt = x });
            return q1.AsQueryable();
        }
        //Válasszuk ki azt a kést amivel a legelégedettebbek voltak a vásárlók
        public Kes LegjobbanErtekelt()
        {
            var q2 =
                (from x in KesRepo.Read().ToList()
                 join y in VelemenyRepo.Read().ToList() on x.Gyartasi_Cikkszam equals y.Gyartasi_Cikkszam
                 orderby y.Elegedettseg descending
                 select x).FirstOrDefault();
            return q2;
        }

    }
}
