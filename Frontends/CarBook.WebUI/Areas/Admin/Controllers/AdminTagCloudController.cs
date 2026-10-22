using CarBook.Dto.AuthorDtos;
using CarBook.Dto.BlogDtos;
using CarBook.Dto.BrandDtos;
using CarBook.Dto.TagCloudDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminTagCloud")]
    public class AdminTagCloudController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminTagCloudController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:44365/api/TagClouds");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultTagCloudDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreateTagCloud")]
        public async Task<IActionResult> CreateTagCloud()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44365/api/Blogs");
            var JsonData = await responseMessage.Content.ReadAsStringAsync();
            var valuues = JsonConvert.DeserializeObject<List<ResultBlogDto>>(JsonData);
            List<SelectListItem> blogValues = (from x in valuues
                                               select new SelectListItem
                                               {
                                                   Text = x.BlogTitle,
                                                   Value = x.BlogId.ToString()
                                               }).ToList();
            ViewBag.BlogValues = blogValues;
            return View();
        }

        [HttpPost]
        [Route("CreateTagCloud")]
        public async Task<IActionResult> CreateTagCloud(CreateTagCloudDto createTagCloudDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTagCloudDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44365/api/TagClouds", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTagCloud", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        [Route("UpdateTagCloud/{id}")]
        public async Task<IActionResult> UpdateTagCloud(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage1 = await client.GetAsync("https://localhost:44365/api/Blogs");
            var JsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultBlogDto>>(JsonData1);
            List<SelectListItem> blogValues = (from x in values1
                                               select new SelectListItem
                                               {
                                                   Text = x.BlogTitle,
                                                   Value = x.BlogId.ToString()
                                               }).ToList();
            ViewBag.BlogValues = blogValues;

            var responseMessage = await client.GetAsync($"https://localhost:44365/api/TagClouds/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateTagCloudDto>(jsonData);
                values.TagCloudId = id;
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateTagCloud/{id}")]
        public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudDto updateTagCloudDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateTagCloudDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:44365/api/TagClouds/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTagCloud", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteTagCloud/{id}")]
        public async Task<IActionResult> DeleteTagCloud(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44365/api/TagClouds/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTagCloud", new { area = "Admin" });
            }
            return View();
        }
    }
}
