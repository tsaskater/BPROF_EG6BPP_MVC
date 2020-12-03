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

        public IQueryable<Kes_Bolt> GetAllPlayers()
        {
            return KesBoltRepo.Read();
        }


        public Kes_Bolt GetPlayer(string kbId)
        {
            return KesBoltRepo.Read(kbId);
        }


        public void UpdatePlayer(string o_kbid, Kes_Bolt n_kesbolt)
        {
            KesBoltRepo.Update(o_kbid, n_kesbolt);
        }

    }
}
