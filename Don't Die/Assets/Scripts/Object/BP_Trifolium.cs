using UnityEngine;

public class BP_Trifolium : BulletPattern
{
    public PolarPeriod2D period;
    public float amplitude = 1;
    public int numberOfPoints = 10;

    public override Vector2[] GetPattern()
    {
        return PolarCoordBuilder.Trifolium(amplitude, numberOfPoints, period);
    }
}