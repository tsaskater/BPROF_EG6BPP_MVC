using Models;
using Repository;
using System;
using System.Linq;

namespace Logic
{
    public class VelemenyLogic
    {
        IRepository<Velemeny> VelemenyRepo;

        public VelemenyLogic(IRepository<Velemeny> VelemenyRepo)
        {
            this.VelemenyRepo = VelemenyRepo;
        }


        public void AddVelemeny(Velemeny velemeny)
        {
            this.VelemenyRepo.Add(velemeny);

        }

        public void DeleteVelemeny(string vId)
        {
            this.VelemenyRepo.Delete(vId);
        }

        public IQueryable<Velemeny> GetAllPlayers()
        {
            return VelemenyRepo.Read();
        }


        public Velemeny GetPlayer(string vId)
        {
            return VelemenyRepo.Read(vId);
        }


        public void UpdatePlayer(string o_vid, Velemeny n_review)
        {
            VelemenyRepo.Update(o_vid, n_review);
        }

    }
}
