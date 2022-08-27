namespace RayTracingOneWeek.Objects;

using Point3 = Vec3;

public class Camera
{
    private readonly Point3 _origin;
    private readonly Point3 _lowerLeftCorner;
    private readonly Vec3 _horizontal;
    private readonly Vec3 _vertical;
    private readonly Vec3 _u, _v, _w;
    private readonly double _lensRadius;

    public Camera(Point3 lookFrom, Point3 lookAt, Vec3 vUp, double vFov, double aspectRatio, double aperture,
        double focusDist)
    {
        var theta = Utility.DegreesToRadians(vFov);
        var h = Math.Tan(theta / 2);
        var viewportHeight = 2.0 * h;
        var viewportWidth = aspectRatio * viewportHeight;

        _w = (lookFrom - lookAt).UnitVector();
        _u = Vec3.Cross(vUp, _w).UnitVector();
        _v = Vec3.Cross(_w, _u);

        _origin = lookFrom;
        _horizontal = focusDist * viewportWidth * _u;
        _vertical = focusDist * viewportHeight * _v;
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - focusDist * _w;

        _lensRadius = aperture / 2;
    }

    public Ray GetRay(double s, double t)
    {
        var rd = _lensRadius * Utility.RandomInUnitDisk();
        var offset = _u * rd.X + _v * rd.Y;
        return new Ray(_origin + offset, _lowerLeftCorner + s * _horizontal + t * _vertical - _origin - offset);
    }
}