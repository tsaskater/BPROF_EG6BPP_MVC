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
    [Route("Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        VelemenyLogic reviewLogic;

        public ReviewController(VelemenyLogic reviewLogic)
        {
            this.reviewLogic = reviewLogic;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(string id)
        {
            try
            {
                reviewLogic.DeleteVelemeny(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOneReview(string id)
        {
            try
            {
                return Ok(reviewLogic.GetVelemeny(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                return Ok(reviewLogic.GetAllVelemeny());
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpGet("GetAllReviewsForKnife/{id}")]
        public IActionResult GetAllReviewsForKnife(string id)
        {
            try
            {
                return Ok(reviewLogic.GetAllVelemenyForKes(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
        [HttpPost]
        public IActionResult AddKnife([FromBody] Velemeny v)
        {
            try
            {
                reviewLogic.AddVelemeny(v);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
