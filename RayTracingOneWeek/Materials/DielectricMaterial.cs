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
        var cosTheta = Math.Min(Vec3.Dot(-unitDirection, rec.Normal), 1.0);
        var sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

        var cannotRefract = refractionRatio * sinTheta > 1.0;

        var direction = cannotRefract || reflectance(cosTheta, refractionRatio) > Utility.RandomDouble()
            ? Vec3.Reflect(unitDirection, rec.Normal)
            : Vec3.Refract(unitDirection, rec.Normal, refractionRatio);

        scattered = new Ray(rec.P, direction);
        return true;
    }

    private static double reflectance(double cosine, double refIdx)
    {
        // Use Schlick's approximation for reflectance.
        var r0 = (1 - refIdx) / (1 + refIdx);
        r0 *= r0;
        return r0 + (1 - r0) * Math.Pow((1 - cosine), 5);
    }
}