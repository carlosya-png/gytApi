using System.Net;
using System.Net.Http.Headers;

public class As400Service
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public As400Service(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<object> Cotizar(CotizacionDto request)
    {
        try{
        var token = await _auth.GetToken();

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.PostAsJsonAsync(
            "/api/cotizar-consultasaldo-as400",
            request
        );

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _auth.ClearToken();

            token = await _auth.GetToken();

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            response = await _http.PostAsJsonAsync(
                "/api/cotizar-consultasaldo-as400",
                request
            );
        }

        response.EnsureSuccessStatusCode();
        var resp= await response.Content.ReadFromJsonAsync<CotizacionResponse>();
        decimal saldo = Convert.ToDecimal(resp.Data.PrSaldo, System.Globalization.CultureInfo.InvariantCulture);
        resp.Data.PrSaldo=saldo.ToString();
        return resp;
        }
        catch(Exception e)
        {
             return new
                {
                    error = true,
                    message = e.Message,
                    trace = e.StackTrace,
                    detail = e.InnerException?.Message,
                    url= _http.BaseAddress
                };
        }
    }
}