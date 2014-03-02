using NUnit.Framework;
using Zed.Core.Tests.Domain.ValueObjects;

namespace Zed.Core.Tests.Domain {
    [TestFixture]
    public class ValueObjectTests {

        /// <summary>
        /// For any non-null reference value x, x.Equals(null) must return false
        /// </summary>
        [Test]
        public void Equals_Returns_False_For_Provided_Null_Value() {
            // Arrange
            ColoredPoint2D coloredPoint2D = new ColoredPoint2D(1, 2, "Red");

            // Act
            var valueObjectEquivalenceResult = coloredPoint2D.Equals(null);

            // Assert
            Assert.IsNotNull(coloredPoint2D);
            Assert.IsFalse(valueObjectEquivalenceResult);
        }

        /// <summary>
        /// For any non-null reference value x, x.Equals(x) must return true.
        /// </summary>
        [Test]
        public void Equals_Implements_Reflexive_Equivalence_Relation() {
            // Arrange
            ColoredPoint2D coloredPoint2D = new ColoredPoint2D(1, 2, "Red");


            // Act
            var valueObjectEquivalenceResult = coloredPoint2D.Equals(coloredPoint2D);
            var hashCodeEquivalenceResult = coloredPoint2D.GetHashCode().Equals(coloredPoint2D.GetHashCode());

            // Assert
            Assert.IsNotNull(coloredPoint2D);
            Assert.IsTrue(valueObjectEquivalenceResult);
            Assert.IsTrue(hashCodeEquivalenceResult);
        }

        /// <summary>
        /// Two value objects are equal if they have the same properties and same type.
        /// Equivalence is based on the same inheritance hierarcy and the same identifiers or business keys.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Test]
        public void Two_ValueObjects_Are_Equal_If_They_Have_Same_Properties_And_Type_Equals_Implements_Symmetric_Equivalence_Relation() {
            // Arrange
            ColoredPoint2D coloredPoint2DX = new ColoredPoint2D(1, 2, "Red");
            ColoredPoint2D coloredPoint2DY = new ColoredPoint2D(1, 2, "Red");

            // Act
            var valueObjectsEquivalenceResultXY = coloredPoint2DX.Equals(coloredPoint2DY);
            var valueObjectsEquivalenceResultYX = coloredPoint2DY.Equals(coloredPoint2DX);
            var hashCodeEquivalenceResult = coloredPoint2DX.GetHashCode().Equals(coloredPoint2DY.GetHashCode());

            // Assert
            Assert.IsNotNull(coloredPoint2DX);
            Assert.IsNotNull(coloredPoint2DY);
            Assert.IsTrue(valueObjectsEquivalenceResultXY);
            Assert.IsTrue(valueObjectsEquivalenceResultYX);
            Assert.IsTrue(hashCodeEquivalenceResult);
        }

        /// <summary>
        /// Two ValueObjects of the same type and different properties(ValueMembers) are not equal.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Test]
        public void Two_ValueObjects_Of_Same_Type_And_Different_Properties_Are_Not_Equal_Equals_Implements_Symmetric_Equivalence_Relation() {
            // Arrange
            ColoredPoint2D coloredPoint2DX = new ColoredPoint2D(1, 2, "Red");
            ColoredPoint2D coloredPoint2DY = new ColoredPoint2D(3, 4, "Blue");

            // Act
            var valueObjectsEquivalenceResultXY = coloredPoint2DX.Equals(coloredPoint2DY);
            var valueObjectsEquivalenceResultYX = coloredPoint2DY.Equals(coloredPoint2DX);

            // Assert
            Assert.IsNotNull(coloredPoint2DX);
            Assert.IsNotNull(coloredPoint2DY);
            Assert.IsFalse(valueObjectsEquivalenceResultXY);
            Assert.IsFalse(valueObjectsEquivalenceResultYX);
        }

        /// <summary>
        /// Two ValueObjects of different type in the same type hierarchy are not equal.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Test]
        public void Two_ValueObjects_Of_Different_Type_In_Same_Type_Hierarchy_Are_Not_Equal_Equals_Implements_Symmetric_Equivalence_Relation() {
            // Arrange
            ColoredPoint2D coloredPoint2DX = new ColoredPoint2D(1, 2, "Red");
            Point3D point3DY = new Point3D(1, 2, 3);

            // Act
            var valueObjectsEquivalenceResultXY = coloredPoint2DX.Equals(point3DY);
            var valueObjectsEquivalenceResultYX = point3DY.Equals(coloredPoint2DX);

            // Assert
            Assert.IsNotNull(coloredPoint2DX);
            Assert.IsNotNull(point3DY);
            Assert.IsFalse(valueObjectsEquivalenceResultXY);
            Assert.IsFalse(valueObjectsEquivalenceResultYX);
        }

        /// <summary>
        /// Two ValueObjects of different type are not equal.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Test]
        public void Two_ValueObjects_Of_Different_Type_Are_Not_Equal_Equals_Implements_Symmetric_Equivalence_Relation() {
            // Arrange
            ColoredPoint2D coloredPoint2DX = new ColoredPoint2D(1, 2, "Red");
            Money moneyY = new Money(12, "EUR");

            // Act
            var valueObjectsEquivalenceResultXY = coloredPoint2DX.Equals(moneyY);
            var valueObjectsEquivalenceResultYX = moneyY.Equals(coloredPoint2DX);

            // Assert
            Assert.IsNotNull(coloredPoint2DX);
            Assert.IsNotNull(moneyY);
            Assert.IsFalse(valueObjectsEquivalenceResultXY);
            Assert.IsFalse(valueObjectsEquivalenceResultYX);
        }

        /// <summary>
        /// Three value objects are equal if they have the same properties and type..
        /// For any non-null reference values x, y, z, if x.Equals(y) returns true and
        /// y.Equals(z) return true, then x.Equals(z) must return true.
        /// </summary>
        [Test]
        public void Three_ValueObjects_Are_Equal_If_They_Have_Same_Properties_And_Type_Equals_Implements_Transitive_Equivalence_Relation() {
            ColoredPoint2D coloredPoint2DX = new ColoredPoint2D(1, 2, "Red");
            ColoredPoint2D coloredPoint2DY = new ColoredPoint2D(1, 2, "Red");
            ColoredPoint2D coloredPoint2DZ = new ColoredPoint2D(1, 2, "Red");

            // Act
            var entitiesEquivalenceResultXY = coloredPoint2DX.Equals(coloredPoint2DY);
            var entitiesEquivalenceResultYZ = coloredPoint2DY.Equals(coloredPoint2DZ);
            var entitiesEquivalenceResultXZ = coloredPoint2DX.Equals(coloredPoint2DZ);
            var hashCodeEquivalenceResultXY = coloredPoint2DX.GetHashCode().Equals(coloredPoint2DY.GetHashCode());
            var hashCodeEquivalenceResultYZ = coloredPoint2DY.GetHashCode().Equals(coloredPoint2DZ.GetHashCode());
            var hashCodeEquivalenceResultXZ = coloredPoint2DX.GetHashCode().Equals(coloredPoint2DZ.GetHashCode());

            // Assert
            Assert.IsNotNull(coloredPoint2DX);
            Assert.IsNotNull(coloredPoint2DY);
            Assert.IsNotNull(coloredPoint2DZ);
            Assert.IsTrue(entitiesEquivalenceResultXY);
            Assert.IsTrue(entitiesEquivalenceResultYZ);
            Assert.IsTrue(entitiesEquivalenceResultXZ);
            Assert.IsTrue(hashCodeEquivalenceResultXY);
            Assert.IsTrue(hashCodeEquivalenceResultYZ);
            Assert.IsTrue(hashCodeEquivalenceResultXZ);
        }

        [Test]
        public void In_Equals_Comparison_NonValueMember_Properties_Are_Ignored() {
            // Arrange
            Money moneyX = new Money(12, "EUR") {
                NotValueMember1 = "X1",
                NotValueMember2 = "X2",
            };
            Money moneyY = new Money(12, "EUR") {
                NotValueMember1 = "Y1",
                NotValueMember2 = "Y2"
            };

            // Act
            var valueObjectsEquivalenceResultXY = moneyX.Equals(moneyY);
            var valueObjectsEquivalenceResultYX = moneyY.Equals(moneyX);
            var hashCodeEquivalenceResult = moneyX.GetHashCode().Equals(moneyY.GetHashCode());

            // Assert
            Assert.IsNotNull(moneyX);
            Assert.IsNotNull(moneyY);
            Assert.IsTrue(valueObjectsEquivalenceResultXY);
            Assert.IsTrue(valueObjectsEquivalenceResultYX);
            Assert.IsTrue(hashCodeEquivalenceResult);
        }

        [Test]
        public void Compare_Float_And_Decimal_Value_With_Precision_Attribute() {
            // Arrange
            float floatA = 1.2f * 3f;
            double doubleA = 1.2d * 3d;
            var valueObjectA = new FloatDoubleValueObject(floatA, doubleA);

            float floatB = 3.6f;
            double doubleB = 3.6d;
            var valueObjectB = new FloatDoubleValueObject(floatB, doubleB);

            // Act

            // Assert
            Assert.AreEqual(valueObjectA, valueObjectB);
        }
    }
}
