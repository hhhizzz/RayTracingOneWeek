namespace RayTracingOneWeek.Materials;

using Objects;
using Color = Vec3;

public class MetalMaterial : IMaterial
{
    public Color Color { get; }
    public double Fuzz { get; }

    public MetalMaterial(Color color, double f)
    {
        Color = color;
        Fuzz = f < 1 ? f : 1;
    }

    public bool Scatter(Ray rIn, HitRecord rec, out Vec3 attenuation, out Ray scattered)
    {
        var reflected = Vec3.Reflect(rIn.Direction.UnitVector(), rec.Normal);
        scattered = new Ray(rec.P, reflected + Fuzz * Vec3.RandomInUnitSphere());
        attenuation = Color;
        return (Vec3.Dot(scattered.Direction, rec.Normal) > 0);
    }
}