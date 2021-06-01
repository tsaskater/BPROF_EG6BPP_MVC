using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feleves.Controllers
{
    [Route("KnifeStore")]
    [ApiController]
    public class KnifeStoreController : ControllerBase
    {
        KesBoltLogic knifeStoreLogic;

        public KnifeStoreController(KesBoltLogic knifeStoreLogic)
        {
            this.knifeStoreLogic = knifeStoreLogic;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKnifeStore(string id)
        {
            try
            {
                knifeStoreLogic.DeleteKesBolt(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOneKnifeStore(string id)
        {
            try
            {
                return Ok(knifeStoreLogic.GetKes_Bolt(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet]
        public IActionResult GetAllKnifeStores()
        {
            try
            {
                return Ok(knifeStoreLogic.GetAllKes_Bolt());
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateKnifeStore(string id,[FromBody] Kes_Bolt newKnifeStore)
        {
            try
            {
                knifeStoreLogic.UpdateKes_Bolt(id, newKnifeStore);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpPost]
        public IActionResult AddKnifeStore([FromBody] Kes_Bolt k)
        {
            try
            {
                k.Raktar_Id = Guid.NewGuid().ToString();
                knifeStoreLogic.AddKesBolt(k);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
