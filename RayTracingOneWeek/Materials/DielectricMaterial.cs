namespace RayTracingOneWeek.Materials;

using Objects;

public class DielectricMaterial : IMaterial
{
    public double IndexOfRefraction { get; }

    public DielectricMaterial(double indexOfRefraction)
    {
        IndexOfRefraction = indexOfRefraction;
    }

    public bool Scatter(Ray rIn, HitRecord rec, out Vec3 attenuation, out Ray scattered)
    {
        attenuation = new Vec3(1.0, 1.0, 1.0);
        var refractionRatio = rec.FrontFace ? (1.0 / IndexOfRefraction) : IndexOfRefraction;

        var unitDirection = rIn.Direction.UnitVector();
        var refracted = Vec3.Refract(unitDirection, rec.Normal, refractionRatio);

        scattered = new Ray(rec.P, refracted);
        return true;
    }
}