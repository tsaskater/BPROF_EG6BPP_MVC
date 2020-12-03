using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class KesBoltLogic
    {
        IRepository<Kes_Bolt> KesBoltRepo;

        public KesBoltLogic(IRepository<Kes_Bolt> KesBoltRepo)
        {
            this.KesBoltRepo = KesBoltRepo;
        }


        public void AddKesBolt(Kes_Bolt kesBolt)
        {
            this.KesBoltRepo.Add(kesBolt);

        }

        public void DeleteKesBolt(string kbId)
        {
            this.KesBoltRepo.Delete(kbId);
        }

        public IQueryable<Kes_Bolt> GetAllKes_Bolt()
        {
            return KesBoltRepo.Read();
        }


        public Kes_Bolt GetKes_Bolt(string kbId)
        {
            return KesBoltRepo.Read(kbId);
        }


        public void UpdateKes_Bolt(string o_kbid, Kes_Bolt n_kesbolt)
        {
            KesBoltRepo.Update(o_kbid, n_kesbolt);
        }

    }
}
