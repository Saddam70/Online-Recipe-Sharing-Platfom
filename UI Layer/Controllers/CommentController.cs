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
	[Route("Comment")]
	public class CommentController : Controller
	{
		private readonly IConfiguration _configurtion;
		string baseurl = string.Empty;
		public CommentController(IConfiguration configurtion)
		{
			this._configurtion = configurtion;
			baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
			baseurl += "Comment";//http://localhost:36636/api/v1.0/ProductInfo

		}
		//[Route("/")]
		[Route("CommentList", Name = "CommentList")]
		public async Task<IActionResult> CommentList()
		{
			List<Comment> cmt = new List<Comment>();

			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetAllComments"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						cmt = JsonConvert.DeserializeObject<List<Comment>>(apiresponse);
					}
				}
			}
			return View(cmt);
		}

		[HttpGet]
		[Route("AddComment", Name = "AddComment")]
		public IActionResult AddComment()
		{
			return View();
		}

		[HttpPost]
		[Route("SaveComment", Name = "SaveComment")]
		public async Task<IActionResult> SaveComment(Comment cmt)
		{
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(cmt), Encoding.UTF8, "application/json");
				//http://localhost:36636/api/v1.0/ProductInfo/SaveProduct
				using (var response = await httpClient.PostAsync($"{baseurl}/AddComment", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("CommentList");
					}
					else
					{
						return BadRequest(response);
					}
				}
			}
		}

		[HttpGet]
		[Route("GetCommentById/{id}", Name = "GetCommentById")]
		public async Task<IActionResult> GetCommentById(int id)
		{
			Comment comment = new Comment();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetCommentById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						comment = JsonConvert.DeserializeObject<Comment>(apiresponse);
					}
				}
			}

			return View(comment);
		}
		[HttpGet]
		[Route("DeleteComment/{id}", Name = "DeleteComment")]
		public async Task<IActionResult> DeleteComment(int id)
		{
			#region Get Comment By Id
			Comment comment = new Comment();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetCommentById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						comment = JsonConvert.DeserializeObject<Comment>(apiresponse);
					}
				}
			}

			

			#endregion
			return View(comment);
		}
		[HttpPost]
		[Route("DeleteComments/{id}", Name = "DeleteComments")]
		public async Task<IActionResult> DeleteComment(Comment cmt)
		{
			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.DeleteAsync($"{baseurl}/DeleteComment/{cmt.CommentId}"))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("CommentList");
					}
					else
					{
						return View("Error");
					}
				}
			}
		}
		[HttpGet]
		[Route("EditComment/{id}", Name = "EditComment")]
		public async Task<IActionResult> EditComment(int id)
		{
			#region Get Comment By Id
			Comment comment = new Comment();

			using (var httpclient = new HttpClient())
			{
				using (var response = await httpclient.GetAsync($"{baseurl}/GetCommentById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						comment = JsonConvert.DeserializeObject<Comment>(apiresponse);
					}
				}
			}



			#endregion
			return View(comment);
		}
		[HttpPost]
		[Route("UpdateComment", Name = "UpdateComment")]
		public async Task<IActionResult> UpdateComment(Comment comment)
		{
			using (var httpclient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
				using (var response = await httpclient.PutAsync($"{baseurl}/UpdateComment", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("CommentList");
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
