using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Feleves.Controllers.Home
{
    public class HomeController : Controller
    {
        VelemenyLogic ReviewLogic;
        KesLogic KnifeLogic;
        KesBoltLogic KnifeStoreLogic;

        public HomeController(VelemenyLogic Velemenylogic, KesLogic Keslogic, KesBoltLogic KesBoltlogic)
        {
            this.ReviewLogic = Velemenylogic;
            this.KnifeLogic = Keslogic;
            this.KnifeStoreLogic = KesBoltlogic;
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
            kesbolt.Raktar_Id= Guid.NewGuid().ToString();
            KnifeStoreLogic.AddKesBolt(kesbolt);
            return RedirectToAction(nameof(ListKesBolt));
        }
        //KesBOLT Listazasok
        public IActionResult ListKesBolt()
        {
            return View(KnifeStoreLogic.GetAllKes_Bolt());

        }
        [HttpGet]
        public IActionResult AddKes(string id)
        {
            return View(nameof(AddKes),id);
        }
        [HttpPost]
        public IActionResult AddKes(Kes kes)
        {
            kes.Gyartasi_Cikkszam= Guid.NewGuid().ToString();
            KnifeLogic.AddKes(kes);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(kes.Raktar_Id));
        }
        //Kes Listazas
        [HttpGet]
        public IActionResult ListKes(string id)
        {
            return View(KnifeStoreLogic.GetKesek(id));

        }
        //Delete Kés    TODO:(Cascade deletere szükség lesz a foregin key miatt)
        [HttpGet]
        public IActionResult DeleteKes(string id)
        {
            Kes k = KnifeLogic.GetKes(id);
            KnifeLogic.DeleteKes(id);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(k.Raktar_Id));
        }


        //Velemeny hozzáadás    TODO: A elégedettség értékét "csillagokban" lehessen megadni legkevesebb fél csillag legnagyobb 5
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
        //Vissza lépés a véleményektől
        [HttpGet]
        public IActionResult BackToKesek(string id)//id itt Gyartasi_Cikkszam
        {
            Kes k = KnifeLogic.GetKes(id);
            return View(nameof(ListKes), KnifeStoreLogic.GetKesek(k.Raktar_Id));
        }


        //Adat Generátor    TODO:felhasználó barátabbá tétel MSGboxos kérdéssel, hogy véletlen ne töltsük fel kétszer ugyanazzal az adattal
        [HttpGet]
        public  IActionResult GenerateData()
        {
            /*---------------------------------------------------*/
            /*Kes_Bolt Kb = new Kes_Bolt{Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Hq",
                Cim = "564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                Weboldal = "https://www.bladehq.com/"};
            KnifeStoreLogic.AddKesBolt(Kb);
            Kes k = new Kes{Gyartasi_Cikkszam = Guid.NewGuid().ToString(),Gyarto = "Benchmade",
                Modell_nev = "Griptilian",Markolat = "FRN",Bevont_Penge = false,Penge_Hossz = 88,
                Acel = "CPM-S30V",Ar = 42290,Raktar_Id = Kb.Raktar_Id};
            KnifeLogic.AddKes(k); //TODO: Vélemények szöveg megírása
            Velemeny v = new Velemeny{Velemeny_Id = Guid.NewGuid().ToString(),Szerzo = "Nick Shabazz",
                Elegedettseg = 5,VelemenySzovege = "lorem ipsum",Gyartasi_Cikkszam = k.Gyartasi_Cikkszam};
            ReviewLogic.AddVelemeny(v);*/
            /*---------------------------------------------------*/
            Generator(
                /*Bolt Név:*/"Blade Hq",
                /*Bolt Cím:*/"564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                /*Bolt Weboldal:*/"https://www.bladehq.com/",
                /*Kés gyártó*/"Benchmade", /*Kés modell*/"Griptilian",/*Kés Markolat anyaga*/ "FRN",
                /*Bevont penge*/false,/*Kés pengehossz*/88,/*Kés acél*/"CPM-S30V",/*Kés gyártó*/42290,
                /*Vélemény szerző*/"Nick Shabazz",/*Vélemény értékelés*/ 5,
                /*Vélemény szöveg*/ "lorem ipsum");

            
            return RedirectToAction(nameof(Index));
        }
        private void Generator(string boltnev,string boltcim,string bolturl,
            string kesgyarto,string kesmodell,string kesmarkolat,bool bevont,
            int kespengehossz,string kesacel,int ar,string velemenyszerzonev,
            int velmenyertekeles,string velmenyszoveg)
        {
            Kes_Bolt Kb = new Kes_Bolt
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = boltnev,
                Cim = boltcim,
                Weboldal = bolturl
            };
            KnifeStoreLogic.AddKesBolt(Kb);
            Kes k = new Kes
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = kesgyarto,
                Modell_nev = kesmodell,
                Markolat = kesmarkolat,
                Bevont_Penge = bevont,
                Penge_Hossz = kespengehossz,
                Acel = kesacel,
                Ar = ar,
                Raktar_Id = Kb.Raktar_Id
            };
            KnifeLogic.AddKes(k); //TODO: Vélemények szöveg megírása
            Velemeny v = new Velemeny
            {
                Velemeny_Id = Guid.NewGuid().ToString(),
                Szerzo = velemenyszerzonev,
                Elegedettseg = velmenyertekeles,
                VelemenySzovege = velmenyszoveg,
                Gyartasi_Cikkszam = k.Gyartasi_Cikkszam
            };
            ReviewLogic.AddVelemeny(v);
        }

    }
}
