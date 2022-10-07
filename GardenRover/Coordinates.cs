namespace GardenRover
{
    using Interfaces;
    public class Coordinates : ICoordinates
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }

        public Coordinates(int xAxis, int yAxis) => (XAxis, YAxis) = (xAxis, yAxis);
    }
}