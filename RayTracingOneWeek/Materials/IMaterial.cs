using RayTracingOneWeek.Objects;

namespace RayTracingOneWeek.Materials;

public interface IMaterial
{
    bool Scatter(Ray rIn, HitRecord rec, out Vec3 attenuation, out Ray scattered);
}