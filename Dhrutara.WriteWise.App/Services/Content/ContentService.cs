using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dhrutara.WriteWise.App.Services.Content
{
    public class ContentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public ContentService(AuthService authService, HttpClient httpClient)
        {
            _authService = authService;
            _httpClient = httpClient;
        }

        public async Task<string[]> GetContentAsync(ApiRequest request, CancellationToken cancellationToken)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = true,
            };

            UserContext? user = await _authService.SigninAsync(true, cancellationToken).ConfigureAwait(false);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", $"Bearer {user?.AccessToken}");

            HttpResponseMessage apiResponse = await _httpClient
                .PostAsJsonAsync("getcontent", request, options, cancellationToken)
                .ConfigureAwait(false);

            if (apiResponse.IsSuccessStatusCode)
            {
                Stream resultContent = await apiResponse
                    .Content
                    .ReadAsStreamAsync(cancellationToken)
                    .ConfigureAwait(false);

                ApiResponse? response = await JsonSerializer
                    .DeserializeAsync<ApiResponse>(resultContent,options, cancellationToken)
                    .ConfigureAwait(false);

                return response?.Content??Array.Empty<string>();
            }

            return Array.Empty<string>();
        }

        private class LowerCaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name) =>
                name.ToLower();
        }
    }
}