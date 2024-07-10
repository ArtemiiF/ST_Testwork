using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ST_Testwork.Interfaces;
using ST_Testwork.Models;

namespace ST_Testwork.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportService airportService;
        private readonly IValidator<AirportsIATACodes> airportsIATACodesValidator;

        public AirportDistanceController(
            IAirportService airportService,
            IValidator<AirportsIATACodes> airportsIATACodesValidator
            )
        {
            this.airportsIATACodesValidator = airportsIATACodesValidator;
            this.airportService = airportService;
        }

        [HttpGet("get-distance")]
        public async Task<IActionResult> Get([FromQuery] AirportsIATACodes airportsIATACodes)
        {
            var validationrResult = await airportsIATACodesValidator.ValidateAsync(airportsIATACodes);
            if (!validationrResult.IsValid)
            { 
                return BadRequest(validationrResult.Errors);
            }
            return Ok(await airportService.GetDistanceBetweenTwoAirportsInMilesAsync(airportsIATACodes.FirstAirportCode, airportsIATACodes.SecondAirportCode));
        }
    }
}
