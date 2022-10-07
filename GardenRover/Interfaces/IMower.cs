namespace GardenRover.Interfaces
{
    using System.Collections.Generic;

    public interface IMower
    {
        public ICoordinates Location { get; set; }
        public Facing Heading { get; set; }

        public void Command(IReadOnlyList<string> orders);

        public string CurrentPosition();
    }
}