using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI_Layer.Models;

namespace UI_Layer.Controllers
{
	[Route("Ingredient")]
	public class IngredientController : Controller
	{
		private readonly IConfiguration _configurtion;
		string baseurl = string.Empty;
		public IngredientController(IConfiguration configurtion)
		{
			this._configurtion = configurtion;
			baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
			baseurl += "Ingredient";//http://localhost:36636/api/Recipe //from service layer

		}
		//[Route("/")]
		[Route("IngredientList", Name = "IngredientList")]
		public async Task<IActionResult> IngredientList()
		{
			List<Ingredient> ing = new List<Ingredient>();

			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetAllIngredients"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						ing = JsonConvert.DeserializeObject<List<Ingredient>>(apiresponse);
					}
				}
			}
			return View(ing);
		}
		[HttpGet]
		[Route("AddIngredient", Name = "AddIngredient")]
		public IActionResult AddIngredient()
		{
			return View();
		}

		[HttpPost]
		[Route("SaveIngredient", Name = "SaveIngredient")]
		public async Task<IActionResult> SaveIngredient(Ingredient ingredient)
		{
			using (var httpclient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
				using (var response = await httpclient.PostAsync($"{baseurl}/AddIngredient", content))
				{
					if (response.IsSuccessStatusCode)
					{
						//return RedirectToAction("IngredientList");
						return RedirectToRoute("IngredientList");
					}
					else
					{
						return BadRequest();
						//return View("AddIngredient", ingredient);
					}
				}
			}
		}
		[HttpGet]
		[Route("GetIngredientById/{id}", Name = "GetIngredientById")]
		public async Task<IActionResult> GetIngredientById(int id)
		{
			Ingredient ingredient = new Ingredient();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetIngredientById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						ingredient = JsonConvert.DeserializeObject<Ingredient>(apiresponse);
					}
				}
			}

			return View(ingredient);
		}
		[HttpGet]
		[Route("DeleteIngredient/{id}", Name = "DeleteIngredient")]
		public async Task<IActionResult> DeleteIngredient(int id)
		{
			#region Get Ingredient By Id
			Ingredient ingredient = new Ingredient();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetIngredientById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						ingredient = JsonConvert.DeserializeObject<Ingredient>(apiresponse);
					}
				}
			}



			#endregion
			return View(ingredient);
			
		}
		[HttpPost]
		[Route("DeleteIngre/{id}", Name = "DeleteIngre")]
		
		public async Task<IActionResult> DeleteIngredient(Ingredient ing)
		{

			using (var httpClient = new HttpClient())
			{//http://localhost:36636/api/v1.0/ProductInfo/DeleteProduct/id
				using (var response = await httpClient.DeleteAsync($"{baseurl}/DeleteIngredient/{ing.IngredientId}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						return RedirectToRoute("IngredientList");
					}
				}
			}
			return View(ing);

		}
		[HttpGet]
		[Route("EditIngredient/{id}", Name = "EditIngredient")]
		public async Task<IActionResult> EditIngredient(int id)
		{
			Ingredient ingredient = new Ingredient();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetIngredientById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						ingredient = JsonConvert.DeserializeObject<Ingredient>(apiresponse);
					}
				}
			}

			return View(ingredient);
		}
		[HttpPost]
		[Route("UpdateIngredient", Name = "UpdateIngredient")]
		public async Task<IActionResult> UpdateIngredient(Ingredient ingredient)
		{
			using (var httpclient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(ingredient), Encoding.UTF8, "application/json");
				using (var response = await httpclient.PutAsync($"{baseurl}/UpdateIngredient", content))
				{
					if (response.IsSuccessStatusCode)
					{
						//return RedirectToAction("IngredientList");
						return RedirectToRoute("IngredientList");
					}
					else
					{
						//return View("EditIngredient", ingredient);
						return BadRequest(response);
					}
				}
			}
		}
	}
}
