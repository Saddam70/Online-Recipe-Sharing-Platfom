using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI_Layer.Models;

namespace UI_Layer.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
       
        private readonly IConfiguration _configurtion;
        //string baseurl = string.Empty;
        private string baseurl;
        public AdminController(IConfiguration configurtion)
        {
            this._configurtion = configurtion;
            baseurl = this._configurtion.GetValue<string>("WebAPIbaseURL");//http://localhost:36636/api/
            baseurl += "Admin";//http://localhost:36636/api/v1.0/ProductInfo

        }
        [HttpGet]
        [Route("/")]
        [Route("LoginPage", Name = "LoginPage")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login", Name = "Login")]
        public async Task<IActionResult> Login(Admin adminInfo)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(adminInfo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseurl}/ValidateAdmin", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        HttpContext.Session.SetString("token", token);
                        switch (adminInfo.Role)
                        {
                            case "Admin":
                                return RedirectToRoute("UserList");
                            case "Chef":
                                return RedirectToRoute("RecipeList");
                            case "User":
                                return RedirectToRoute("Dashboard");
                            default:
                                return RedirectToRoute("LoginPage"); // Redirect to login page if role is unknown
                        }
                    }
                    else
                    {
						ModelState.AddModelError("", "Invalid login attempt");
						return View();
                        
                    }
                }
                //// Validate User if Admin validation fails
     //           StringContent userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
     //           using (var userResponse = await httpClient.PostAsync($"{baseurl}/ValidateUser", userContent))
     //           {
     //               if (userResponse.IsSuccessStatusCode)
     //               {
     //                   string userToken = await userResponse.Content.ReadAsStringAsync();
     //                   HttpContext.Session.SetString("token", userToken);
     //                   return RedirectToRoute("RecipeList");
     //               }
					//else
					//{
					//	ModelState.AddModelError("", "Invalid login attempt");
					//	return View();
					//	//return Content("Error");
					//}
				}
         
        }
        [HttpGet]
        [Route("Dashboard",Name ="Dashboard")]
        public IActionResult Dashboard()
        {
                // You can pass the user data to the view using ViewBag or a model
                ViewBag.Username = User.Identity.Name;
                return View();
        }
        [Route("LogOut", Name = "LogOut")]
        public IActionResult LogOut()
        {
            return RedirectToRoute("LoginPage");
        }
        [Route("About", Name = "About")]
        public IActionResult Aboutus()
        {
            return View();
        }
        [Route("Contact", Name = "Contact")]
        public IActionResult Contactus()
        {
            return View();
        }
	
		[HttpPost]
        [Route("Saveresponse", Name = "Saveresponse")]
        public IActionResult Saveresponse()
        {
            return View();
        }
		[Route("facebook", Name = "facebook")]
		public IActionResult Facebook()
		{
			return Redirect("https://www.facebook.com/login.php/");
		}
		[Route("gmail", Name = "gmail")]
		public IActionResult Google()
		{
			return Redirect("https://accounts.google.com/v3/signin/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2Fu%2F0%2F&emr=1&followup=https%3A%2F%2Fmail.google.com%2Fmail%2Fu%2F0%2F&ifkv=Ab5oB3ojxeXrsf_Y9C0zbnsMW-ia-N5vB09lHyBH-0ElKX9MayQ9rf8JkHlLeyAdinvDN99BwARfcw&osid=1&passive=1209600&service=mail&flowName=GlifWebSignIn&flowEntry=ServiceLogin&dsh=S758407908%3A1725722123773415&ddm=0");
		}
	}
 }

