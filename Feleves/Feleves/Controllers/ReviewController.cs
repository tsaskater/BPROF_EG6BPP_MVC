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
    [Authorize]
    [Route("Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        VelemenyLogic reviewLogic;

        public ReviewController(VelemenyLogic reviewLogic)
        {
            this.reviewLogic = reviewLogic;
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IActionResult AddReview([FromBody] Velemeny v)
        {
            try
            {
                v.Velemeny_Id = Guid.NewGuid().ToString();
                reviewLogic.AddVelemeny(v);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateReview(string id, [FromBody] Velemeny newVelmeny)
        {
            try
            {
                reviewLogic.UpdateVelemeny(id, newVelmeny);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request error: {ex}");
            }
        }
    }
}
