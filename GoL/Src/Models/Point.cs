namespace GoL.Models {
    public class Point {
        public readonly long X;
        public readonly long Y;
        public Point(long x, long y) {
            X = x;
            Y = y;
        }

        private bool Equals(Point other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Point) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);

        public static bool operator ==(Point a, Point b)
            => !(a is null) && !(b is null) && a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point a, Point b) 
            => a is null || b is null || a.X != b.X || a.Y != b.Y;
    }
}