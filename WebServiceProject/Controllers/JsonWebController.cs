using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;

namespace WebServiceProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonWebController : ControllerBase
    {
        private readonly ILogger<JsonWebController> logger;
        private readonly HttpClient httpClient;

        public JsonWebController(ILogger<JsonWebController> logger)
        {
            this.logger = logger;
            this.httpClient = new();
        }

        [HttpPost]
        public async Task<IActionResult> DownloadAndSaveData([FromBody] string apiUrl, [FromBody] string method) 
        {
            apiUrl = "https://example.com/api/data";

            using (var response = await httpClient.GetAsync(apiUrl))
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
