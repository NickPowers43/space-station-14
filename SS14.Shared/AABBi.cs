using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace SS14.Shared
{
    public struct AABBi
    {
        public Vector2i bl, tr;

        public AABBi(Vector2i bl, Vector2i tr)
        {
            this.bl = bl;
            this.tr = tr;
        }

        public bool Contains(Vector2i p)
        {
            return (p.X > bl.X && p.X < tr.X) && (p.Y > bl.Y && p.Y < tr.Y);
        }

        public void Fit(Vector2i point)
        {
            if (point.X < bl.X)
            {
                bl.X = point.X;
            }
            if (point.Y < bl.Y)
            {
                bl.Y = point.Y;
            }

            if (point.X >= tr.X)
            {
                tr.X = point.X + 1;
            }
            if (point.Y >= tr.Y)
            {
                tr.Y = point.Y + 1;
            }
        }

        public void FitAABB(ref AABBi aabb)
        {
            bl.X = Math.Min(aabb.bl.X, bl.X);
            bl.Y = Math.Min(aabb.bl.Y, bl.Y);

            tr.X = Math.Max(aabb.tr.X, tr.X);
            tr.Y = Math.Max(aabb.tr.Y, tr.Y);
        }

        public static System.Boolean operator ==(AABBi a, AABBi b)
        {
            return (a.bl == b.bl) && (a.tr == b.tr);
        }

        public static System.Boolean operator !=(AABBi a, AABBi b)
        {
            return (a.bl != b.bl) || (a.tr != b.tr);
        }
    }
}
