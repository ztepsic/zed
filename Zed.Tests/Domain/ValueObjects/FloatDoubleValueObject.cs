using Zed.Domain;
using Zed.Utilities;

namespace Zed.Tests.Domain.ValueObjects {
    public class FloatDoubleValueObject : ValueObject {

        private readonly float floatValue;

        [Precision(NumericHelper.EPSILON_00001_FLOAT)]
        public float FloatValue {
            get { return floatValue; }
        }

        private readonly double doubleValue;

        [Precision(NumericHelper.EPSILON_00001_DOUBLE)]
        public double DoubleValue {
            get { return doubleValue; }
        }

        public FloatDoubleValueObject(float floatValue, double doubleValue) {
            this.floatValue = floatValue;
            this.doubleValue = doubleValue;
        }

    }
}
