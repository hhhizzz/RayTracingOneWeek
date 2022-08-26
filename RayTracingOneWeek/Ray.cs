namespace RayTracingOneWeek;

using Point3 = Vec3;

public class Ray
{
    public Point3 Origin { get; }
    public Point3 Direction { get; }

    public Ray()
    {
        Origin = new Point3();
        Direction = new Point3();
    }

    public Ray(Vec3 origin, Vec3 direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Point3 At(double t)
    {
        return Origin + t * Direction;
    }
}