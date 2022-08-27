using RayTracingOneWeek.Materials;

namespace RayTracingOneWeek.Objects;

using Point3 = Vec3;

public class Sphere : IHitTable
{
    public Point3 Center { get; }
    public double Radius { get; }
    
    public IMaterial Material { get; }


    public Sphere(Vec3 center, double radius, IMaterial material)
    {
        Center = center;
        Radius = radius;
        Material = material;
    }

    public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec)
    {
        rec = new HitRecord();
        Vec3 oc = r.Origin - Center;
        double a = r.Direction.LengthSquared();
        double halfB = Vec3.Dot(oc, r.Direction);
        double c = oc.LengthSquared() - Radius * Radius;

        var discriminant = halfB * halfB - a * c;
        if (discriminant < 0)
        {
            return false;
        }

        var sqrtD = Math.Sqrt(discriminant);

        // Find the nearest root that lies in the acceptable range.
        var root = (-halfB - sqrtD) / a;
        if (root <= tMin || tMax < root)
        {
            root = (-halfB + sqrtD) / a;
            if (root < tMin || tMax < root)
            {
                return false;
            }
        }

        rec = new HitRecord
        {
            T = root,
        };
        rec.P = r.At(rec.T);

        var outwardNormal = (rec.P - Center) / Radius;
        rec.SetFaceNormal(r, outwardNormal);
        rec.Material = Material;
        return true;
    }
}