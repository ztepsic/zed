using System;
using NUnit.Framework;
using Zed.Utilities;

namespace Zed.Tests.Utilities {

    [TestFixture]
    public class DateTimeHelperTests {

        [Test]
        public void ToUnixTimeSeconds_SomeDateTime_UnixTimeSeconds() {
            // Arrange
            const long currentUnixTimesampSeconds = 1551742604;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 36, 44, DateTimeKind.Utc);

            // Act
            var resultUnixTime = currentDateTime.ToUnixTimeSeconds();

            // Assert
            Assert.AreEqual(currentUnixTimesampSeconds, resultUnixTime);
        }


        [Test]
        public void ToUnixTimeMiliseconds_SomeDateTime_UnixTimeMiliseconds() {
            // Arrange
            const long currentUnixTimesampMiliseconds = 1551742838747;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 40, 38, 747, DateTimeKind.Utc);

            // Act
            var resultUnixTime = currentDateTime.ToUnixTimeMilliseconds();

            // Assert
            Assert.AreEqual(currentUnixTimesampMiliseconds, resultUnixTime);
        }

        [Test]
        public void FromUnixTimeSeconds_SomeUnixTime_DateTime() {
            // Arrange
            const long currentUnixTimesampSeconds = 1551742604;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 36, 44, DateTimeKind.Utc);

            // Act
            var resultDateTime = DateTimeHelper.FromUnixTimeSeconds(currentUnixTimesampSeconds);

            // Assert
            Assert.AreEqual(currentDateTime, resultDateTime);
        }


        [Test]
        public void FromUnixTimeMiliseconds_SomeUnixTime_DateTime() {
            // Arrange
            const long currentUnixTimesampMiliseconds = 1551742838747;
            DateTime currentDateTime = new DateTime(2019, 03, 04, 23, 40, 38, 747, DateTimeKind.Utc);

            // Act
            var resultDateTime = DateTimeHelper.FromUnixTimeMiliseconds(currentUnixTimesampMiliseconds);

            // Assert
            Assert.AreEqual(currentDateTime, resultDateTime);
        }

    }
}
