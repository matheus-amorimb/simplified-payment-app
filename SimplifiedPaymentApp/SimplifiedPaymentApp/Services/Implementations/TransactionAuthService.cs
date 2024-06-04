using System.Text.Json;
using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Services;

public class TransactionAuthService : ITransactionAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TransactionAuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> IsAuthorized()
    {
        var httpClient = _httpClientFactory.CreateClient("TransactionAuthService");
        var response = await httpClient.GetAsync("authorize");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            var authorizeResponse = JsonSerializer.Deserialize<TransactionAuthResponseDto>(responseContent);
            return authorizeResponse.data.Authorization;
        }
        else
        {
            var errorMessage = $"Authorization failed with status code: {(int)response.StatusCode}";
            throw new HttpRequestException(errorMessage);
        }
    }
}