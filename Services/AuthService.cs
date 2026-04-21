using Microsoft.Extensions.Caching.Memory;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;

    private const string CACHE_KEY = "AS400_TOKEN";

    public AuthService(HttpClient http, IMemoryCache cache)
    {
        _http = http;
        _cache = cache;
    }

    public async Task<string> GetToken()
    {
        if (_cache.TryGetValue(CACHE_KEY, out string token))
            return token;

        var response = await _http.PostAsJsonAsync("/auth/login", new
        {
            user = "admin",
            password = "1234"
        });

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<LoginResponse>();
        token = data.Token;

        var expiration = JwtHelper.GetExpiration(token);

        var cacheTime = expiration.AddMinutes(-2);

        _cache.Set(CACHE_KEY, token, cacheTime);

        return token;
    }

    public void ClearToken()
    {
        _cache.Remove(CACHE_KEY);
    }
}