using CarBook.Dto.CarPricingDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace CarBook.WebUI.ViewComponents.CarViewComponents
{
    public class _CarDetailRelatedCarComponentPartialView : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailRelatedCarComponentPartialView(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://carbookparanova-aad2bfcmesgkfwaq.germanywestcentral-01.azurewebsites.net/api/CarPricings/CarPricingWithPerod");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarPricingWithCarDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
