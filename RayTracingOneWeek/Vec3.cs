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

    public static Vec3 operator +(Vec3 v1, Vec3 v2)
    {
        return new Vec3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
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
        return new Vec3(v1.X / d, v1.Y / d, v1.Z / d);
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

    public double SquaredLength()
    {
        return X * X + Y * Y + Z * Z;
    }

    public Vec3 UnitVector()
    {
        return this / Length();
    }

    public override string ToString()
    {
        return $"{X} {Y} {Z}";
    }
}