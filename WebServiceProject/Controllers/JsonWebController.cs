using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebServiceDomain.Services;
using WebServiceProject.Models;

namespace WebServiceProject.Controllers
{
    [ApiController]
    [Route("api/")]
    public class JsonWebController : ControllerBase
    {
        private readonly IDataService dataService;
        private readonly ILogger<JsonWebController> logger;
        private readonly HttpClient httpClient;

        public JsonWebController(ILogger<JsonWebController> logger, IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
            httpClient = new();
        }

        [HttpPost]
        [Route("post/")]
        public async Task<IActionResult> DownloadAndSaveData([FromBody] RequestModel requestModel)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(requestModel.apiUrl, UriKind.Absolute))
                {
                    return BadRequest("Wrong link");
                }

                using (var response = await httpClient.GetAsync(requestModel.apiUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = await response.Content.ReadAsStringAsync();

                        using (JsonDocument document = JsonDocument.Parse(jsonContent))
                        {
                            JsonElement elements = document.RootElement;
                            await SearchJsonValues(elements, requestModel.searchJsonKey);
                        }

                        return Ok("Data downloaded and saved successfully.");
                    }
                }
                return BadRequest("Failed to download data from the API.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("Failed to download or save data from the API.");
            }
        }

        private async Task SearchJsonValues(JsonElement elements, string searchKey, bool isKeyElements = false) 
        {
            // ищет нужные ключи и значения
            if (elements.ValueKind == JsonValueKind.Array)
            {
                // Обходим элементы массива
                foreach (JsonElement element in elements.EnumerateArray())
                {
                    if (element.ValueKind == JsonValueKind.Array || element.ValueKind == JsonValueKind.Object)
                    {
                        SearchJsonValues(element, searchKey); // обратно прогоняем рекурсивно
                    }
                    else if(isKeyElements)
                    {
                        if(!await dataService.Create(searchKey, element.ToString()))
                        {
                            throw new Exception("Ошибка сохранения");
                        }
                    }
                }
            }
            else if (elements.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in elements.EnumerateObject())
                {
                    string propertyName = property.Name;
                    JsonElement propertyValue = property.Value;

                    if (propertyValue.ValueKind == JsonValueKind.Array || propertyValue.ValueKind == JsonValueKind.Object)
                    {
                        if (propertyName == searchKey) 
                        {
                            // обратно прогоняем рекурсивно, но уже сохраняем  
                            SearchJsonValues(propertyValue, searchKey, true);
                        }
                        else
                        {
                            // обратно прогоняем рекурсивно
                            SearchJsonValues(propertyValue, searchKey);

                        }
                    }
                    else if(isKeyElements || propertyName== searchKey)
                    {
                        if(!await dataService.Create(searchKey, propertyValue.ToString()))
                        {
                            throw new Exception("Ошибка сохранения");
                        }
                    }
                }
            }
            else if(isKeyElements)
            {
                if (!await dataService.Create(searchKey, elements.ToString()))
                {
                    throw new Exception("Ошибка сохранения");

                }
            }
        }
    }
}
