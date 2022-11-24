using FluentValidation;

namespace ParkingMetersCA.Application.ParkingMeters.Queries.GetParkingMeters;
public class GetPrarkingMetersWithPaginationQueryValidator : AbstractValidator<GetPrarkingMetersWithPaginationQuery>
{
    public GetPrarkingMetersWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.")
            .LessThanOrEqualTo(500).WithMessage("PageSize must be less than 500.");
    }
}
