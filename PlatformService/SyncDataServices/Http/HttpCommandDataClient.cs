using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private IConfiguration _config;
        private HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task SendPlatformToCommand(PlatformReadDto paltform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(paltform),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_config["CommandService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService");
            }
            else
            {
                System.Console.WriteLine("--> Sycn POST to CommandService FAILED");
            }
        }
    }
}