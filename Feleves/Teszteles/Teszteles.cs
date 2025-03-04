﻿using Logic;
using Models;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static Models.Statisztika;

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
            List<Velemeny> ElvartKimenet = new List<Velemeny>()
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
                Cim = "Extrametál,Magyarország",
                Weboldal = "https://extrametal.hu/"
            };
            Kes_Bolt ElvartKesBolt = TesztKesBolt;
            MockoltKesBoltRepo.Setup(x => x.Read(TesztKesBolt.Raktar_Id)).Returns(TesztKesBolt);
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
            MockoltKesRepo.Setup(x => x.Update(k.Gyartasi_Cikkszam, It.IsAny<Kes>()));
            KesLogic kesLogic = new KesLogic(MockoltKesRepo.Object);
            kesLogic.UpdateKes(k.Gyartasi_Cikkszam, k);
            MockoltKesRepo.Verify(x => x.Update(k.Gyartasi_Cikkszam, k), Times.Once);
        }
        [Test]
        public void DeleteKes()
        {
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>(MockBehavior.Loose);
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
        [Test]
        public void Legalis()
        {
            Mock<IRepository<Kes_Bolt>> MockoltKesBoltRepo = new Mock<IRepository<Kes_Bolt>>(MockBehavior.Loose);
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>(MockBehavior.Loose);
            List<Kes_Bolt> KesBoltLista = new List<Kes_Bolt>();
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Hq",
                Cim = "564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                Weboldal = "https://www.bladehq.com/"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Extrametál Kés (üzlethálózat)",
                Cim = "Extrametál,Magyarország",
                Weboldal = "https://extrametal.hu/"
            });
            /*KESEK:-----------*/
            List<Kes> KesLista = new List<Kes>();
            /*ELSO BOLTBA-----------*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490,
                Raktar_Id = KesBoltLista[0].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Anthem 781",
                Markolat = "Titanium",
                Bevont_Penge = false,
                Penge_Hossz = 89,
                Acel = "CPM-S30V",
                Ar = 183690,
                Raktar_Id = KesBoltLista[0].Raktar_Id
            });
            /*Masodik BOLTBA-----------*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490,
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Anthem 781",
                Markolat = "Titanium",
                Bevont_Penge = false,
                Penge_Hossz = 89,
                Acel = "CPM-S30V",
                Ar = 183690,
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });


            List<Legalis> ElvartLegalisLista = new List<Legalis>();
            ElvartLegalisLista.Add(new Statisztika.Legalis()
            {
                Bolt = KesBoltLista[0],
                Termek = KesLista[0]
            });
            ElvartLegalisLista.Add(new Statisztika.Legalis()
            {
                Bolt = KesBoltLista[1],
                Termek = KesLista[2]
            });

            /*---------*/
            MockoltKesBoltRepo.Setup(x => x.Read()).Returns(KesBoltLista.AsQueryable());
            MockoltKesRepo.Setup(x => x.Read()).Returns(KesLista.AsQueryable());
            NemCRUDLogic nemCRUDLogic = new NemCRUDLogic(MockoltKesBoltRepo.Object, MockoltKesRepo.Object);
            var kimenet = nemCRUDLogic.Legalis(KesBoltLista, KesLista);
            Assert.That(kimenet, Is.EquivalentTo(ElvartLegalisLista));
            Assert.That(kimenet.Count, Is.EqualTo(ElvartLegalisLista.Count));
        }
        [Test]
        public void LegjobbanErtekelt()
        {
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>(MockBehavior.Loose);
            Mock<IRepository<Velemeny>> MockoltVelemenyRepo = new Mock<IRepository<Velemeny>>(MockBehavior.Loose);
            List<Kes> KesLista = new List<Kes>();
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Chaparral",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 71,
                Acel = "CTS-XHP",
                Ar = 40790,
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490,
            });
            List<Velemeny> VelemenyLista = new List<Velemeny>();
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Nick Shabazz",
                Elegedettseg = 8,
                VelemenySzovege = "I'd reccomend this product" +
                "You can check out my review here:" +
                "https://www.youtube.com/watch?v=N-0ERB3tBOU",
                Gyartasi_Cikkszam = KesLista[1].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Cutlerylover",
                Elegedettseg = 10,
                VelemenySzovege = "I have the version with the wave " +
                "feature. I'm pleased with it! You can see my review " +
                "here: https://www.youtube.com/watch?v=XfnyMcUJip4",
                Gyartasi_Cikkszam = KesLista[1].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Slicey Dicey",
                Elegedettseg = 6,
                VelemenySzovege = "Just a well known good piece to have " +
                "in your collection. I made a video about it you can see it " +
                "here: https://www.youtube.com/watch?v=eWWO9KMEIZI",
                Gyartasi_Cikkszam = KesLista[1].Gyartasi_Cikkszam
            });
            /*-------*/
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Slicey Dicey",
                Elegedettseg = 1,
                VelemenySzovege = "Very thin blade it's fragile",
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Nick Shabazz",
                Elegedettseg = 10,
                VelemenySzovege = "It's one of my favourite spyderco-s ever",
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });

            Kes ElvartKes = KesLista[1];
            MockoltKesRepo.Setup(x => x.Read()).Returns(KesLista.AsQueryable());
            MockoltVelemenyRepo.Setup(x => x.Read()).Returns(VelemenyLista.AsQueryable());
            NemCRUDLogic nemCRUDLogic = new NemCRUDLogic(MockoltKesRepo.Object, MockoltVelemenyRepo.Object);
            var kimenet = nemCRUDLogic.LegjobbanErtekelt(KesLista, VelemenyLista);
            Assert.That(kimenet, Is.EqualTo(ElvartKes));
        }
        [Test]
        public void Boltokcpms30()
        {
            Mock<IRepository<Kes_Bolt>> MockoltKesBoltRepo = new Mock<IRepository<Kes_Bolt>>(MockBehavior.Loose);
            Mock<IRepository<Kes>> MockoltKesRepo = new Mock<IRepository<Kes>>(MockBehavior.Loose);
            List<Kes_Bolt> KesBoltLista = new List<Kes_Bolt>();
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Hq",
                Cim = "564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                Weboldal = "https://www.bladehq.com/"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Extrametál Kés (üzlethálózat)",
                Cim = "Extrametál,Magyarország",
                Weboldal = "https://extrametal.hu/"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Shop",
                Cim = "Budapest, Vendel u. 15 - 17, 1096, Magyarország",
                Weboldal = "https://www.bladeshop.hu/"
            });
            List<Kes> KesLista = new List<Kes>();
            /*Elso Boltba---*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Chaparral",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 71,
                Acel = "CTS-XHP",
                Ar = 40790,
                Raktar_Id = KesBoltLista[0].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Delica",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 73,
                Acel = "VG-10",
                Ar = 35490,
                Raktar_Id = KesBoltLista[0].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Mini Griptilian 556",
                Markolat = "Noryl GTX",
                Bevont_Penge = true,
                Penge_Hossz = 74,
                Acel = "CPM-154",
                Ar = 38990,
                Raktar_Id = KesBoltLista[0].Raktar_Id
            });
            /*Masodik Boltba---*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Anthem 781",
                Markolat = "Titanium",
                Bevont_Penge = false,
                Penge_Hossz = 89,
                Acel = "CPM-S30V",
                Ar = 183690,
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Bugout 535-2001 Limited Edition",
                Markolat = "Grivory",
                Bevont_Penge = false,
                Penge_Hossz = 82,
                Acel = "CPM-S30V",
                Ar = 54190,
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            /*Harmadik Boltba---*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Spyderco",
                Modell_nev = "Para Military 2",
                Markolat = "G10",
                Bevont_Penge = false,
                Penge_Hossz = 87,
                Acel = "CPM-S30V",
                Ar = 63790,
                Raktar_Id = KesBoltLista[2].Raktar_Id
            });

            List<Kes_Bolt> ElvartKesBoltLista = new List<Kes_Bolt>()
            {
                KesBoltLista[1],
                KesBoltLista[2]
            };
            MockoltKesBoltRepo.Setup(x => x.Read()).Returns(KesBoltLista.AsQueryable());
            MockoltKesRepo.Setup(x => x.Read()).Returns(KesLista.AsQueryable());
            NemCRUDLogic nemCRUDLogic = new NemCRUDLogic(MockoltKesBoltRepo.Object, MockoltKesRepo.Object);
            var kimenet = nemCRUDLogic.Boltokcpms30(KesBoltLista, KesLista);
            Assert.That(kimenet, Is.EquivalentTo(ElvartKesBoltLista));
        }

    }
}
