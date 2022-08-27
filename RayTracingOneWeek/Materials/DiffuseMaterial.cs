namespace RayTracingOneWeek.Materials;

using Objects;
using Color = Vec3;

public class DiffuseMaterial : IMaterial
{
    public Color Color { get; }

    public DiffuseMaterial(Color color)
    {
        Color = color;
    }

    public bool Scatter(Ray rIn, HitRecord rec, out Vec3 attenuation, out Ray scattered)
    {
        var scatterDirection = rec.Normal + Vec3.RandomUnitVector();

        // Catch degenerate scatter direction
        if (scatterDirection.NearZero())
            scatterDirection = rec.Normal;

        scattered = new Ray(rec.P, scatterDirection);
        attenuation = Color;
        return true;
    }
}