using FluentValidation;

namespace ParkingMetersCA.Application.ParkingMeters.Commands.CreateParkingMeter;
public class CreateParkingMeterValidator : AbstractValidator<CreateParkingMeterCommand>
{
    public CreateParkingMeterValidator()
    {
        RuleFor(v => v.Address)
            .NotEmpty();
    }
}