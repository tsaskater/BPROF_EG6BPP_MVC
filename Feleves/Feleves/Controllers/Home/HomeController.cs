using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using static Models.Statisztika;

namespace Feleves.Controllers.Home
{
    public class HomeController : Controller
    {
        VelemenyLogic ReviewLogic;
        KesLogic KnifeLogic;
        KesBoltLogic KnifeStoreLogic;
        NemCRUDLogic NonCrudLogic;

        public HomeController(VelemenyLogic Velemenylogic, KesLogic Keslogic, KesBoltLogic KesBoltlogic, NemCRUDLogic NonCrudLogic)
        {
            this.ReviewLogic = Velemenylogic;
            this.KnifeLogic = Keslogic;
            this.KnifeStoreLogic = KesBoltlogic;
            this.NonCrudLogic = NonCrudLogic;
        }

        public IActionResult Index()
        {
            return View();
        }
        //KesBOLT
        [HttpGet]
        public IActionResult AddKesBolt()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddKesBolt(Kes_Bolt kesbolt)
        {
            kesbolt.Raktar_Id = Guid.NewGuid().ToString();
            KnifeStoreLogic.AddKesBolt(kesbolt);
            return RedirectToAction(nameof(ListKesBolt));
        }
        //KesBOLT Listazasok
        [HttpGet]
        public IActionResult ListKesBolt()
        {
            return View(KnifeStoreLogic.GetAllKes_Bolt());

        }
        //KesBolt Szerkesztése
        [HttpGet]
        public IActionResult EditKesBolt(string id)
        {
            Kes_Bolt kb = KnifeStoreLogic.GetKes_Bolt(id);
            return View(kb);
        }
        [HttpPost]
        public IActionResult EditKesBolt(Kes_Bolt kb)
        {
            KnifeStoreLogic.UpdateKes_Bolt(kb.Raktar_Id, kb);
            return RedirectToAction(nameof(ListKesBolt));
        }
        //KesBolt Törlés
        public IActionResult DeleteKesBolt(string id)
        {
            KnifeStoreLogic.DeleteKesBolt(id);
            return RedirectToAction(nameof(ListKesBolt));
        }
        //Kes Hozzadas
        [HttpGet]
        public IActionResult AddKes(string id)
        {
            return View(nameof(AddKes), id);
        }
        [HttpPost]
        public IActionResult AddKes(Kes kes)
        {
            kes.Gyartasi_Cikkszam = Guid.NewGuid().ToString();
            KnifeLogic.AddKes(kes);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(kes.Raktar_Id));
        }
        //Kes Listazas
        [HttpGet]
        public IActionResult ListKes(string id)
        {
            return View(KnifeStoreLogic.GetKesek(id));

        }

        //Kes szerkesztes
        [HttpGet]
        public IActionResult EditKes(string id)
        {
            Kes k = KnifeLogic.GetKes(id);
            return View(k);
        }
        [HttpPost]
        public IActionResult EditKes(Kes k)
        {
            KnifeLogic.UpdateKes(k.Gyartasi_Cikkszam, k);
            return RedirectToAction(nameof(ListKes), new { id = k.Raktar_Id });
        }
        //Delete Kés    TODO:(Cascade deletere szükség lesz a foregin key miatt) megoldva:)
        [HttpGet]
        public IActionResult DeleteKes(string id)
        {
            Kes k = KnifeLogic.GetKes(id);
            KnifeLogic.DeleteKes(id);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(k.Raktar_Id));
        }


        //Velemeny hozzáadás    TODO: A elégedettség értékét "csillagokban" lehessen megadni legkevesebb fél csillag legnagyobb 5 megoldva:)
        [HttpGet]
        public IActionResult AddVelemeny(string id)
        {
            return View(nameof(AddVelemeny), id);
        }
        [HttpPost]
        public IActionResult AddVelemeny(Velemeny velemeny)
        {
            velemeny.Velemeny_Id = Guid.NewGuid().ToString();
            ReviewLogic.AddVelemeny(velemeny);
            return View(nameof(ListVelemeny), KnifeLogic.GetVelemenyek(velemeny.Gyartasi_Cikkszam));
        }
        //Velemenyek Listazas
        [HttpGet]
        public IActionResult ListVelemeny(string id)
        {
            return View(KnifeLogic.GetVelemenyek(id));
        }

        //Vélemény szerkesztés
        [HttpGet]
        public IActionResult EditVelemeny(string id)
        {
            Velemeny v = ReviewLogic.GetVelemeny(id);
            return View(v);
        }
        [HttpPost]
        public IActionResult EditVelemeny(Velemeny v)
        {
            ReviewLogic.UpdateVelemeny(v.Velemeny_Id, v);
            return RedirectToAction(nameof(ListVelemeny), new { id = v.Gyartasi_Cikkszam });
        }
        [HttpGet]
        public IActionResult DeleteVelemeny(string id)
        {
            Velemeny v = ReviewLogic.GetVelemeny(id);
            ReviewLogic.DeleteVelemeny(id);
            return View(nameof(ListVelemeny), KnifeLogic.GetVelemenyek(v.Gyartasi_Cikkszam));
        }
        //Vissza lépés a véleményektől
        [HttpGet]
        public IActionResult BackToKesek(string id)//id itt Gyartasi_Cikkszam
        {
            Kes k = KnifeLogic.GetKes(id);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(k.Raktar_Id));
        }


        //Adat Generátor    TODO:felhasználó barátabbá tétel MSGboxos kérdéssel, hogy véletlen ne töltsük fel kétszer ugyanazzal az adattal
        [HttpGet]
        public IActionResult GenerateData()
        {
            GenerateSampleData();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ListLegalis()
        {
            /*List<Kes_Bolt> kesboltok = KnifeStoreLogic.GetAllKes_Bolt().ToList();
            List<Kes> kesek = KnifeLogic.GetAllKes().ToList();
            var q1 =
                (from x in kesboltok
                 join y in kesek on x.Raktar_Id equals y.Raktar_Id into g
                from Kesek in g
                where Kesek.Penge_Hossz <= 80
                select new Legalis { Termek= Kesek, Bolt=x});*/
            return View(NonCrudLogic.Legalis(KnifeStoreLogic.GetAllKes_Bolt().ToList(),KnifeLogic.GetAllKes().ToList()));
        }
        public IActionResult ListLegjobbanErtekelt()
        {
            return View(NonCrudLogic.LegjobbanErtekelt(KnifeLogic.GetAllKes().ToList(),ReviewLogic.GetAllVelemeny().ToList()));
        }
        public IActionResult ListBoltokcpms30()
        {
            return View(NonCrudLogic.Boltokcpms30(KnifeStoreLogic.GetAllKes_Bolt().ToList(), KnifeLogic.GetAllKes().ToList()));
        }


        //Statikusan előre megírt függvény az adatgeneráláshoz
        public void GenerateSampleData()
        {
            /*BOLTOK:-----------*/
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
                Cim = "Extrametál,Magyaország",
                Weboldal = "https://extrametal.hu/"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Shop",
                Cim = "Budapest, Vendel u. 15 - 17, 1096, Magyarország",
                Weboldal = "https://www.bladeshop.hu/"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Knife Center",
                Cim = "5201 Lad Land Dr, Fredericksburg, VA 22407, Egyesült Államok",
                Weboldal = "https://www.knifecenter.com/"
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
                Gyarto = "Benchmade",
                Modell_nev = "Mini Griptilian 556",
                Markolat = "Noryl GTX",
                Bevont_Penge = true,
                Penge_Hossz = 74,
                Acel = "CPM-S30V",
                Ar = 38990,
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
            /*MASODIK BOLTBA------*/
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
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Ontario",
                Modell_nev = "Rat 1",
                Markolat = "GRN",
                Bevont_Penge = false,
                Penge_Hossz = 92,
                Acel = "D2",
                Ar = 16390,
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Ontario",
                Modell_nev = "Rat 2",
                Markolat = "GRN",
                Bevont_Penge = false,
                Penge_Hossz = 76,
                Acel = "D2",
                Ar = 15390,
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
                Raktar_Id = KesBoltLista[1].Raktar_Id
            });
            /*HARMADIK BOLTBA---------*/
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Ontario",
                Modell_nev = "Rat 1",
                Markolat = "GRN",
                Bevont_Penge = false,
                Penge_Hossz = 92,
                Acel = "D2",
                Ar = 16390,
                Raktar_Id = KesBoltLista[2].Raktar_Id
            });
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
                Raktar_Id = KesBoltLista[2].Raktar_Id
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
                Raktar_Id = KesBoltLista[2].Raktar_Id
            });
            KesLista.Add(new Kes()
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Ontario",
                Modell_nev = "Rat 2",
                Markolat = "GRN",
                Bevont_Penge = false,
                Penge_Hossz = 76,
                Acel = "AUS-8",
                Ar = 15390,
                Raktar_Id = KesBoltLista[2].Raktar_Id
            });
            /*VELEMENYEK------------*/
            List<Velemeny> VelemenyLista = new List<Velemeny>();
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Nick Shabazz",
                Elegedettseg = 8,
                VelemenySzovege = "I'd reccomend this product" +
                "You can check out my review here:" +
                "https://www.youtube.com/watch?v=N-0ERB3tBOU",
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Cutlerylover",
                Elegedettseg = 10,
                VelemenySzovege = "I have the version with the wave " +
                "feature. I'm pleased with it! You can see my review " +
                "here: https://www.youtube.com/watch?v=XfnyMcUJip4",
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "Slicey Dicey",
                Elegedettseg = 6,
                VelemenySzovege = "Just a well known good piece to have " +
                "in your collection. I made a video about it you can see it " +
                "here: https://www.youtube.com/watch?v=eWWO9KMEIZI",
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "User 1",
                Elegedettseg = 4,
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "User 2",
                Elegedettseg = 5,
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "User 3",
                Elegedettseg = 4,
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            VelemenyLista.Add(new Velemeny()
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = "User 4",
                Elegedettseg = 1,
                Gyartasi_Cikkszam = KesLista[0].Gyartasi_Cikkszam
            });
            /*---------*/
            for (int i = 1; i < KesLista.Count; i++)
            {
                Random rnd = new Random();
                for (int j = 0; j < 5; j++)
                {
                    VelemenyLista.Add(new Velemeny()
                    {
                        Velemeny_Id = Guid.NewGuid().ToString(),
                        Szerzo = $"User {rnd.Next(1, 10)}",
                        Elegedettseg = rnd.Next(1, 10),
                        Gyartasi_Cikkszam = KesLista[i].Gyartasi_Cikkszam
                    });
                }
            }
            /*----------*/
            foreach (var item in KesBoltLista)
            {
                KnifeStoreLogic.AddKesBolt(item);
            }
            foreach (var item in KesLista)
            {
                KnifeLogic.AddKes(item);
            }
            foreach (var item in VelemenyLista)
            {
                ReviewLogic.AddVelemeny(item);
            }

        }

    }
}
