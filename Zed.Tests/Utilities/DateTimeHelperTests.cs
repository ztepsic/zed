using System;
using Xunit;
using Zed.Utilities;

namespace Zed.Tests.Utilities {

    
    public class DateTimeHelperTests {

        [Fact]
        public void ToUnixTimeSeconds_SomeDateTime_UnixTimeSeconds() {
            // Arrange
            const long currentUnixTimesampSeconds = 1551742604;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 36, 44, DateTimeKind.Utc);

            // Act
            var resultUnixTime = currentDateTime.ToUnixTimeSeconds();

            // Assert
            Assert.Equal(currentUnixTimesampSeconds, resultUnixTime);
        }


        [Fact]
        public void ToUnixTimeMiliseconds_SomeDateTime_UnixTimeMiliseconds() {
            // Arrange
            const long currentUnixTimesampMiliseconds = 1551742838747;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 40, 38, 747, DateTimeKind.Utc);

            // Act
            var resultUnixTime = currentDateTime.ToUnixTimeMilliseconds();

            // Assert
            Assert.Equal(currentUnixTimesampMiliseconds, resultUnixTime);
        }

        [Fact]
        public void FromUnixTimeSeconds_SomeUnixTime_DateTime() {
            // Arrange
            const long currentUnixTimesampSeconds = 1551742604;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 36, 44, DateTimeKind.Utc);

            // Act
            var resultDateTime = DateTimeHelper.FromUnixTimeSeconds(currentUnixTimesampSeconds);

            // Assert
            Assert.Equal(currentDateTime, resultDateTime);
        }


        [Fact]
        public void FromUnixTimeMiliseconds_SomeUnixTime_DateTime() {
            // Arrange
            const long currentUnixTimesampMiliseconds = 1551742838747;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 40, 38, 747, DateTimeKind.Utc);

            // Act
            var resultDateTime = DateTimeHelper.FromUnixTimeMiliseconds(currentUnixTimesampMiliseconds);

            // Assert
            Assert.Equal(currentDateTime, resultDateTime);
        }

    }
}
