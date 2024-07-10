using FluentValidation;
using ST_Testwork.Models;

namespace ST_Testwork.Validators
{
    public class AirportIATACodeValidator: AbstractValidator<AirportsIATACodes>
    {
        public AirportIATACodeValidator()
        {
            RuleFor(x => x.FirstAirportCode).NotNull()
                .WithMessage("IATA FirstAirportCode cannot be null")
                .Length(3, 3)
                .WithMessage("IATA FirstAirportCode must be 3 character")
                .Matches(@"^[A-Z]+$")
                .WithMessage("IATA FirstAirportCode accepts only capital letters");

            RuleFor(x => x.SecondAirportCode).NotNull()
                .WithMessage("IATA SecondAirportCode cannot be null")
                .Length(3, 3)
                .WithMessage("IATA SecondAirportCode must be 3 character")
                .Matches(@"^[A-Z]+$")
                .WithMessage("IATA SecondAirportCode accepts only capital letters");
        }
    }
}
