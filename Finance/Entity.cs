using System;

namespace Finance
{
    public class Entity
    {
        public int Id { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (Entity)obj;
            return Equals(p);
        }

        protected bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        public static bool operator ==(Entity x, Entity y)
        {
            var x1 = (Object)x;
            var y1 = (Object)y;

            if (x1 != null && y1 != null)
                return x.Equals(y);

            return x1 == y1;
        }

        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
