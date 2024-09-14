using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI_Layer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace UI_Layer.Controllers
{
    [Route("Recipe")]
    public class RecipeController : Controller
	{
		

		private readonly IConfiguration _configurtion;
        string baseurl = string.Empty;
        public RecipeController(IConfiguration configurtion)
        {
            this._configurtion = configurtion;
            baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
            baseurl += "Recipe";//http://localhost:36636/api/Recipe //from service layer

        }
        //[Route("/")]
        [HttpGet]
        [Route("RecipeList", Name = "RecipeList")]
        public async Task<IActionResult> RecipeList()
        {
            List<Recipe> recipes = new List<Recipe>();
            
            using (HttpClient httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"{baseurl}/GetAllRecipes"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        recipes = JsonConvert.DeserializeObject<List<Recipe>>(apiResponse);
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            //return Content("Invalid User");
                            return Unauthorized();
                        }
                    }
                }
            }

            return View(recipes);

        }

        [HttpGet]
        [Route("AddRecipe", Name = "AddRecipe")]
        public IActionResult AddRecipe()
        {
			//ViewBag.NewRecipeId = _recipeRepo.AutoRecipeId();
			return View();
        }
        [HttpPost]
        [Route("SaveRecipe", Name = "SaveRecipe")]
        public async Task<IActionResult> SaveRecipe(Recipe rcp)
        {
            //retrieve token from session

            //var token = HttpContext.Session.GetString("token");

            //token = token.Trim('"');

            using (HttpClient httpClient = new HttpClient())
            {
                //to pass token thorugh request header

               // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                StringContent content = new StringContent(JsonConvert.SerializeObject(rcp), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseurl}/AddRecipes", content))//name of method from service layer
                {
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToRoute("RecipeList");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }

        [HttpGet]
        [Route("GetRecipeById", Name = "GetRecipeById")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            Recipe recipe=new Recipe();
            using (var httpClient=new HttpClient()) 
            {
                using (var res = await httpClient.GetAsync($"{baseurl}/GetRecipeById/{id}"))
                {
                    if (res.IsSuccessStatusCode) { 
                    string apirespone= await res.Content.ReadAsStringAsync();
                        recipe=JsonConvert.DeserializeObject<Recipe>(apirespone);
                    }
                }
            }
            return View(recipe);
        }

        [HttpGet]
        [Route("DeleteRecipe/{id}", Name = "DeleteRecipe")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            #region Get Recipe By Id
            Recipe recipe = new Recipe();


            using (var httpClient = new HttpClient())
            {
                //http://localhost:36636/api/v1.0/ProductInfo/GetById
                using (var response = await httpClient.GetAsync($"{baseurl}/GetRecipeById/{id}"))
                {
                    if (response.IsSuccessStatusCode) //200-299
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        //To Convert Json string into List<EventInfo>
                        recipe = JsonConvert.DeserializeObject<Recipe>(apiResponse);
                    }
                }
            }
            #endregion
            return View(recipe);
        }
        [HttpPost]
        [Route("DeleteRecipes", Name = "DeleteRecipes")]
        public async Task<IActionResult> DeleteRecipe(Recipe recipe)
        {

            using (var httpClient = new HttpClient())
            {//http://localhost:36636/api/v1.0/ProductInfo/DeleteProduct/id
                using (var response = await httpClient.DeleteAsync($"{baseurl}/DeleteRecipe/{recipe.RecipeId}"))
                {
                    if (response.IsSuccessStatusCode) //200-299
                    {
                        return RedirectToRoute("RecipeList");
                    }
                }
            }
            return View(recipe);

        }
        [HttpGet]
        [Route("EditRecipe/{id}", Name = "EditRecipe")]
        public async Task<IActionResult> EditRecipe(int id)
        {
            #region Get  By Recipe Code
            Recipe recipe = new Recipe();


            using (var httpClient = new HttpClient())
            {
                //http://localhost:36636/api/v1.0/ProductInfo/GetById
                using (var response = await httpClient.GetAsync($"{baseurl}/GetRecipeById/{id}"))
                {
                    if (response.IsSuccessStatusCode) //200-299
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        //To Convert Json string into List<EventInfo>
                        recipe = JsonConvert.DeserializeObject<Recipe>(apiResponse);
                    }
                }
            }
            #endregion
            return View(recipe);
        }

        [HttpPost]
        [Route("UpdateRecipe", Name = "UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe(Recipe recipe)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync($"{baseurl}/UpdateRecipe",content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToRoute("RecipeList");
                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                }
            }
        }
    }
}