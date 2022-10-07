namespace GardenRover
{
    using FluentValidation;

    public class CustomerValidator : AbstractValidator<Coordinates>
    {
        public CustomerValidator()
        {
            RuleFor(coordinates => coordinates.XAxis).GreaterThan(0);
            RuleFor(coordinates => coordinates.YAxis).GreaterThan(0);
        }
    }
}
