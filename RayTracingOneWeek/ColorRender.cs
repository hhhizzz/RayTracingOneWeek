namespace RayTracingOneWeek;

using Color = Vec3;

public class ColorRender
{
    public static void WriteColor(StreamWriter writer, Color pixelColor)
    {
        writer.WriteLine(
            $"{(int)(255.999 * pixelColor.X)} {(int)(255.999 * pixelColor.Y)} {(int)(255.999 * pixelColor.Z)}");
    }
}