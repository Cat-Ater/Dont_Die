using UnityEngine;

public class BP_ArchSpiral : BulletPattern
{
    [Range(0, 20)]
    public float t = 0; 
    public PolarPeriod2D period; 
    public float amplitude = 1;
    public float wrapping = 1;
    public int numberOfPoints = 10;

    public override Vector2[] GetPattern()
    {
        return PolarCoordBuilder.ArchimedeanSpiral(amplitude, wrapping, numberOfPoints, period);
    }
}
