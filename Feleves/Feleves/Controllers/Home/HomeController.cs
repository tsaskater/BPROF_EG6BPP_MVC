using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Feleves.Controllers.Home
{
    public class HomeController : Controller
    {
        VelemenyLogic Velemenylogic;
        KesLogic Keslogic;
        KesBoltLogic KesBoltlogic;

        public HomeController(VelemenyLogic Velemenylogic, KesLogic Keslogic, KesBoltLogic KesBoltlogic)
        {
            this.Velemenylogic = Velemenylogic;
            this.Keslogic = Keslogic;
            this.KesBoltlogic = KesBoltlogic;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
