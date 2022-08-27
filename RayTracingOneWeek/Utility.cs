namespace RayTracingOneWeek;

public static class Utility
{
    public static double Pi = 3.1415926535897932385;

    private static Random _random = new Random();

    public static double RandomDouble()
    {
        return _random.NextDouble();
    }

    public static double RandomDouble(double min, double max)
    {
        return min + (max - min) * RandomDouble();
    }

    public static Vec3 RandomInUnitDisk()
    {
        while (true)
        {
            var p = new Vec3(RandomDouble(-1, 1), RandomDouble(-1, 1), 0);
            if (p.LengthSquared() >= 1) continue;
            return p;
        }
    }

    public static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    public static double Clamp(double x, double min, double max)
    {
        if (x < min) return min;
        if (x > max) return max;
        return x;
    }
}