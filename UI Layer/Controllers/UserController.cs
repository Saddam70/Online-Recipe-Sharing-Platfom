using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
	[Route("User")]
	public class UserController : Controller
	{
		private readonly IConfiguration _configurtion;
		string baseurl = string.Empty;
		public UserController(IConfiguration configurtion)
		{
			this._configurtion = configurtion;
			baseurl = _configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:44301/api/
			baseurl += "User";//http://localhost:44301/api/User/

		}
	  //[Route("/")]
		[HttpGet]
		[Route("Userlist", Name = "UserList")]
		public async Task<IActionResult> UserList()
		{
			List<User> users = new List<User>();
			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetAllUser"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						users = JsonConvert.DeserializeObject<List<User>>(apiresponse);
					}
				}
			}
			return View(users);
		}
		[HttpGet]
		[Route("GetByUserId", Name = "GetByUserId")]
		public async Task<IActionResult> GetByUserId(int id)
		{
			User users = new User();
			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetUserById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						users = JsonConvert.DeserializeObject<User>(apiresponse);
					}
				}
			}
			return View(users);
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("RegisterUser", Name = "RegisterUser")]
		public IActionResult RegisterUser()
		{
			return View();
		}
		//RegisterUser
		[HttpPost]
		[Route("SaveUser", Name = "SaveUser")]
		public async Task<IActionResult> RegisterUser(User user)
		{
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
				//issue with in this line
				using (var response = await httpClient.PostAsync($"{baseurl}/RegisterUser", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("LoginPage");
					}
					else
					{
						return BadRequest();
					}
				}
			}
		}
		[HttpGet]
		[Route("DeleteUser/{id}", Name = "DeleteUser")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			#region Get User By Id
			User users = new User();
			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetUserById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						users = JsonConvert.DeserializeObject<User>(apiresponse);
					}
				}
			}


			#endregion
			return View(users);
		}

		[HttpPost]
		[Route("RemoveUser", Name = "RemoveUser")]
		public async Task<IActionResult> DeleteUser(User user)
		{

			using (var httpClient = new HttpClient())
			{//http://localhost:36636/api/v1.0/ProductInfo/DeleteProduct/id
				using (var response = await httpClient.DeleteAsync($"{baseurl}/DeleteUser/{user.UserId}"))
				{
					if (response.IsSuccessStatusCode) //200-299
					{
						return RedirectToRoute("UserList");
					}
				}
			}
			return View(user);

		}
		[HttpGet]
		[Route("EditUser/{id}", Name = "EditUser")]
		public async Task<IActionResult> UpdateUser(int id)
		{
			#region Get User By Id
			User users = new User();
			using (var httpclient = new HttpClient())
			{
				//http://localhost:36636/api/v1.0/ProductInfo/GetAll
				using (var response = await httpclient.GetAsync($"{baseurl}/GetUserById/{id}"))
				{
					if (response.IsSuccessStatusCode)
					{
						string apiresponse = await response.Content.ReadAsStringAsync();
						//convet json to list<productinfo>
						users = JsonConvert.DeserializeObject<User>(apiresponse);
					}
				}
			}
			#endregion
			return View(users);
		}

		[HttpPost]
		[Route("UpdateUser", Name ="UpdateUser")]
		public async Task<IActionResult> UpdateUser(User user)
		{
			using (var httpClient = new HttpClient())
			{//http://localhost:36636/api/v1.0/ProductInfo/UpdateProduct/id
				StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PutAsync($"{baseurl}/UpdateUser", content))
				{
					if (response.IsSuccessStatusCode)
					{
						return RedirectToRoute("UserList");
					}
					else
					{
						return BadRequest(response);
					}
				}
			}
			//return View(user);
		}
		//[Route("/")]
		[HttpGet]
		[Route("UserLoginPage", Name = "UserLoginPage")]
		public IActionResult UserLogin()
		{
			return View();  // Return login view for the user
		}

		[HttpPost]
		[Route("UserLogin", Name = "UserLogin")]
		public async Task<IActionResult> UserLogin(Users userInfo)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

				// Sending the user data to the validation endpoint (for example, /ValidateUser)
				using (var response = await httpClient.PostAsync($"{baseurl}/ValidateUser", content))
				{
					if (response.IsSuccessStatusCode)
					{
						string token = await response.Content.ReadAsStringAsync(); // Get the token from the response
						HttpContext.Session.SetString("token", token);  // Store the token in the session
						return RedirectToRoute("RecipeList"); // Redirect to the dashboard or any authenticated route
					}
					else
					{
						// If login fails, reload the login page with an error message
						ViewBag.Error = "Invalid login credentials. Please try again.";
						return View();
					}
				}
			}
		}
		[Route("Abouts", Name = "Abouts")]
		public IActionResult Aboutus()
		{
			return View();
		}
		[Route("Contacts", Name = "Contacts")]
		public IActionResult Contactus()
		{
			return View();
		}

	}
}

