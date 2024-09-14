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
	[Route("RecipeStep")]
	public class RecipeStepController : Controller
	{
		private readonly IConfiguration _configurtion;
		string baseurl = string.Empty;
		public RecipeStepController(IConfiguration configurtion)
		{
			this._configurtion = configurtion;
			baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
			baseurl += "RecipeStep";//http://localhost:36636/api/v1.0/ProductInfo

		}
		//[Route("/")]
		[Route("RecipeStepList", Name = "RecipeStepList")]
		public async Task<IActionResult> RecipeStepList()
		{
			List<RecipeStep> recipestep = new List<RecipeStep>();

			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetAllRecipeSteps"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						recipestep = JsonConvert.DeserializeObject<List<RecipeStep>>(apiresponse);
					}
				}
			}
			return View(recipestep);
		}

		[HttpGet]
		[Route("AddRecipeStep", Name = "AddRecipeStep")]
		public IActionResult AddRecipeStep()
		{
			return View();
		}

		[HttpPost]
		[Route("SaveRecipeStep", Name = "SaveRecipeStep")]
		public async Task<IActionResult> SaveRecipeStep(RecipeStep recstp)
		{
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(recstp), Encoding.UTF8, "application/json");
				//http://localhost:36636/api/v1.0/ProductInfo/SaveProduct
				using (var response = await httpClient.PostAsync($"{baseurl}/AddRecipeStep", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("RecipeStepList");
					}
					else
					{
						return BadRequest(response);
					}
				}
			}
		}

		[Route("GetRecipestepById/{id}", Name = "GetRecipestepById")]
		public async Task<IActionResult> GetRecipeStepById(int id)
		{
		RecipeStep rcpstp = new RecipeStep();


			using (var httpClient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetById
				using (var response = await httpClient.GetAsync($"{baseurl}/GetRecipeStepById/{id}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						string apiResponse = await response.Content.ReadAsStringAsync();

						//To Convert Json string into List<EventInfo>
						rcpstp = JsonConvert.DeserializeObject<RecipeStep>(apiResponse);
					}
				}
			}
			return View(rcpstp);

		}

		[HttpGet]
		[Route("DeleteRecipeStep/{id}", Name = "DeleteRecipeStep")]
		public async Task<IActionResult> DeleteRecipeStep(int id)
		{
			#region Get RecipeStep By Id
			RecipeStep rcpstp = new RecipeStep();


			using (var httpClient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetById
				using (var response = await httpClient.GetAsync($"{baseurl}/GetRecipeStepById/{id}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						string apiResponse = await response.Content.ReadAsStringAsync();

						//To Convert Json string into List<EventInfo>
						rcpstp = JsonConvert.DeserializeObject<RecipeStep>(apiResponse);
					}
				}
			}
			
			#endregion
			return View(rcpstp);
		}

		[HttpPost]
		[Route("RemoveRecipeSteps", Name = "RemoveRecipeSteps")]
		public async Task<IActionResult> DeleteRecipeStep(RecipeStep rcpstp)
		{

			using (var httpClient = new HttpClient())
			{//http://localhost:36636/api/v1.0/ProductInfo/DeleteProduct/id
				using (var response = await httpClient.DeleteAsync($"{baseurl}/DeleteRecipeStepof/{rcpstp.RecipeStepId}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						return RedirectToRoute("RecipeStepList");
					}
				}
			}
			return View(rcpstp);

		}

		[HttpGet]
		[Route("EditRecipeStep/{id}", Name = "EditRecipeStep")]
		public async Task<IActionResult> UpdateRecipeStep(int id)
		{
			#region Get Recipestep By Id
			RecipeStep rcpstp = new RecipeStep();


			using (var httpClient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetById
				using (var response = await httpClient.GetAsync($"{baseurl}/GetRecipeStepById/{id}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						string apiResponse = await response.Content.ReadAsStringAsync();

						//To Convert Json string into List<EventInfo>
						rcpstp = JsonConvert.DeserializeObject<RecipeStep>(apiResponse);
					}
				}
			}
			#endregion
			return View(rcpstp);
		}

		[HttpPost]
		[Route("UpdateRecipeStep", Name = "UpdateRecipeStep")]
		public async Task<IActionResult> UpdateRecipeStep(RecipeStep rcpstep)
		{
			using (var httpClient = new HttpClient())
			{//http://localhost:36636/api/v1.0/ProductInfo/UpdateProduct/id
				StringContent content = new StringContent(JsonConvert.SerializeObject(rcpstep), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PutAsync($"{baseurl}/UpdateRecipeStep", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("RecipeStepList");
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
