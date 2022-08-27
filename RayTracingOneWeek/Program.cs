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

    private static HitTableList RandomScene()
    {
        HitTableList world = new();

        var groundMaterial = new DiffuseMaterial(new Color(0.5, 0.5, 0.5));
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, groundMaterial));

        for (int i = -11; i < 11; i++)
        {
            for (int b = 0; b < 11; b++)
            {
                var chooseMat = Utility.RandomDouble();
                var center = new Point3(i + 0.9 * Utility.RandomDouble(), 0.2, b + 0.9 * Utility.RandomDouble());

                if ((center - new Point3(4, 0.2, 0)).Length() > 0.9)
                {
                    IMaterial sphereMaterial = chooseMat switch
                    {
                        < 0.8 => new DiffuseMaterial(new Color(Utility.RandomDouble() * Utility.RandomDouble(),
                            Utility.RandomDouble() * Utility.RandomDouble(),
                            Utility.RandomDouble() * Utility.RandomDouble())),
                        < 0.95 => new MetalMaterial(new Color(0.5 * (1 + Utility.RandomDouble()),
                            0.5 * (1 + Utility.RandomDouble()),
                            0.5 * (1 + Utility.RandomDouble())), 0.5 * Utility.RandomDouble()),
                        _ => new DielectricMaterial(1.5)
                    };
                    world.Add(new Sphere(center, 0.2, sphereMaterial));
                }
            }
        }

        var material1 = new DielectricMaterial(1.5);
        world.Add(new Sphere(new Point3(0, 1, 0), 1, material1));

        var material2 = new DiffuseMaterial(new Color(0.4, 0.2, 0.1));
        world.Add(new Sphere(new Point3(-4, 1, 0), 1, material2));

        var material3 = new MetalMaterial(new Color(0.7, 0.6, 0.5), 0.0);
        world.Add(new Sphere(new Point3(4, 1, 0), 1, material3));

        return world;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public static void Main(string[] args)
    {
        //Image
        using StreamWriter file = new("image.ppm");
        const double aspectRatio = (double)3 / 2;
        const int imageWidth = 1200;
        const int imageHeight = (int)(imageWidth / aspectRatio);
        const int samplesPerPixel = 500;
        const int maxDepth = 50;

        //World
        var world = RandomScene();

        //Camera
        Point3 lookFrom = new(13, 2, 3);
        Point3 lookAt = new(0, 0, 0);
        Vec3 vUp = new(0, 1, 0);
        double distToFocus = 10;
        double aperture = 0.1;
        Camera camera = new Camera(lookFrom, lookAt, vUp, 20, aspectRatio, aperture, distToFocus);

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