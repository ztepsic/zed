namespace Zed.Core.Tests.Domain.ValueObjects {
    public class ColoredPoint3D : Point3D {
        private readonly string color;
        public string Color { get { return color; } }
        public ColoredPoint3D(int x, int y, int z, string color)
            : base(x, y, z) {
            this.color = color;
        }
    }
}
