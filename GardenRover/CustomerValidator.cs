namespace GardenRover
{
    using FluentValidation;

    public class CustomerValidator : AbstractValidator<Coordinates>
    {
        public CustomerValidator()
        {
            RuleFor(coordinates => coordinates.XAxis).GreaterThanOrEqualTo(0);
            RuleFor(coordinates => coordinates.YAxis).GreaterThanOrEqualTo(0);
        }
    }
}
