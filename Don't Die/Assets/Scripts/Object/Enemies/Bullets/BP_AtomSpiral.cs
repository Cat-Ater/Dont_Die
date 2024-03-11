using UnityEngine;

public class BP_AtomSpiral : BulletPattern
{
    public PolarPeriod2D period;
    public float amplitude = 1;
    public float wrapping = 1;
    public int numberOfPoints = 10;

    public override Vector2[] GetPattern()
    {
        return PolarCoordBuilder.AtomSpiral(amplitude, numberOfPoints, period);
    }
}
