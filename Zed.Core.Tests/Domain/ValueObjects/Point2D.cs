namespace Zed.Core.Tests.Domain.ValueObjects {
    public abstract class Point2D : Point {
        private readonly int y;
        public int Y { get { return y; } }
        public Point2D(int x, int y)
            : base(x) {
            this.y = y;
        }
    }
}
