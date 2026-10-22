using CarBook.Dto.CarDescriptionDtos;
using CarBook.Dto.CarFeatureDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailDescriptionComponentPartialView : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailDescriptionComponentPartialView(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://carbookparanova-aad2bfcmesgkfwaq.germanywestcentral-01.azurewebsites.net/api/CarDescription/GetCarDescriptionByCarId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<CarDescriptionDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
