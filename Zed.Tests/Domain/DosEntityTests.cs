using NUnit.Framework;
using Zed.Tests.Domain.Entities.DomainObjectsSignatureImpl;

namespace Zed.Tests.Domain {
    [TestFixture]
    public class DosEntityTests {

        [Test]
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
            Assert.AreEqual(default(int), carA.Id);
            Assert.AreEqual(default(int), carB.Id);
            Assert.AreEqual(default(int), carC.Id);
            Assert.IsFalse(areEqualCarACarB);
            Assert.IsTrue(areEqualCarACarC);
            Assert.IsFalse(ReferenceEquals(carA, carB));

        }
    }
}
