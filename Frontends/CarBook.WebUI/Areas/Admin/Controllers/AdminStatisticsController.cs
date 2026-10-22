using CarBook.Dto.AuthorDtos;
using CarBook.Dto.StatisticsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminStatistics")]
    public class AdminStatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStatisticsController(IHttpClientFactory httpClientFactory)
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
                #region Statistic1
                var responseMessage1 = await client.GetAsync("https://localhost:44365/api/Statistics/GetCarCount");
                if (responseMessage1.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage1.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.CarCount = values.CarCount;
                }
                #endregion

                #region Statistic2
                var responseMessage2 = await client.GetAsync("https://localhost:44365/api/Statistics/GetLocationCount");
                if (responseMessage2.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage2.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.LocationCount = values.LocationCount;
                }
                #endregion

                #region Statistic3
                var responseMessage3 = await client.GetAsync("https://localhost:44365/api/Statistics/GetAuthorCount");
                if (responseMessage3.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage3.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.AuthorCount = values.AuthorCount;
                }
                #endregion

                #region Statistic4
                var responseMessage4 = await client.GetAsync("https://localhost:44365/api/Statistics/GetBlogCount");
                if (responseMessage4.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage4.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.BlogCount = values.BlogCount;
                }
                #endregion

                #region Statistic5
                var responseMessage5 = await client.GetAsync("https://localhost:44365/api/Statistics/GetBrandCount");
                if (responseMessage5.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage5.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.BrandCount = values.BrandCount;
                }
                #endregion

                #region Statistic6
                var responseMessage6 = await client.GetAsync("https://localhost:44365/api/Statistics/GetGasolineCarCount");
                if (responseMessage6.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage6.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.GasolineCarCount = values.GasolineCarCount;
                }
                #endregion

                #region Statistic7
                var responseMessage7 = await client.GetAsync("https://localhost:44365/api/Statistics/GetDieselCarCount");
                if (responseMessage7.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage7.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.DieselCarCount = values.DieselCarCount;
                }
                #endregion

                #region Statistic8
                var responseMessage8 = await client.GetAsync("https://localhost:44365/api/Statistics/GetHybridCarCount");
                if (responseMessage8.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage8.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.HybridCarCount = values.HybridCarCount;
                }
                #endregion

                #region Statistic9
                var responseMessage9 = await client.GetAsync("https://localhost:44365/api/Statistics/GetLowKilometterCarCount");
                if (responseMessage9.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage9.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.LowKilometerCarCount = values.LowKilometerCarCount;
                }
                #endregion

                #region Statistic10
                var responseMessage10 = await client.GetAsync("https://localhost:44365/api/Statistics/GetAutoTransmissionCarCount");
                if (responseMessage10.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage10.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.AutoTransmissionCarCount = values.AutoTransmissionCarCount;
                }
                #endregion

                #region Statistic11
                var responseMessage11 = await client.GetAsync("https://localhost:44365/api/Statistics/GetAvgRentPriceDaily");
                if (responseMessage11.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage11.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.AvgPriceDaily = values.AvgPriceDaily;
                }
                #endregion

                #region Statistic12
                var responseMessage12 = await client.GetAsync("https://localhost:44365/api/Statistics/GetAvgRentPriceWeekly");
                if (responseMessage12.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage12.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.AvgPriceWeekly = values.AvgPriceWeekly;
                }
                #endregion

                #region Statistic13
                var responseMessage13 = await client.GetAsync("https://localhost:44365/api/Statistics/GetAvgRentPriceMonthly");
                if (responseMessage13.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage13.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.AvgPriceMonthly = values.AvgPriceMonthly;
                }
                #endregion

                #region Statistic14
                var responseMessage14 = await client.GetAsync("https://localhost:44365/api/Statistics/GetBrandMostCars");
                if (responseMessage14.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage14.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.BrandMostCar = values.CommonBrand;
                }
                #endregion

                #region Statistic15
                var responseMessage15 = await client.GetAsync("https://localhost:44365/api/Statistics/GetMostCommentedBlogTitle");
                if (responseMessage15.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage15.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.MostCommentedBlog = values.BlogTitle;
                }
                #endregion

                #region Statistic16
                var responseMessage16 = await client.GetAsync("https://localhost:44365/api/Statistics/GetCheapestCarModel");
                if (responseMessage16.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage16.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.CheapestCarModel = values.CarCheapestModel;
                }
                #endregion

                #region Statistic17
                var responseMessage17 = await client.GetAsync("https://localhost:44365/api/Statistics/GetMostExpensiveCarModel");
                if (responseMessage17.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage17.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                    ViewBag.MostExpensiveCarModel = values.CarExpensiveModel;
                }
                #endregion
            }
            return View();
        }
    }
}
