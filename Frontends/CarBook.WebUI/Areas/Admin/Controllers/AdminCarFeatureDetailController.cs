using CarBook.Dto.AboutDtos;
using CarBook.Dto.CarDtos;
using CarBook.Dto.CarFeatureDtos;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminCarFeatureDetail")]
    public class AdminCarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:44365/api/CarFeatures/GetCarFeatureByCarId/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarFeatureDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(List<UpdateCarFeatureDto> updateCarFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            foreach (var item in updateCarFeatureDto)
            {
                if (item.CarFeatureAvaileble)
                {
                    await client.GetAsync("https://localhost:44365/api/CarFeatures/ChangeCarFeatureAvilebleToTrue?id=" + item.CarFeatureId);
                }
                else
                {
                    await client.GetAsync("https://localhost:44365/api/CarFeatures/ChangeCarFeatureAvilebleToFalse?id=" + item.CarFeatureId);
                }
            }
            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
        }
    }
}