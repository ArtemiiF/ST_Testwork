using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ST_Testwork.Interfaces;

namespace ST_Testwork.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportService airportService;
        public AirportDistanceController(IAirportService airportService)
        {
            this.airportService = airportService;
        }

        [HttpGet("get-distance")]
        public async Task<IActionResult> Get([FromQuery] string firstAirport, [FromQuery] string secondAirport)
        {
            return Ok(await airportService.GetDistanceBetweenTwoAirportsInMilesAsync(firstAirport, secondAirport));
        }
    }
}
