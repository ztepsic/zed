namespace Zed.Core.Tests.Domain.ValueObjects {
    public class Point3D : Point2D {
        private readonly int z;
        public int Z { get { return z; } }
        public Point3D(int x, int y, int z)
            : base(x, y) {
            this.z = z;
        }
    }
}
