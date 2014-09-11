using NUnit.Framework;
using Zed.Objects;

namespace Zed.Tests.Objects {
    [TestFixture]
    public class ImmutableObjectTests {

        class Car : ImmutableObject {
            private string name;

            public string Name {
                get { return name; }
                set {
                    FailIfImmutable();
                    name = value;
                }

            }

            private float topSpeed;

            public float TopSpeed {
                get { return topSpeed; }
                set {
                    FailIfImmutable();
                    topSpeed = value;
                }
            }

            protected override void OnFrozen() {
                name = name + " is Frozen";
            }


        }

        [Test]
        public void IsImmutable_FreezedObject_IsImmutable() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act
            var isImmutable = car.IsImmutable;

            // Assert
            Assert.IsTrue(isImmutable);

        }

        [Test]
        public void OnFrozen_AfterFreezeCallOnImmutableObject_OnFrozenIsCalled() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act

            // Assert
            Assert.IsTrue(car.IsImmutable);
            Assert.AreEqual("BMW is Frozen", car.Name);

        }

        [Test]
        [ExpectedException(typeof(ImmutableObjectException))]
        public void DataChangeAfterFreeze_ThrowsImmutableObjectException() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act
            car.Name = "Audi";

            // Assert

        }

    }
}
