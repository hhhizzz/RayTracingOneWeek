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