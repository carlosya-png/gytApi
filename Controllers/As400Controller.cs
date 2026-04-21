


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/As400")]
public class TestController : ControllerBase
{
    private readonly As400Service _service;

    public TestController(As400Service service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Test(CotizacionDto cotizacion)
    {
       

        var result = await _service.Cotizar(cotizacion);

        return Ok(result);
    }
}