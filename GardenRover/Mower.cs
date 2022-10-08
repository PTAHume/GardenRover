

namespace GardenRover
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mower : IMower
    {
        private ICoordinates Boundary { get; }

        public Mower(ICoordinates startingLocation, string initialHeading, ICoordinates initialBoundary)
        {
            Location = startingLocation;
            Boundary = initialBoundary;
            if (!Enum.TryParse<Facing>(initialHeading, true, out Facing heading))
            {
                throw new ArgumentException("Mower direction parameter is invalid");
            }

            Heading = heading;
            if (Location.XAxis > Boundary.XAxis || Location.YAxis > Boundary.YAxis)
            {
                throw new ArgumentException("Mower location is outside of the boundary");
            }

            if (!new CustomerValidator().Validate(Location as Coordinates).IsValid)
            {
                throw new ArgumentException("Mower location parameter is invalid");
            }
        }

        public ICoordinates Location { get; set; }
        public Facing Heading { get; set; }

        public void Command(IReadOnlyList<string> orders)
        {
            if (orders?.Any() != true)
            {
                return;
            }

            foreach (string orderItem in orders)
            {
                if (!Enum.TryParse<Orders>(orderItem, true, out Orders order))
                {
                    throw new ArgumentException("Command is invalid");
                }

                switch (order)
                {
                    case Orders.L:
                        Heading = ((int)Heading) - 1 < 0 ? Facing.W : Heading - 1;
                        break;

                    case Orders.R:
                        Heading = ((int)Heading) + 1 > 3 ? Facing.N : Heading + 1;
                        break;

                    case Orders.M:
                        if (Heading == Facing.N && Location.YAxis + 1 <= this.Boundary.YAxis)
                        {
                            Location.YAxis++;
                            break;
                        }
                        else if (Heading == Facing.S && Location.YAxis - 1 >= 0)
                        {
                            Location.YAxis--;
                            break;
                        }
                        else if (Heading == Facing.E && Location.XAxis + 1 <= this.Boundary.XAxis)
                        {
                            Location.XAxis++;
                            break;
                        }
                        else if (Heading == Facing.W && Location.XAxis - 1 >= 0)
                        {
                            Location.XAxis--;
                            break;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        public string CurrentPosition()
        {
            return $"{Location.XAxis} {Location.YAxis} {Enum.GetName(typeof(Facing), Heading)}";
        }
    }
}