namespace GardenRover.Tests
{
    using Interfaces;
    using NUnit.Framework;
    using System;

    public class MowerTests
    {

        [Test]
        public void CanProvideCurrentPositionAsString()
        {
            //arrange
            ICoordinates startingLocation = new Coordinates(3, 3);
            const string initialHeading = "N";
            ICoordinates initialBoundary = new Coordinates(5, 5);

            //act
            IMower mower = new Mower(startingLocation, initialHeading, initialBoundary);

            //assert
            Assert.AreEqual("3 3 N", mower.CurrentPosition(),
                "Mower is not in the starting correct position");
        }

        [Test]
        public void MowerCanMoveAndTurn()
        {
            //arrange
            ICoordinates startingLocation = new Coordinates(3, 3);
            const string initialHeading = "W";
            ICoordinates initialBoundary = new Coordinates(5, 5);

            //act
            IMower mower = new Mower(startingLocation, initialHeading, initialBoundary);
            mower.Command(new string[] { "R", "M", "L", "M", "M", "L" });

            //assert
            Assert.AreEqual("1 4 S", mower.CurrentPosition(),
                "Mower is not in the correct new position");
        }

        [Test]
        public void CanNotMoveOutOfBounds()
        {
            //arrange
            ICoordinates startingLocation = new Coordinates(0, 0);
            const string initialHeading = "N";
            ICoordinates initialBoundary = new Coordinates(5, 5);

            //act
            IMower mower = new Mower(startingLocation, initialHeading, initialBoundary);
            mower.Command(new string[] { "M", "M", "M", "M", "M", "M", "M" });

            //assert 
            Assert.AreEqual("0 5 N", mower.CurrentPosition(), "Failed top boundary test");

            //act
            mower.Command(new string[] { "R", "R", "M", "M", "M", "M", "M", "M", "M" });

            //assert 
            Assert.AreEqual("0 0 S", mower.CurrentPosition(), "Failed bottom boundary test");

            //act
            mower.Command(new string[] { "L", "M", "M", "M", "M", "M", "M", "M" });

            //assert
            Assert.AreEqual("5 0 E", mower.CurrentPosition(), "Failed right boundary test");

            //act
            mower.Command(new string[] { "R", "R", "M", "M", "M", "M", "M", "M", "M" });

            //assert 
            Assert.AreEqual("0 0 W", mower.CurrentPosition(), "Failed left boundary test");
        }

        [Test]
        public void CanMoveMultipleMowers()
        {
            //arrange
            IMower firstMower = new Mower(
                new Coordinates(3, 3),
                "N", new Coordinates(5, 5));
            IMower secondMower = new Mower(
                new Coordinates(2, 2),
                "E", new Coordinates(5, 5));

            //act
            firstMower.Command(new string[] { "R", "M", "M", "R", "M", "M" });
            secondMower.Command(new string[] { "R", "M", "L", "M", "M", "L" });

            //assert 
            Assert.AreEqual("5 1 S", firstMower.CurrentPosition());
            Assert.AreEqual("4 1 N", secondMower.CurrentPosition());
        }

        [Test]
        public void CanCompleteUseCase()
        {
            //arrange
            IMower firstMower = new Mower(
                startingLocation: new Coordinates(xAxis: 1, yAxis: 2),
                initialHeading: "N", initialBoundary: new Coordinates(xAxis: 5, yAxis: 5));
            IMower secondMower = new Mower(
                startingLocation: new Coordinates(xAxis: 3, yAxis: 3),
                initialHeading: "E", initialBoundary: new Coordinates(xAxis: 5, yAxis: 5));

            //act
            firstMower.Command(orders: new[] { "L", "M", "L", "M", "L", "M", "L", "M", "M" });
            secondMower.Command(orders: new[] { "M", "M", "R", "M", "M", "R", "M", "R", "R", "M" });

            //assert 
            Assert.AreEqual(expected: "1 3 N", actual: firstMower.CurrentPosition());
            Assert.AreEqual(expected: "5 1 E", actual: secondMower.CurrentPosition());
        }

        [Test]
        public void CanHandleBadCommands()
        {
            Assert.Throws<ArgumentException>(code: () => new Mower(
                startingLocation: new Coordinates(xAxis: 10, yAxis: 10),
                initialHeading: "N", initialBoundary: new Coordinates(xAxis: 5, yAxis: 5)));

        }
    }
}