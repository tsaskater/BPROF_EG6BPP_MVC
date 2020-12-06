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




        //Adat Generátor    TODO:felhasználó barátabbá tétel MSGboxos kérdéssel, hogy véletlen ne töltsük fel kétszer ugyanazzal az adattal
        public  IActionResult GenerateData()
        {
            Kes_Bolt Kb = new Kes_Bolt
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Hq",
                Cim = "564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                Weboldal = "https://www.bladehq.com/"
            };
            KnifeStoreLogic.AddKesBolt(Kb);
            Kes k = new Kes
            {
                Gyartasi_Cikkszam = Guid.NewGuid().ToString(),
                Gyarto = "Benchmade",
                Modell_nev = "Griptilian",
                Markolat = "FRN",
                Bevont_Penge = false,
                Penge_Hossz = 88,
                Acel = "CPM-S30V",
                Ar = 42290,
                Raktar_Id = Kb.Raktar_Id
            };
            KnifeLogic.AddKes(k);

            return View(nameof(Index));
        }

    }
}
