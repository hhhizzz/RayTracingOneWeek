namespace RayTracingOneWeek;

using Color = Vec3;

public class ColorRender
{
    public static void WriteColor(StreamWriter writer, Color pixelColor, int samplesPerPixel)
    {
        var r = pixelColor.X;
        var g = pixelColor.Y;
        var b = pixelColor.Z;

        var scale = 1.0 / samplesPerPixel;
        r = Math.Sqrt(scale * r);
        g = Math.Sqrt(scale * g);
        b = Math.Sqrt(scale * b);

        var rInt = (int)(256 * Math.Clamp(r, 0.0, 0.999));
        var gInt = (int)(256 * Math.Clamp(g, 0.0, 0.999));
        var bInt = (int)(256 * Math.Clamp(b, 0.0, 0.999));

        writer.WriteLine($"{rInt} {gInt} {bInt}");
    }
}