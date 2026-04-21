


using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/As400")]
public class TestController : ControllerBase
{
    private readonly As400Service _service;
    private readonly string _baseUrl;
    public TestController(As400Service service,IConfiguration config)
    {
        _service = service;
        _baseUrl = config["ExternalApi:server"];
    }

    [HttpPost]
    public async Task<IActionResult> Test(CotizacionDto cotizacion)
    {
        var result = await _service.Cotizar(cotizacion);

        return Ok(result);
    }

    [HttpGet("ping")]
    public async Task<IActionResult> PingServer()
    {
        try
        {
            
            var ping = new Ping();
            var reply = await ping.SendPingAsync(_baseUrl);

            if (reply.Status == IPStatus.Success)
            {
                return Ok(new { status = "OK", time = reply.RoundtripTime });
            }

            return StatusCode(500, new { status = "FAIL" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message, trace=ex.StackTrace,innerEx=ex.InnerException.Message, host= _baseUrl });
        }
    }
    [HttpGet("tcp-check")]
    public async Task<IActionResult> CheckTcp()
    {
        try
        {
            using var client = new TcpClient();

            var task = client.ConnectAsync(_baseUrl,81);
            var timeout = Task.Delay(3000);

            var completed = await Task.WhenAny(task, timeout);

            if (completed == timeout)
                return StatusCode(500, new { status = "timeout" });

            return Ok(new { status = "connected" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}