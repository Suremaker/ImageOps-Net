using System.Drawing;

namespace ImageOps.Sources.Regions
{
    class Section
    {
        private readonly double _aMultiplier;

        public Section(Point item1, Point item2)
        {
            P1 = item1;
            P2 = item2;
            if (P1.X != P2.X)
            {
                A = (item1.Y - item2.Y) / (double)(item1.X - item2.X);
                B = item1.Y - A * item1.X;
                _aMultiplier = 1.0 / A;
            }
        }

        public Point P1 { get; private set; }
        public Point P2 { get; private set; }
        public double A { get; private set; }
        public double B { get; private set; }

        public double IsCrossedByLeftHorizontalRay(Point startPoint)
        {
            if (!IsOnOppositeSides(P1.Y, P2.Y, startPoint.Y))
                return double.NaN;
            if (P1.X == P2.X) //perpendicular
                return P1.X;
            if (P1.Y == P2.Y)
                return IsOnOppositeSides(P1.X, P2.X, startPoint.X)
                    ? startPoint.X
                    : double.NaN;
            var crossX = (startPoint.Y - B) * _aMultiplier;
            return (startPoint.Y != P1.Y || crossX != P1.X) ? crossX : double.NaN;
        }

        private static bool IsOnOppositeSides(int sy1, int sy2, int py)
        {
            return (py - sy1) * (py - sy2) < 1;
        }
    }
}