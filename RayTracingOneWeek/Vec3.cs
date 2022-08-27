namespace RayTracingOneWeek;

using Point3 = Vec3;
using Color = Vec3;

public class Vec3
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public Vec3()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public Vec3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vec3 Random()
    {
        return new Vec3(Utility.RandomDouble(), Utility.RandomDouble(), Utility.RandomDouble());
    }

    public static Vec3 Random(double min, double max)
    {
        return new Vec3(Utility.RandomDouble(min, max), Utility.RandomDouble(min, max), Utility.RandomDouble(min, max));
    }

    public static Vec3 operator +(Vec3 v1, Vec3 v2)
    {
        return new Vec3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vec3 operator -(Vec3 v)
    {
        return new Vec3(-v.X, -v.Y, -v.Z);
    }

    public static Vec3 operator -(Vec3 v1, Vec3 v2)
    {
        return new Vec3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vec3 operator *(Vec3 v1, double d)
    {
        return new Vec3(v1.X * d, v1.Y * d, v1.Z * d);
    }

    public static Vec3 operator *(double d, Vec3 v1)
    {
        return new Vec3(v1.X * d, v1.Y * d, v1.Z * d);
    }

    public static Vec3 operator *(Vec3 v1, Vec3 v2)
    {
        return new Vec3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    }

    public static Vec3 operator /(Vec3 v1, double d)
    {
        return v1 * (1 / d);
    }

    public static double Dot(Vec3 v1, Vec3 v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static Vec3 Cross(Vec3 v1, Vec3 v2)
    {
        return new Vec3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
    }

    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public double LengthSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    public Vec3 UnitVector()
    {
        return this / Length();
    }

    public bool NearZero()
    {
        // Return true if the vector is close to zero in all dimensions.
        const double s = 1e-8;
        return (Math.Abs(X) < s) && (Math.Abs(Y) < s) && (Math.Abs(Z) < s);
    }

    public static Vec3 RandomUnitVector()
    {
        return RandomInUnitSphere().UnitVector();
    }

    public static Vec3 RandomInUnitSphere()
    {
        while (true)
        {
            var p = Random(-1, 1);
            if (p.LengthSquared() >= 1) continue;
            return p;
        }
    }

    public static Vec3 Reflect(Vec3 v, Vec3 n)
    {
        return v - 2 * Vec3.Dot(v, n) * n;
    }

    public static Vec3 Refract(Vec3 uv, Vec3 n, double etaiOverEtat)
    {
        var cosTheta = Math.Min(Vec3.Dot(-uv, n), 1.0);
        var rOutPerp = etaiOverEtat * (uv + cosTheta * n);
        var rOutParallel = -Math.Sqrt(Math.Abs(1.0 - rOutPerp.LengthSquared())) * n;
        return rOutPerp + rOutParallel;
    }

    public override string ToString()
    {
        return $"{X} {Y} {Z}";
    }
}