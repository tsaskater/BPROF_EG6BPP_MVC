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
    [Route("Knife")]
    [ApiController]
    public class KnifeController : ControllerBase
    {
        KesLogic knifeLogic;

        public KnifeController(KesLogic knfieLogic)
        {
            this.knifeLogic = knfieLogic;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKes(string id)
        {
            try
            {
                knifeLogic.DeleteKes(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOneKnife(string id)
        {
            try
            {
                return Ok(knifeLogic.GetKes(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet]
        public IActionResult GetAllKnifes()
        {
            try
            {
                return Ok(knifeLogic.GetAllKes());
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet("AllKnifesForKnifeStore/{id}")]
        public IActionResult GetAllKnifesForKnifeStore(string id)
        {
            try
            {
                return Ok(knifeLogic.GetAllKesForKesbolt(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpPost]
        public IActionResult AddKnife([FromBody] Kes k)
        {
            try
            {
                k.Gyartasi_Cikkszam = Guid.NewGuid().ToString();
                knifeLogic.AddKes(k);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateKnife(string id, [FromBody] Kes newKnife)
        {
            try
            {
                knifeLogic.UpdateKes(id, newKnife);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
    }
}
