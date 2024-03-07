using UnityEngine;

public class Box
{
    public Box()
    {
    }

    public Vector3 center { get; set; }
    public float height { get; set; }
    public float width { get; set; }
    public Vector2 GetMin { get => new Vector2(center.x - (width/2), center.y - (height/2)); }
    public Vector2 GetMax { get => new Vector2(center.x + (width / 2), center.y + (height / 2)); }

    public void OnDraw()
    {
        Gizmos.DrawCube(center, new Vector3(width, height));
    }
}