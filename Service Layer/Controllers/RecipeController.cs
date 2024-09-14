using DAL.Repository;
using DAL.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepo<Recipe> _recipeRepo;
        public RecipeController(IRecipeRepo<Recipe> recipeRepo) 
        {
            this._recipeRepo = recipeRepo;
        }
        [HttpPost]
        [Route("AddRecipes")]
        [Consumes("application/json")]
        [Produces("application/json")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
			
			//if (recipe == null)
			//{
			//	return BadRequest();
			//}

			//recipe.RecipeId = _recipeRepo.AutoRecipeId(); // Generate the RecipeId

			var newRecipe = _recipeRepo.AddRecipe(recipe);
			if (newRecipe != null)
			{
				return Ok(newRecipe); // 200 OK
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete]
		[Route("DeleteRecipe/{id}")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult DeleteRecipe(int id)
		{
			var deletedRecipe = _recipeRepo.DeleteRecipe(id);
			if (deletedRecipe != null)
			{
				return Ok(deletedRecipe); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpPut]
		[Route("UpdateRecipe")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult UpdateRecipe([FromBody] Recipe recipe)
		{
			var updatedRecipe = _recipeRepo.UpdateRecipe(recipe);
			if (updatedRecipe != null)
			{
				return Ok(updatedRecipe); // 200
			}
			else
			{
				return BadRequest(); // 400
			}
		}
		[HttpGet]
		[Route("GetRecipeById/{id}")]
		[Produces("application/json")]
		public IActionResult GetRecipeById(int id)
		{
			var recipe = _recipeRepo.GetRecipeById(id);
			if (recipe != null)
			{
				return Ok(recipe); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpGet]
		[Route("GetAllRecipes")]
		[Produces("application/json")]
		public IActionResult GetAllRecipe()
		{
			//     var products = _productRepo.GetAllProducts();
			//return Ok(products);
			var recipes = _recipeRepo.GetAllRecipe();
			if (recipes != null && recipes.Any())
			{
				return Ok(recipes); // 200
			}
			else
			{
				return NoContent(); // 204
			}
		}
	}
}
