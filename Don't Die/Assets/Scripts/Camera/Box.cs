using UnityEngine;

namespace C_Math
{
    [System.Serializable]
    public class BoxDataComponent
    {
        public float width;
        public float height;

        public Box GetBox(Vector3 position)
        {
            return new Box()
            {
                center = position,
                height = height,
                width = width
            };
        }
    }

    [System.Serializable]
    public class SphereDataComponent
    {
        public float radius;

        public Sphere GetSphere(Vector3 position)
        {
            return new Sphere()
            {
                center = position,
                radius = radius,
            };
        }
    }

    public class Box
    {
        public Box()
        {
        }

        public Vector3 center { get; set; }
        public float height { get; set; }
        public float width { get; set; }
        public Vector2 GetMin { get => new Vector2(center.x - (width / 2), center.y - (height / 2)); }
        public Vector2 GetMax { get => new Vector2(center.x + (width / 2), center.y + (height / 2)); }

        public void OnDraw()
        {
            Gizmos.DrawWireCube(center, new Vector3(width, height));
        }
    }

    public class Sphere
    {
        public Sphere() { }

        public Vector3 center { get; set; }

        public float radius { get; set; }

        public void OnDraw()
        {
            Gizmos.DrawWireSphere(center, radius);
        }
    }
}

