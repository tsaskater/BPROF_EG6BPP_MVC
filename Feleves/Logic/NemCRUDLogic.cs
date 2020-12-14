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
        public NemCRUDLogic(IRepository<Kes_Bolt> KesBoltRepo, IRepository<Kes> KesRepo)
        {
            this.KesBoltRepo = KesBoltRepo;
            this.KesRepo = KesRepo;
        }


        //Listázzuk ki a magyarországon hordható késeket és boltjaikat (penge hossz 80 miliméter alatt)
        public IQueryable<Legalis> Legalis(List<Kes_Bolt> kesboltlista,List<Kes> keslista)
        {
            /*List<Kes_Bolt> kesboltok = KesBoltRepo.Read().ToList();
            List<Kes> kesek = KesRepo.Read().ToList();*/
            var q1 =
                (from x in kesboltlista
                 join y in keslista on x.Raktar_Id equals y.Raktar_Id
                 where y.Penge_Hossz <= 80
                 select new Legalis { Termek = y, Bolt = x });
            return q1.AsQueryable();
        }
        //Válasszuk ki azt a kést amivel a legelégedettebbek voltak a vásárlók
        public Kes LegjobbanErtekelt(List<Kes> keslista,List<Velemeny> velemenylista)
        {
            var q2 =
                (from x in keslista
                 join y in velemenylista on x.Gyartasi_Cikkszam equals y.Gyartasi_Cikkszam
                 orderby y.Elegedettseg descending
                 select x).FirstOrDefault();
            return q2;
        }

        //Listázzuk ki az összes olyan boltot ahol tartanak CPM-S30V acéltípusú kést
        public IQueryable<Kes_Bolt> Boltokcpms30(List<Kes_Bolt> kesboltlista,List<Kes> keslista)
        {
            var q3 =
                (from x in kesboltlista
                 join y in keslista on x.Raktar_Id equals y.Raktar_Id
                 where y.Acel== "CPM-S30V"
                 select x).Distinct();
            return q3.AsQueryable();
        }

    }
}
