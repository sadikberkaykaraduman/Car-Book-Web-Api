using CarBook.Dto.LocatiolDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarBook.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44365/api/Locations");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
            List<SelectListItem> locationValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.LocationName,
                                                       Value = x.LocationId.ToString()
                                                   }).ToList();
            ViewBag.LocationValues = locationValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string locationId)
        {
            TempData["LocationId"] = locationId;
            return RedirectToAction("Index", "RentACarList");
        }
    }
}
