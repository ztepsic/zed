using System;

namespace Zed.Core.Utilities {
    /// <summary>
    /// NumbericHelper is a helper/extension class for numeric data types.
    /// </summary>
    public static class NumericHelper {

        /// <summary>
        /// The error margin of precision 0,00001
        /// </summary>
        public const float EPSILON_00001_FLOAT = 0.00001f;

        /// <summary>
        /// The error margin of precision 0,00001
        /// </summary>
        public const double EPSILON_00001_DOUBLE = 0.00001d;

        /// <summary>
        /// Method checks not whether the numbers are exactly the same, but wheather their difference
        /// is very small or whether the numbers are nearly equal. The error margin that the difference is compared to is called epsilon.
        /// Float number has precision of 7 digits.
        /// 
        /// Due to rounding errors, most floating-point numbers end up being slightly imprecise.
        /// As long as this imprecision stays small, it can usually be ignored.
        /// It also means that numbers expected to be equal (e.g. when calculating the same result through different correct methods)
        /// often differ slightly, and a simple equality test fails.
        /// </summary>
        /// <param name="a">First float number</param>
        /// <param name="b">Second float number</param>
        /// <param name="epsilon">The error margin that the difference is compared to.</param>
        /// <returns>True if the numbers are nearly equal (their differences is very small), otherwise false</returns>
        /// <see href="http://floating-point-gui.de/errors/comparison/"/>
        /// <see href="http://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp"/>
        public static bool AreNearlyEqual(float a, float b, float epsilon) {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b) {  // shortcut, handles infinities
                return true;
            } else if (a == 0 || b == 0 || diff < float.Epsilon) {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * float.Epsilon);
            } else {
                return diff / (absA + absB) < epsilon;
            }
        }

        /// <summary>
        /// Method checks not whether the numbers are exactly the same, but wheather their difference
        /// is very small or whether the numbers are nearly equal. The error margin that the difference is compared to is called epsilon.
        /// Float number has precision of 7 digits.
        /// 
        /// Due to rounding errors, most floating-point numbers end up being slightly imprecise.
        /// As long as this imprecision stays small, it can usually be ignored.
        /// It also means that numbers expected to be equal (e.g. when calculating the same result through different correct methods)
        /// often differ slightly, and a simple equality test fails.
        /// </summary>
        /// <param name="current">current float number</param>
        /// <param name="other">other float number</param>
        /// <param name="epsilon">The error margin that the difference is compared to.</param>
        /// <returns>True if the numbers are nearly equal (their differences is very small), otherwise false</returns>
        /// <see href="http://floating-point-gui.de/errors/comparison/"/>
        /// <see href="http://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp"/>
        public static bool IsNearlyEqual(this float current, float other, float epsilon) {
            return AreNearlyEqual(current, other, epsilon);
        }

        /// <summary>
        /// Method checks not whether the numbers are exactly the same, but wheather their difference
        /// is very small or whether the numbers are nearly equal. The error margin that the difference is compared to is called epsilon.
        /// Double number has precision of 15-16 digits.
        /// 
        /// Due to rounding errors, most floating-point numbers end up being slightly imprecise.
        /// As long as this imprecision stays small, it can usually be ignored.
        /// It also means that numbers expected to be equal (e.g. when calculating the same result through different correct methods)
        /// often differ slightly, and a simple equality test fails.
        /// </summary>
        /// <param name="a">First double number</param>
        /// <param name="b">Second double number</param>
        /// <param name="epsilon">The error margin that the difference is compared to.</param>
        /// <returns>True if the numbers are nearly equal (their differences is very small), otherwise false</returns>
        /// <see href="http://floating-point-gui.de/errors/comparison/"/>
        /// <see href="http://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp"/>
        public static bool AreNearlyEqual(double a, double b, double epsilon) {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a == b) {  // shortcut, handles infinities
                return true;
            } else if (a == 0 || b == 0 || diff < double.Epsilon) {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * double.Epsilon);
            } else {
                return diff / (absA + absB) < epsilon;
            }
        }

        /// <summary>
        /// Method checks not whether the numbers are exactly the same, but wheather their difference
        /// is very small or whether the numbers are nearly equal. The error margin that the difference is compared to is called epsilon.
        /// Double number has precision of 15-16 digits.
        /// 
        /// Due to rounding errors, most floating-point numbers end up being slightly imprecise.
        /// As long as this imprecision stays small, it can usually be ignored.
        /// It also means that numbers expected to be equal (e.g. when calculating the same result through different correct methods)
        /// often differ slightly, and a simple equality test fails.
        /// </summary>
        /// <param name="current">current double number</param>
        /// <param name="other">other double number</param>
        /// <param name="epsilon">The error margin that the difference is compared to.</param>
        /// <returns>True if the numbers are nearly equal (their differences is very small), otherwise false</returns>
        /// <see href="http://floating-point-gui.de/errors/comparison/"/>
        /// <see href="http://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp"/>
        public static bool IsNearlyEqual(this double current, double other, double epsilon) {
            return AreNearlyEqual(current, other, epsilon);
        }

    }
}
