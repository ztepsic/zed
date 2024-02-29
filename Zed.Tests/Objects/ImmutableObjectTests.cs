using Xunit;
using Zed.Objects;

namespace Zed.Tests.Objects {
    
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

        [Fact]
        public void IsImmutable_FreezedObject_IsImmutable() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act
            var isImmutable = car.IsImmutable;

            // Assert
            Assert.True(isImmutable);

        }

        [Fact]
        public void OnFrozen_AfterFreezeCallOnImmutableObject_OnFrozenIsCalled() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act

            // Assert
            Assert.True(car.IsImmutable);
            Assert.Equal("BMW is Frozen", car.Name);

        }

        [Fact]
        public void DataChangeAfterFreeze_ThrowsImmutableObjectException() {
            // Arrange
            Car car = new Car();
            car.Name = "BMW";
            car.TopSpeed = 250.3f;
            car.Freeze();

            // Act and Assert
            Assert.Throws<ImmutableObjectException>(() => car.Name = "Audi");

            // Assert

        }

    }
}
