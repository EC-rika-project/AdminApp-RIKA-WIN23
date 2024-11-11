using AdminApp.ViewModels;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace AdminApp.Controllers
{
    public class UserController : Controller
    {

        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;


        public UserController(IConfiguration configuration)
        {
            _client = new HttpClient { BaseAddress = new Uri("https://userprovider-rika-win23.azurewebsites.net/") };
            _configuration = configuration;
        }


        [Route("users")]
        [ActionName("_users")]

        // Nedan hämtar alla users
        public async Task<IActionResult> GetUsers()
        {

            var items = await _client.GetFromJsonAsync<List<UserProfileDto>>($"/api/account{_configuration["ApiKeys:MyApiKey"]}");

            UserViewModel model = new();
            model.UserList = items;

            return View("Partials/UserPartials/_users", model);

        }

        // Nedan hämtar user mallen

        [Route("user/add")]
        public IActionResult AddUser()
        {
            return View("Partials/UserPartials/_userAdd");
        }

        // Nedan lägger till usern

        [Route("user/add")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {

            if (ModelState.IsValid)
            {

                UserDTO userDto = new();

                if(model.Admin == true)
                {
                    userDto = new()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        SecurityKey = _configuration["ApiKeys:AdminAppKey"]
                    };

                }else
                {
                    userDto = new() 
                    {   Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        SecurityKey = ""
                    };
                }


                var content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");

                var apikey = _configuration["ApiKeys:MyApiKey"];

                var response = await _client.PostAsync($"api/signup{_configuration["ApiKeys:MyApiKey"]}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Account Added";
                    return View("Partials/UserPartials/_userAdded");
                }
                else
                {
                    TempData["successMessage"] = "*Account already exists";
                    TempData["successMessageTwo"] = "*Only admin accounts can be added at this moment";
                    return View("Partials/UserPartials/_userAdd");
                }

               
            }


            return View("Partials/UserPartials/_userAdd", model);


        }


        // Nedan hämtar usern för edit 

        [Route("user/edit/{value}")]
        public async Task<IActionResult> EditUser(string value)
        {

            if(value != null)
            {
                var item = await _client.GetFromJsonAsync<UserProfileDto>($"/api/account/{value.ToLower()}{_configuration["ApiKeys:MyApiKey"]}");


                UserViewModel model = new();

                
                model.FirstName = item!.Profile!.FirstName;
                model.LastName = item.Profile.LastName;
                model.Email = item.Profile.Email;


                return View("Partials/UserPartials/_userUpdate", model);

            }

            return View("Partials/UserPartials/_userSettings");
        }


        // Nedan tar bort usern

        [Route("user/delete/{value}")]
        public async Task<IActionResult> DeleteUser(string value)
        {


            if(value == null)
            {
                var itemss = await _client.GetFromJsonAsync<List<UserProfileDto>>($"/api/account{_configuration["ApiKeys:MyApiKey"]}");

                UserViewModel models = new();
                models.UserList = itemss;

                return View("Partials/UserPartials/_users", models);
            }


            var response = await _client.DeleteAsync($"api/account{_configuration["ApiKeys:MyApiKey"]}&id={value}");


            if (response.IsSuccessStatusCode)
            {
                return View("Partials/UserPartials/_userAdded");
            }


            var items = await _client.GetFromJsonAsync<List<UserProfileDto>>($"/api/account{_configuration["ApiKeys:MyApiKey"]}");

            UserViewModel model = new();
            model.UserList = items;

            return View("Partials/UserPartials/_users", model);
        }



    }
}
