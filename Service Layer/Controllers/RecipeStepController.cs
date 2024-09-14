using DAL.Models;
using DAL.Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service_Layer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeStepController : ControllerBase
	{
		private readonly IRecipeStepRepo<RecipeStep> _recipestepRepo;
		public RecipeStepController(IRecipeStepRepo<RecipeStep> recipestepRepo)
		{
			this._recipestepRepo = recipestepRepo;
		}
		[HttpPost]
		[Route("AddRecipeStep")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult AddRecipeStep([FromBody] RecipeStep recipestep)
		{
			var newRecipestep = _recipestepRepo.AddRecipeStep(recipestep);
			if (newRecipestep != null)
			{
				return Ok(newRecipestep);//200
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete]
		[Route("DeleteRecipeStepof/{id}")]
		[Produces("application/json")]
		public IActionResult DeleteRecipeStep(int id)
		{
			var deletedRecipeStep = _recipestepRepo.DeleteRecipeStep(id);
			if (deletedRecipeStep != null)
			{
				return Ok(deletedRecipeStep); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpPut]
		[Route("UpdateRecipeStep")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult UpdateRecipeStep([FromBody] RecipeStep recipestep)
		{
			var updatedRecipeStep = _recipestepRepo.UpdateRecipeStep(recipestep);
			if (updatedRecipeStep != null)
			{
				return Ok(updatedRecipeStep); // 200
			}
			else
			{
				return BadRequest(); // 400
			}
		}
		[HttpGet]
		[Route("GetRecipeStepById/{id}")]
		[Produces("application/json")]
		public IActionResult GetRecipeStepById(int id)
		{
			var recipeStep = _recipestepRepo.GetRecipeStepById(id);
			if (recipeStep != null)
			{
				return Ok(recipeStep); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpGet]
		[Route("GetAllRecipeSteps")]
		[Produces("application/json")]
		public IActionResult GetAllRecipeSteps()
		{
			var recipeSteps = _recipestepRepo.GetAllRecipeStep();
			if (recipeSteps != null && recipeSteps.Any())
			{
				return Ok(recipeSteps); // 200
			}
			else
			{
				return NoContent(); // 204
			}
		}
	}
}
