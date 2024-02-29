using System.Diagnostics;
using Xunit;
using Zed.Utilities;

namespace Zed.Tests.Utilities {
    
    public class NumericHelperExtensionTests {

        [Fact]
        public void Float_Precision_Compare01() {
            // Arrange
            // Initialize two floats with apparently identical values 
            float floatA = .33333f;
            float floatB = 1f / 3;

            Debug.WriteLine(floatA);
            Debug.WriteLine(floatB);

            // Act
            var isNearlyEqual = floatA.IsNearlyEqual(floatB, NumericHelper.EPSILON_00001_FLOAT);
            var areNearlyEqual = NumericHelper.AreNearlyEqual(floatA, floatB, NumericHelper.EPSILON_00001_FLOAT);

            // Assert
            Assert.NotEqual(floatA, floatB);
            Assert.False(floatA == floatB);
            Assert.True(isNearlyEqual);
            Assert.True(areNearlyEqual);

        }

        [Fact]
        public void Float_Presision_Compare02() {
            // Arrange
            float floatA = 1.2f;
            float floatB = 3f;
            float floatC = floatA * floatB;

            const float EXPECTED_RESULT = 3.6f;

            // Act
            var isNearlyEqual = floatC.IsNearlyEqual(EXPECTED_RESULT, NumericHelper.EPSILON_00001_FLOAT);
            var areNearlyEqual = NumericHelper.AreNearlyEqual(floatC, EXPECTED_RESULT, NumericHelper.EPSILON_00001_FLOAT);

            // Assert
            Assert.NotEqual(EXPECTED_RESULT, floatC);
            Assert.False(EXPECTED_RESULT == floatC);
            Assert.True(isNearlyEqual);
            Assert.True(areNearlyEqual);

        }

        [Fact]
        public void Float_Presision_Compare_Infinities() {
            // Arrange
            float floatA = float.PositiveInfinity;
            float floatB = float.PositiveInfinity;

            // Act
            var isNearlyEqual = floatA.IsNearlyEqual(floatB, NumericHelper.EPSILON_00001_FLOAT);
            var areNearlyEqual = NumericHelper.AreNearlyEqual(floatA, floatB, NumericHelper.EPSILON_00001_FLOAT);

            // Assert
            Assert.Equal(floatA, floatB);
            Assert.True(floatA == floatB);
            Assert.True(isNearlyEqual);
            Assert.True(areNearlyEqual);
        }

        [Fact]
        public void Double_Precision_Compare01() {
            // Arrange
            // Initialize two floats with apparently identical values 
            double doubleA = .33333d;
            double doubleB = 1d / 3;

            Debug.WriteLine(doubleA);
            Debug.WriteLine(doubleB);

            // Act
            var isNearlyEqual = doubleA.IsNearlyEqual(doubleB, NumericHelper.EPSILON_00001_DOUBLE);
            var areNearlyEqual = NumericHelper.AreNearlyEqual(doubleA, doubleB, NumericHelper.EPSILON_00001_DOUBLE);

            // Assert
            Assert.NotEqual(doubleA, doubleB);
            Assert.False(doubleA == doubleB);
            Assert.True(isNearlyEqual);
            Assert.True(areNearlyEqual);

        }


        [Fact]
        public void Double_Presision_Compare02() {
            // Arrange
            double doubleA = 1.2d;
            double doubleB = 3d;
            double doubleC = doubleA * doubleB;

            const double EXPECTED_RESULT = 3.6d;

            // Act
            var isNearlyEqual = doubleC.IsNearlyEqual(EXPECTED_RESULT, NumericHelper.EPSILON_00001_DOUBLE);
            var areNearlyEqual = NumericHelper.AreNearlyEqual(doubleC, EXPECTED_RESULT, NumericHelper.EPSILON_00001_DOUBLE);

            // Assert
            Assert.NotEqual(EXPECTED_RESULT, doubleC);
            Assert.False(EXPECTED_RESULT == doubleC);
            Assert.True(isNearlyEqual);
            Assert.True(areNearlyEqual);

        }


    }
}
