using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedLibrary;
using System;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class ParsedDataTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_AircraftIDIsNullOrEmpty()
        {
            // Arrange
            var parsedData = new ParsedData
            {
                AircraftID = "",
                AccelX = 0
            };

            // Act
            parsedData.Validate();

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Validate_AccelXIsOutOfRange()
        {
            // Arrange
            var parsedData = new ParsedData
            {
                AircraftID = "A123",
                AccelX = 15 // Out of range
            };

            // Act
            parsedData.Validate();

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void Validate_WhenDataIsValid()
        {
            // Arrange
            var parsedData = new ParsedData
            {
                AircraftID = "A123",
                AccelX = 5 // Within range
            };

            // Act & Assert
            parsedData.Validate();
        }
    }
}
