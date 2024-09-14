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
	[Route("Rating")]
	public class RatingController : Controller
	{
		private readonly IConfiguration _configurtion;
		string baseurl = string.Empty;
		public RatingController(IConfiguration configurtion)
		{
			this._configurtion = configurtion;
			baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
			baseurl += "Rating";//http://localhost:36636/api/v1.0/ProductInfo

		}
		//[Route("/")]
		[Route("RatingList", Name = "RatingList")]
		public async Task<IActionResult> RatingList()
		{
			List<Rating> rate = new List<Rating>();

			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetAllRatings"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						rate = JsonConvert.DeserializeObject<List<Rating>>(apiresponse);
					}
				}
			}
			return View(rate);
		}

		[HttpGet]
		[Route("AddRating", Name = "AddRating")]
		public IActionResult AddRating()
		{
			return View();
		}

		[HttpPost]
		[Route("SaveRating", Name = "SaveRating")]
		public async Task<IActionResult> SaveRating(Rating rate)
		{
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(rate), Encoding.UTF8, "application/json");
				//http://localhost:36636/api/v1.0/ProductInfo/SaveProduct
				using (var response = await httpClient.PostAsync($"{baseurl}/AddRating", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("RatingList");
						//return RedirectToRoute("CommentList");
					}
					else
					{
						return BadRequest(response);
					}
				}
			}
		}

		[HttpGet]
		[Route("GetRatingById/{id}", Name = "GetRatingtById")]
		public async Task<IActionResult> GetRatingById(int id)
		{
			Rating rating = new Rating();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetRatingById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						rating = JsonConvert.DeserializeObject<Rating>(apiresponse);
					}
				}
			}

			return View(rating);
		}
		[HttpGet]
		[Route("DeleteRating/{id}", Name = "DeleteRating")]
		public async Task<IActionResult> DeleteRating(int id)
		{
			#region Get Rating By Id
			Rating rating = new Rating();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetRatingById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						rating = JsonConvert.DeserializeObject<Rating>(apiresponse);
					}
				}
			}


			#endregion
			return View(rating);
		}
		[HttpPost]
		[Route("DeleteRatings/{id}", Name = "DeleteRatings")]
		public async Task<IActionResult> DeleteRating(Rating rate
			)
		{
			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.DeleteAsync($"{baseurl}/DeleteRating/{rate.RatingId}"))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("RatingList");
					}
					else
					{
						return View("Error");
					}
				}
			}
		}
		[HttpGet]
		[Route("EditRating/{id}", Name = "EditRating")]
		public async Task<IActionResult> EditRating(int id)
		{
			#region Get Rating By Id
			Rating rating = new Rating();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetRatingById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						rating = JsonConvert.DeserializeObject<Rating>(apiresponse);
					}
				}
			}


			#endregion
			return View(rating);
		}
		[HttpPost]
		[Route("UpdateRating", Name = "UpdateRating")]
		public async Task<IActionResult> UpdateRating(Rating rate)
		{
			using (var httpclient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(rate), Encoding.UTF8, "application/json");
				using (var response = await httpclient.PutAsync($"{baseurl}/UpdateRating", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("RatingList");
						//return RedirectToAction("CommentList");
					}
					else
					{
						return BadRequest("Error");
						//return View("EditComment", comment);
					}
				}
			}
		}
	}
}
