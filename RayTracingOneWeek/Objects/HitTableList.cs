namespace RayTracingOneWeek.Objects;

public class HitTableList : IHitTable
{
    private readonly List<IHitTable> _hitTables;

    public HitTableList()
    {
        _hitTables = new List<IHitTable>();
    }

    public void Add(IHitTable hitTable)
    {
        _hitTables.Add(hitTable);
    }

    public void Clear()
    {
        _hitTables.Clear();
    }

    public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec)
    {
        rec = new HitRecord();
        var hitAnything = false;
        var closestSoFar = tMax;

        foreach (var hitTable in _hitTables)
        {
            if (hitTable.Hit(r, tMin, closestSoFar, out var tempRec))
            {
                hitAnything = true;
                closestSoFar = tempRec.T;
                rec = tempRec;
            }
        }

        return hitAnything;
    }
}