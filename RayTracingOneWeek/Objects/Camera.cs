namespace RayTracingOneWeek.Objects;

using Point3 = Vec3;

public class Camera
{
    private readonly Point3 _origin;
    private readonly Point3 _lowerLeftCorner;
    private readonly Vec3 _horizontal;
    private readonly Vec3 _vertical;

    public Camera()
    {
        var aspectRatio = 16.0 / 9.0;
        var viewportHeight = 2.0;
        var viewportWidth = aspectRatio * viewportHeight;
        var focalLength = 1.0;

        _origin = new Point3(0, 0, 0);
        _horizontal = new Point3(viewportWidth, 0, 0);
        _vertical = new Point3(0, viewportHeight, 0);
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - new Point3(0, 0, focalLength);
    }

    public Ray GetRay(double u, double v)
    {
        return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
    }
}