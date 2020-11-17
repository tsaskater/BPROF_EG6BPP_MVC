using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    class KesLogic
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

        public IQueryable<Kes> GetAllPlayers()
        {
            return KesRepo.Read();
        }


        public Kes GetPlayer(string kId)
        {
            return KesRepo.Read(kId);
        }


        public void UpdatePlayer(string o_kid, Kes n_kes)
        {
            KesRepo.Update(o_kid, n_kes);
        }

    }
}
