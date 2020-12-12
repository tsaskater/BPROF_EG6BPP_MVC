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
    }
}
