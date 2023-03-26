using Dhrutara.WriteWise.App.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dhrutara.WriteWise.App.Services.Content
{
    public class ContentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly LocalContentProvider _localContent;
        public ContentService(AuthService authService, HttpClient httpClient, LocalContentProvider localContent)
        {
            _authService = authService;
            _httpClient = httpClient;
            _localContent = localContent;

            SupportedContentTypes =  _localContent.SupportedContentTypes;
            SupportedContentCategories = _localContent.SupportedContentCategories;
            SupportedRelationships = _localContent.SupportedRelationships;
        }

        public IEnumerable<ContentType> SupportedContentTypes { get; init; }
        public IEnumerable<ContentCategory> SupportedContentCategories { get; init; }
        public IEnumerable<Relationship> SupportedRelationships { get; init; }

        public async Task<string[]> GetContentAsync(ApiRequest request, CancellationToken cancellationToken)
        {
            UserContext? user = await _authService.SigninAsync(false, cancellationToken).ConfigureAwait(false);
            if(user != null) {
                return await GetContentFromServerAsync(request, user, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return GetLocalContentAsync(request);
            }
        }

        private async Task<string[]> GetContentFromServerAsync(ApiRequest request, UserContext user, CancellationToken cancellationToken)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = true,
            };

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
                    .DeserializeAsync<ApiResponse>(resultContent, options, cancellationToken)
                    .ConfigureAwait(false);

                return response?.Content ?? Array.Empty<string>();
            }

            return Array.Empty<string>();
        }

        private string[] GetLocalContentAsync(ApiRequest request)
        {
            return _localContent.GetContent(request.Type, request.Category, request.To);
        }

        private class LowerCaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name) =>
                name.ToLower();
        }
    }
}