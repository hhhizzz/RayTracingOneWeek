using System.Diagnostics.CodeAnalysis;
using RayTracingOneWeek.Materials;
using RayTracingOneWeek.Objects;

namespace RayTracingOneWeek;
//Image

using Color = Vec3;
using Point3 = Vec3;

public static class Program
{
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    private static Color RayColor(Ray r, IHitTable world, int depth)
    {
        if (depth <= 0)
        {
            return new Color(0, 0, 0);
        }

        if (world.Hit(r, 0.001f, double.MaxValue, out var rec))
        {
            if (rec.Material.Scatter(r, rec, out var attenuation, out var scattered))
            {
                return attenuation * RayColor(scattered, world, depth - 1);
            }

            return new Color(0, 0, 0);
        }

        var unitDirection = r.Direction.UnitVector();
        var t = 0.5 * (unitDirection.Y + 1.0);
        return (1.0 - t) * new Color(1, 1, 1) + t * new Color(0.5, 0.7, 1.0);
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public static void Main(string[] args)
    {
        //Image
        using StreamWriter file = new("image.ppm");
        const double aspectRatio = (double)16 / 9;
        const int imageWidth = 400;
        const int imageHeight = (int)(imageWidth / aspectRatio);
        const int samplesPerPixel = 100;
        const int maxDepth = 50;

        //World
        var world = new HitTableList();
        var materialGround = new DiffuseMaterial(new Color(0.8, 0.8, 0.0));
        var materialCenter = new DielectricMaterial(1.5);
        var materialLeft = new DielectricMaterial(1.5);
        var materialRight = new MetalMaterial(new Color(0.8, 0.6, 0.2), 1.0);

        world.Add(new Sphere(new Point3(0, -100.5, -1), 100, materialGround));
        world.Add(new Sphere(new Point3(0, 0, -1), 0.5, materialCenter));
        world.Add(new Sphere(new Point3(-1, 0, -1), 0.5, materialLeft));
        world.Add(new Sphere(new Point3(1, 0, -1), 0.5, materialRight));

        //Camera
        Camera camera = new Camera();

        //Render
        file.Write($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (var j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.Write($"\rScan lines remaining: {j:000}");
            for (var i = 0; i < imageWidth; i++)
            {
                Color pixelColor = new(0, 0, 0);
                for (var s = 0; s < samplesPerPixel; s++)
                {
                    var u = (i + Utility.RandomDouble()) / (imageWidth - 1);
                    var v = (j + Utility.RandomDouble()) / (imageHeight - 1);
                    var r = camera.GetRay(u, v);
                    pixelColor += RayColor(r, world, maxDepth);
                }

                ColorRender.WriteColor(file, pixelColor, samplesPerPixel);
            }
        }

        Console.Error.WriteLine("\nDone.");
    }
}