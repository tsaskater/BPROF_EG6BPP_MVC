using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class KesLogic
    {
        IRepository<Kes> KesRepo;

        public KesLogic(IRepository<Kes> KesRepo)
        {
            this.KesRepo = KesRepo;
        }


        public void AddKes(Kes kes)
        {
            this.KesRepo.Add(kes);

        }

        public void DeleteKes(string kId)
        {
            this.KesRepo.Delete(kId);
        }

        public IQueryable<Kes> GetAllKes()
        {
            return KesRepo.Read();
        }


        public Kes GetKes(string kId)
        {
            return KesRepo.Read(kId);
        }


        public void UpdateKes(string o_kid, Kes n_kes)
        {
            KesRepo.Update(o_kid, n_kes);
        }
        public IQueryable<Velemeny> GetVelemenyek(string gyartasiCikkszam)
        {
            return KesRepo.Read(gyartasiCikkszam).Velemenyek.AsQueryable();
        }

        public IQueryable<Kes> GetAllKesForKesbolt(string v_id)
        {
            return KesRepo.Read().Where(x => x.Raktar_Id == v_id);
        }

    }
}
