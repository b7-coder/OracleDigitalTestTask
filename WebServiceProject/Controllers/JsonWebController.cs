using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;

namespace WebServiceProject.Controllers
{
    public class JsonWebController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;

        public JsonWebController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _httpClient = new();
        }

        [HttpPost]
        public async Task<IActionResult> DownloadAndSaveData() 
        {
            string apiUrl = "https://example.com/api/data"; // Замените на URL API для скачивания JSON данных.

            using (var response = await _httpClient.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var jsonContent = await response.Content.ReadAsStringAsync();
                    //var jsonData = JsonSerializer.Deserialize<JsonData>(jsonContent);

                    //// Сохранение в БД
                    //_dbContext.JsonData.Add(jsonData);
                    //await _dbContext.SaveChangesAsync();

                    return Ok("Data downloaded and saved successfully.");
                }

                return BadRequest("Failed to download data from the API.");
            }
        }
    }
}
