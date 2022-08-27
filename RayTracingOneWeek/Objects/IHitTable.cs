namespace RayTracingOneWeek.Objects;

using Point3 = Vec3;

public struct HitRecord
{
    public Point3 P;
    public Vec3 Normal;
    public double T;
    public bool FrontFace;
    
    public void SetFaceNormal(Ray r, Vec3 outwardNormal)
    {
        FrontFace = Vec3.Dot(r.Direction, outwardNormal) < 0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
    }
}

public interface IHitTable
{
    public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec);
}