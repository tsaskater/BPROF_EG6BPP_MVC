using Logic;
using Models;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Teszteles
{
    [TestFixture]
    public class Teszteles
    {
        //Crud Tesztek: Összesre,egyre,add,update,delete
        [Test]
        public void GetAllVelemeny()
        {
            Mock<IRepository<Velemeny>> MockoltRepo = new Mock<IRepository<Velemeny>>(MockBehavior.Loose);
            List<Velemeny> VelemenyTesztLista = new List<Velemeny>()
            {
                new Velemeny(){ Szerzo = "Nick Shabazz" ,Elegedettseg = 8},
                new Velemeny(){ Szerzo = "Nagy Gábor" ,Elegedettseg = 2},
                new Velemeny(){ Szerzo = "Kss Ferenc" ,Elegedettseg = 3}
            };
            List<Velemeny> ElvartKimenet= new List<Velemeny>()
            {
                VelemenyTesztLista[0],
                VelemenyTesztLista[1],
                VelemenyTesztLista[2]
            };
            MockoltRepo.Setup(x => x.Read()).Returns(VelemenyTesztLista.AsQueryable());
            VelemenyLogic velemenyLogic = new VelemenyLogic(MockoltRepo.Object);
            var Kimenet = velemenyLogic.GetAllVelemeny();

            Assert.That(Kimenet, Is.EquivalentTo(ElvartKimenet));
            Assert.That(Kimenet.Count, Is.EqualTo(ElvartKimenet.Count));
        }
        [Test]
        public void GetKesBolt()
        {
            Mock<IRepository<Kes_Bolt>> MockoltKesBoltRepo = new Mock<IRepository<Kes_Bolt>>(MockBehavior.Loose);
            Kes_Bolt TesztKesBolt = new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Extrametál Kés (üzlethálózat)",
                Cim = "Extrametál,Magyaország",
                Weboldal = "https://extrametal.hu/"
            };
            Kes_Bolt ElvartKesBolt = TesztKesBolt;
            MockoltKesBoltRepo.Setup(x=>x.Read(TesztKesBolt.Raktar_Id)).Returns(TesztKesBolt);
            KesBoltLogic kesBoltLogic = new KesBoltLogic(MockoltKesBoltRepo.Object);
            var Kimenet = kesBoltLogic.GetKes_Bolt(TesztKesBolt.Raktar_Id);
            Assert.That(Kimenet, Is.EqualTo(ElvartKesBolt));
        }
        [Test]
        public void AddKes()
        {
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>();
            Kes k = new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490
            };
            MockoltKesRepo.Setup(x => x.Add(It.IsAny<Kes>()));
            KesLogic kesLogic = new KesLogic(MockoltKesRepo.Object);
            kesLogic.AddKes(k);
            MockoltKesRepo.Verify(x => x.Add(k), Times.Once);
        }
        [Test]
        public void UpdateKes()
        {
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>();
            Kes k = new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490
            };
            MockoltKesRepo.Setup(x => x.Update(k.Gyartasi_Cikkszam,It.IsAny<Kes>()));
            KesLogic kesLogic = new KesLogic(MockoltKesRepo.Object);
            kesLogic.UpdateKes(k.Gyartasi_Cikkszam,k);
            MockoltKesRepo.Verify(x => x.Update(k.Gyartasi_Cikkszam, k), Times.Once);
        }
        [Test]
        public void DeleteKes()
        {
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>();
            Kes k = new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490
            };
            MockoltKesRepo.Setup(x => x.Delete(It.IsAny<Kes>()));
            KesLogic kesLogic = new KesLogic(MockoltKesRepo.Object);
            kesLogic.DeleteKes(k.Gyartasi_Cikkszam);
            MockoltKesRepo.Verify(x => x.Delete(k.Gyartasi_Cikkszam), Times.Once);
        }

    }
}
