using DAL.Models;
using DAL.Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service_Layer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientController : ControllerBase
	{
		private readonly IIngredientRepo<Ingredient> _ingredientRepo;
		public IngredientController(IIngredientRepo<Ingredient> ingredientRepo)
		{
			this._ingredientRepo = ingredientRepo;
		}
		[HttpPost]
		[Route("AddIngredient")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult AddIngredient([FromBody] Ingredient ingredient)
		{
			var newingredient = _ingredientRepo.AddIngredient(ingredient);
			if (newingredient != null)
			{
				return Ok(newingredient);//200
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete]
		[Route("DeleteIngredient/{id}")]
		[Produces("application/json")]
		public IActionResult DeleteIngredient(int id)
		{
			var deletedIngredient = _ingredientRepo.DeleteIngredient(id);
			if (deletedIngredient != null)
			{
				return Ok(deletedIngredient); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpPut]
		[Route("UpdateIngredient")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult UpdateIngredient([FromBody] Ingredient ingredient)
		{
			var updatedIngredient = _ingredientRepo.UpdateIngredient(ingredient);
			if (updatedIngredient != null)
			{
				return Ok(updatedIngredient); // 200
			}
			else
			{
				return BadRequest(); // 400
			}
		}
		[HttpGet]
		[Route("GetIngredientById/{id}")]
		[Produces("application/json")]
		public IActionResult GetIngredientById(int id)
		{
			var ingredient = _ingredientRepo.GetIngredientById(id);
			if (ingredient != null)
			{
				return Ok(ingredient); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpGet]
		[Route("GetAllIngredients")]
		[Produces("application/json")]
		public IActionResult GetAllIngredients()
		{
			var ingredients = _ingredientRepo.GelAllIngredient();
			if (ingredients != null && ingredients.Any())
			{
				return Ok(ingredients); // 200
			}
			else
			{
				return NoContent(); // 204
			}
		}
	}
}
