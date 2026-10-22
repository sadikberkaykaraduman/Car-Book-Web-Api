using CarBook.Dto.CarDtos;
using CarBook.Dto.CarReviewDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailReviewComponentPartialView : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailReviewComponentPartialView(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://carbookparanova-aad2bfcmesgkfwaq.germanywestcentral-01.azurewebsites.net/api/CarReviews/GetCarReviewWithCar/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarReviewDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
