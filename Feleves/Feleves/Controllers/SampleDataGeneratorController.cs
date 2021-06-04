using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feleves.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("SampleDataGenerator")]
    [ApiController]
    public class SampleDataGeneratorController : ControllerBase
    {
        KesBoltLogic KnifeStoreLogic;
        VelemenyLogic ReviewLogic;
        KesLogic KnifeLogic;

        public SampleDataGeneratorController(KesBoltLogic knifeStoreLogic, VelemenyLogic reviewLogic, KesLogic knifeLogic)
        {
            KnifeStoreLogic = knifeStoreLogic;
            ReviewLogic = reviewLogic;
            KnifeLogic = knifeLogic;
        }

        [HttpPost]
        public IActionResult GenerateData()
        {
            try
            {
                GenerateSampleData();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        private void GenerateSampleData()
        {
            /*BOLTOK:-----------*/
            List<Kes_Bolt> KesBoltLista = new List<Kes_Bolt>();
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Hq",
                Cim = "564 West 700 South, Pleasant Grove, UT 84062, Egyesült Államok",
                Weboldal = "https://www.bladehq.com"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Extrametál Kés (üzlethálózat)",
                Cim = "Extrametál,Magyarország",
                Weboldal = "https://extrametal.hu"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Blade Shop",
                Cim = "Budapest, Vendel u. 15 - 17, 1096, Magyarország",
                Weboldal = "https://www.bladeshop.hu"
            });
            KesBoltLista.Add(new Kes_Bolt()
            {
                Raktar_Id = Guid.NewGuid().ToString(),
                Bolt_Nev = "Knife Center",
                Cim = "5201 Lad Land Dr, Fredericksburg, VA 22407, Egyesült Államok",
                Weboldal = "https://www.knifecenter.com"
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
