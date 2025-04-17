
using Microsoft.AspNetCore.Mvc;

[Route("api/travel")]
[ApiController]
public class TravelController : ControllerBase
{
    private readonly ITravelService _travelService;

    public TravelController(ITravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpGet]
    public IActionResult GetTravel()
    {
        var result = _travelService.GetTravelData(); // Fetching data from a service
        return Ok(result);
    }
}
