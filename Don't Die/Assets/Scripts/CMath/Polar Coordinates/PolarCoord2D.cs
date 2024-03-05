using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to represent Polar Coordinate points. 
/// </summary>
public sealed class PolarCoord2D
{
    /// <summary>
    /// Returns a PolarCoord2D with both r and theta set to zero.
    /// </summary>
    public static readonly PolarCoord2D None = new PolarCoord2D(0, 0);
    
    /// <summary>
    /// Returns a PolarCoord2D with r set to 1 and theta set to zero.
    /// </summary>
    public static readonly PolarCoord2D R1T0 = new PolarCoord2D(1, 0);

    /// <summary>
    /// The current radius set to the point. 
    /// </summary>
    [SerializeField] private float _r;

    /// <summary>
    /// The current theta set to the point. 
    /// </summary>
    [SerializeField] private float _theta;

    /// <summary>
    /// Returns the cartestian coordinate X position of the coordinate.  
    /// </summary>
    public float RectangularX => _r * Mathf.Cos(_theta);

    /// <summary>
    /// Returns the cartesian Y position of the coordinate. 
    /// </summary>
    public float RectangularY => _r * Mathf.Sin(_theta);

    /// <summary>
    /// CTOR:
    /// </summary>
    private PolarCoord2D()
    {
        this._r = 0;
        this._theta = 0;
    }

    /// <summary>
    /// CTOR: 
    /// </summary>
    /// <param name="r"> The radius of the point from the origin. </param>
    /// <param name="theta"> The angle of rotation applied to the point in radians. </param>
    public PolarCoord2D(float r, float theta)
    {
        _r = r;
        _theta = theta;
    }

    /// <summary>
    /// Returns the Polar Coordiante as a Cartisian coordinate Vector2. 
    /// </summary>
    public Vector2 ConvertPolarToVec() =>
        new Vector2(RectangularX, RectangularY);

    public static explicit operator Vector2(PolarCoord2D a) =>
        new Vector2(a.RectangularX, a.RectangularY);

    public static explicit operator PolarCoord2D(Vector2 a)
    {
        return new PolarCoord2D(
            Mathf.Sqrt((a.x * a.x) + (a.y * a.y)), //Calculate R. 
            Mathf.Atan(a.y / a.x) //Calculate Theta. 
            );
    }
}
