namespace RayTracingOneWeek;
//Image

using Color = Vec3;

public static class Program
{
    const int ImageWidth = 256;
    const int ImageHeight = 256;

    public static void Main(string[] args)
    {
        using StreamWriter file = new("image.ppm");
//Render
        file.Write($"P3\n{ImageWidth} {ImageHeight}\n255\n");

        for (var j = ImageHeight - 1; j >= 0; j--)
        {
            Console.Error.Write($"\rScan lines remaining: {j}");
            Console.Error.Flush();
            for (var i = 0; i < ImageWidth; i++)
            {
                Color pixelColor = new((double)(i) / (ImageWidth - 1), (double)(j) / (ImageHeight - 1), 0.25);
                ColorRender.WriteColor(file, pixelColor);
            }
        }

        Console.Error.WriteLine("\nDone.");
    }
}