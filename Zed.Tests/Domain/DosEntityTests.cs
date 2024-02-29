using Xunit;
using Zed.Tests.Domain.Entities.DomainObjectsSignatureImpl;

namespace Zed.Tests.Domain {
    
    public class DosEntityTests {

        [Fact]
        public void Two_Transient_Entities_Are_Equal_If_They_Have_Same_Business_Key() {
            // Arrange
            var carA = new Car {
                Name = "BMW",
                TopSpeed = 200
            };

            var carB = new Car {
                Name = "Audi",
                TopSpeed = 200
            };

            var carC = new Car {
                Name = "BMW",
                TopSpeed = 250
            };

            // Act
            var areEqualCarACarB = carA.Equals(carB);
            var areEqualCarACarC = carA.Equals(carC);

            // Assert
            Assert.Equal(default, carA.Id);
            Assert.Equal(default, carB.Id);
            Assert.Equal(default, carC.Id);
            Assert.False(areEqualCarACarB);
            Assert.True(areEqualCarACarC);
            Assert.False(ReferenceEquals(carA, carB));

        }
    }
}
