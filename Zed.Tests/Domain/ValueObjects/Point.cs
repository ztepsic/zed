using Zed.Domain;

namespace Zed.Tests.Domain.ValueObjects {
    public abstract class Point : ValueObject {
        private readonly int x;
        public int X { get { return x; } }
        public Point(int x) {
            this.x = x;
        }
    }
}
