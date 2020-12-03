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
        public IActionResult AddKes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddKes(Kes kes)
        {
            kes.Gyartasi_Cikkszam= Guid.NewGuid().ToString();
            KnifeLogic.AddKes(kes);
            return View();
        }
        
        [HttpGet]
        public IActionResult AddVelemeny()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddVelemeny(Velemeny velemeny)
        {
            velemeny.Velemeny_Id = Guid.NewGuid().ToString();
            ReviewLogic.AddVelemeny(velemeny);
            return View();
        }

        
    }
}
