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
            return RedirectToAction(nameof(Index));
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
            Generator(
                /*Bolt Név:*/"Blade Hq",
                /*Bolt Cím:*/"564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                /*Bolt Weboldal:*/"https://www.bladehq.com/",
                /*Kés gyártó*/"Benchmade", /*Kés modell*/"Griptilian",/*Kés Markolat anyaga*/ "FRN",
                /*Bevont penge*/false,/*Kés pengehossz*/88,/*Kés acél*/"CPM-S30V",/*Kés gyártó*/42290,
                /*Vélemény szerző*/"Nick Shabazz",/*Vélemény értékelés*/ 5,
                /*Vélemény szöveg*/ "lorem ipsum");
            /*--------------------------------------*/
            Generator(
                /*Bolt Név:*/"Blade Shop",
                /*Bolt Cím:*/"Budapest, Vendel u. 15 - 17, 1096, Magyarország",
                /*Bolt Weboldal:*/"https://www.bladeshop.hu/",
                /*Kés gyártó*/"Spyderco", /*Kés modell*/"Delica",/*Kés Markolat anyaga*/ "FRN",
                /*Bevont penge*/true,/*Kés pengehossz*/76,/*Kés acél*/"VG-10",/*Kés gyártó*/32000,
                /*Vélemény szerző*/"Cutlerylover",/*Vélemény értékelés*/ 8,
                /*Vélemény szöveg*/ "lorem ipsum");
            /*--------------------------------------*/
            Generator(
                /*Bolt Név:*/"Extrametál Kés (üzlethálózat)",
                /*Bolt Cím:*/"Extrametál,Magyaország",
                /*Bolt Weboldal:*/"https://extrametal.hu/",
                /*Kés gyártó*/"Bestech", /*Kés modell*/"Paladin",/*Kés Markolat anyaga*/ "G10",
                /*Bevont penge*/false,/*Kés pengehossz*/92,/*Kés acél*/"D2",/*Kés gyártó*/19990,
                /*Vélemény szerző*/"Slicey Dicey",/*Vélemény értékelés*/ 6,
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
