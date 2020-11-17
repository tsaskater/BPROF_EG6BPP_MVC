using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    class KesBoltLogic
    {
        IRepository<Kes_Bolt_Keszlet_Info> KesBoltRepo;

        public KesBoltLogic(IRepository<Kes_Bolt_Keszlet_Info> KesBoltRepo)
        {
            this.KesBoltRepo = KesBoltRepo;
        }


        public void AddKesBolt(Kes_Bolt_Keszlet_Info kesBolt)
        {
            this.KesBoltRepo.Add(kesBolt);

        }

        public void DeleteKesBolt(string kbId)
        {
            this.KesBoltRepo.Delete(kbId);
        }

        public IQueryable<Kes_Bolt_Keszlet_Info> GetAllPlayers()
        {
            return KesBoltRepo.Read();
        }


        public Kes_Bolt_Keszlet_Info GetPlayer(string kbId)
        {
            return KesBoltRepo.Read(kbId);
        }


        public void UpdatePlayer(string o_kbid, Kes_Bolt_Keszlet_Info n_kesbolt)
        {
            KesBoltRepo.Update(o_kbid, n_kesbolt);
        }

    }
}
