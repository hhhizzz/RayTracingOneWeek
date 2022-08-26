namespace RayTracingOneWeek;
//Image

using Color = Vec3;
using Point3 = Vec3;

public static class Program
{
    private static Color RayColor(Ray r)
    {
        Vec3 unitDirection = r.Direction.UnitVector();
        double t = 0.5 * (unitDirection.Y + 1.0);
        return (1.0 - t) * new Color(1, 1, 1) + t * new Color(0.5, 0.7, 1.0);
    }

    public static void Main(string[] args)
    {
        //Image
        const double aspectRatio = (double)16 / 9;
        const int imageWidth = 400;
        const int imageHeight = (int)(imageWidth / aspectRatio);

        //Camera
        var viewportHeight = 2.0;
        var viewportWidth = aspectRatio * viewportHeight;
        var focalLength = 1.0;

        var origin = new Point3(0, 0, 0);
        var horizontal = new Vec3(viewportWidth, 0, 0);
        var vertical = new Vec3(0, viewportHeight, 0);
        var lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vec3(0, 0, focalLength);

        using StreamWriter file = new("image.ppm");
        //Render
        file.Write($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (var j = imageHeight - 1; j >= 0; j--)
        {
            Console.Error.Write($"\rScan lines remaining: {j}");
            Console.Error.Flush();
            for (var i = 0; i < imageWidth; i++)
            {
                var u = (double)i / (imageWidth - 1);
                var v = (double)j / (imageHeight - 1);
                Ray r = new(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
                Color pixelColor = RayColor(r);
                ColorRender.WriteColor(file, pixelColor);
            }
        }

        Console.Error.WriteLine("\nDone.");
    }
}