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
        KesLogic knfieLogic;

        public KnifeController(KesLogic knfieLogic)
        {
            this.knfieLogic = knfieLogic;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKes(string id)
        {
            try
            {
                knfieLogic.DeleteKes(id);
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
                return Ok(knfieLogic.GetKes(id));
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
                return Ok(knfieLogic.GetAllKes());
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
                return Ok(knfieLogic.GetAllKesForKesbolt(id));
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
                knfieLogic.AddKes(k);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
