namespace Zed.Core.Tests.Domain.ValueObjects {
    public class ColoredPoint2D : Point2D {
        private readonly string color;
        public string Color { get { return color; } }
        public ColoredPoint2D(int x, int y, string color)
            : base(x, y) {
            this.color = color;
        }
    }
}
