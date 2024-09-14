using DAL.Models;
using DAL.Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service_Layer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RatingController : ControllerBase
	{
		private readonly IRatingRepo<Rating> _ratingRepo;
		public RatingController(IRatingRepo<Rating> ratingRepo)
		{
			this._ratingRepo = ratingRepo;
		}
		[HttpPost]
		[Route("AddRating")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult AddRating([FromBody] Rating rating)
		{
			var newrating = _ratingRepo.AddRating(rating);
			if (newrating != null)
			{
				return Ok(newrating);//200
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete]
		[Route("DeleteRating/{id}")]
		[Produces("application/json")]
		public IActionResult DeleteRating(int id)
		{
			var deletedRating = _ratingRepo.DeleteRating(id);
			if (deletedRating != null)
			{
				return Ok(deletedRating); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpPut]
		[Route("UpdateRating")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult UpdateRating([FromBody] Rating rating)
		{
			var updatedRating = _ratingRepo.UpdateRating(rating);
			if (updatedRating != null)
			{
				return Ok(updatedRating); // 200
			}
			else
			{
				return BadRequest(); // 400
			}
		}
		[HttpGet]
		[Route("GetRatingById/{id}")]
		[Produces("application/json")]
		public IActionResult GetRatingById(int id)
		{
			var rating = _ratingRepo.GetRatingById(id);
			if (rating != null)
			{
				return Ok(rating); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpGet]
		[Route("GetAllRatings")]
		[Produces("application/json")]
		public IActionResult GetAllRatings()
		{
			var ratings = _ratingRepo.GetAllRating();
			if (ratings != null && ratings.Any())
			{
				return Ok(ratings); // 200
			}
			else
			{
				return NoContent(); // 204
			}
		}
	}
}
