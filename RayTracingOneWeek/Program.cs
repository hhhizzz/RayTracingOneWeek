using RayTracingOneWeek.Objects;

namespace RayTracingOneWeek;
//Image

using Color = Vec3;
using Point3 = Vec3;

public static class Program
{
    private static Color RayColor(Ray r, IHitTable world)
    {
        if (world.Hit(r, 0, double.MaxValue, out var rec))
        {
            return 0.5 * (rec.Normal + new Color(1, 1, 1));
        }

        var unitDirection = r.Direction.UnitVector();
        var t = 0.5 * (unitDirection.Y + 1.0);
        return (1.0 - t) * new Color(1, 1, 1) + t * new Color(0.5, 0.7, 1.0);
    }

    private static double HitSphere(Point3 center, double radius, Ray r)
    {
        Vec3 oc = r.Origin - center;
        double a = r.Direction.LengthSquared();
        double b = 2 * Vec3.Dot(oc, r.Direction);
        double c = oc.LengthSquared() - radius * radius;
        double discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return -1;
        }
        else
        {
            return (-b - Math.Sqrt(discriminant)) / (2.0 * a);
        }
    }

    public static void Main(string[] args)
    {
        //Image
        const double aspectRatio = (double)16 / 9;
        const int imageWidth = 400;
        const int imageHeight = (int)(imageWidth / aspectRatio);
        const int samplesPerPixel = 100;

        //World
        var world = new HitTableList();
        world.Add(new Sphere(new Point3(0, 0, -1), 0.5f));
        world.Add(new Sphere(new Point3(0, -100.5, -1), 100));

        //Camera
        Camera camera = new Camera();

        //Render
        using StreamWriter file = new("image.ppm");
        file.Write($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (var j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.Write($"\rScan lines remaining: {j}");
            Console.Error.Flush();
            for (var i = 0; i < imageWidth; i++)
            {
                Color pixelColor = new(0, 0, 0);
                for (var s = 0; s < samplesPerPixel; s++)
                {
                    var u = (i + Utility.RandomDouble()) / (imageWidth - 1);
                    var v = (j + Utility.RandomDouble()) / (imageHeight - 1);
                    Ray r = camera.GetRay(u, v);
                    pixelColor += RayColor(r, world);
                }

                ColorRender.WriteColor(file, pixelColor, samplesPerPixel);
            }
        }

        Console.Error.WriteLine("\nDone.");
    }
}