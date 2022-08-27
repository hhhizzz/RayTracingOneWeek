namespace RayTracingOneWeek.Objects;

using Point3 = Vec3;

public class Camera
{
    private readonly Point3 _origin;
    private readonly Point3 _lowerLeftCorner;
    private readonly Vec3 _horizontal;
    private readonly Vec3 _vertical;

    public Camera(Point3 lookFrom, Point3 lookAt, Vec3 vUp, double vFov, double aspectRatio)
    {
        var theta = Utility.DegreesToRadians(vFov);
        var h = Math.Tan(theta / 2);
        var viewportHeight = 2.0 * h;
        var viewportWidth = aspectRatio * viewportHeight;

        var w = (lookFrom - lookAt).UnitVector();
        var u = Vec3.Cross(vUp, w).UnitVector();
        var v = Vec3.Cross(w, u);

        _origin = lookFrom;
        _horizontal = viewportWidth * u;
        _vertical = viewportHeight * v;
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - w;
    }

    public Ray GetRay(double u, double v)
    {
        return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
    }
}