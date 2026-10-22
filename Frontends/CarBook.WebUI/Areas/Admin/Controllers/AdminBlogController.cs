using CarBook.Dto.BlogDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminBlog")]
    public class AdminBlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogController(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync("https://localhost:44365/api/Blogs/BlogWithAuthor");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultBlogWithAuthorDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        //[HttpGet]
        //[Route("CreateBlog")]
        //public async Task<IActionResult> CreateBlog()
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage1 = await client.GetAsync("https://localhost:44365/api/Authors");
        //    var JsonData1 = await responseMessage1.Content.ReadAsStringAsync();
        //    var values1 = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(JsonData1);
        //    List<SelectListItem> authorValues = (from x in values1
        //                                         select new SelectListItem
        //                                         {
        //                                             Text = x.AuthorName,
        //                                             Value = x.AuthorId.ToString()
        //                                         }).ToList();
        //    ViewBag.AuthorValues = authorValues;

        //    var responseMessage2 = await client.GetAsync("https://localhost:44365/api/Catergories");
        //    var JsonData2 = await responseMessage2.Content.ReadAsStringAsync();
        //    var values2 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(JsonData2);
        //    List<SelectListItem> categoryValues = (from x in values2
        //                                           select new SelectListItem
        //                                         {
        //                                             Text = x.CategoryName,
        //                                             Value = x.CategoryId.ToString()
        //                                         }).ToList();
        //    ViewBag.CategoryValues = categoryValues;
        //    return View();
        //}

        //[HttpPost]
        //[Route("CreateBlog")]
        //public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var jsonData = JsonConvert.SerializeObject(createBlogDto);
        //    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //    var responseMessage = await client.PostAsync("https://localhost:44365/api/Blogs", content);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        //    }
        //    return View();
        //}

        //[HttpGet]
        //[Route("UpdateBlog/{id}")]
        //public async Task<IActionResult> UpdateBlog(int id)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage1 = await client.GetAsync("https://localhost:44365/api/Authors");
        //    var JsonData1 = await responseMessage1.Content.ReadAsStringAsync();
        //    var values1 = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(JsonData1);
        //    List<SelectListItem> authorValues = (from x in values1
        //                                         select new SelectListItem
        //                                         {
        //                                             Text = x.AuthorName,
        //                                             Value = x.AuthorId.ToString()
        //                                         }).ToList();
        //    ViewBag.AuthorValues = authorValues;

        //    var responseMessage2 = await client.GetAsync("https://localhost:44365/api/Catergories");
        //    var JsonData2 = await responseMessage2.Content.ReadAsStringAsync();
        //    var values2 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(JsonData2);
        //    List<SelectListItem> categoryValues = (from x in values2
        //                                           select new SelectListItem
        //                                           {
        //                                               Text = x.CategoryName,
        //                                               Value = x.CategoryId.ToString()
        //                                           }).ToList();
        //    ViewBag.CategoryValues = categoryValues;

        //    var responseMessage = await client.GetAsync($"https://localhost:44365/api/Blogs/{id}");
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //        var values = JsonConvert.DeserializeObject<UpdateBlogDto>(jsonData);
        //        values.BlogId = id;
        //        return View(values);
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[Route("UpdateBlog/{id}")]
        //public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var jsonData = JsonConvert.SerializeObject(updateBlogDto);
        //    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //    var responseMessage = await client.PutAsync($"https://localhost:44365/api/Blogs/", content);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        //    }
        //    return View();
        //}

        [Route("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44365/api/Blogs/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
            }
            return View();
        }
    }
}
