using C_Math.Collision;
using C_Math.ShapeRandomisation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace C_Math
{
    public static class C_MathF
    {
        public const double E = 2.7182818284590451D;
        
        public const double PI = 3.1415926535897931D;
        
        public const double Rad2Deg = 180F / PI;
        
        public const double Deg2Rad = PI / 180F;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float value) => Mathf.Cos(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float value) => Mathf.Sin(value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sec(float value) => 1 / Mathf.Cos(value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cot(float value) => Mathf.Cos(value) / Mathf.Sin(value);

        /// <summary>
        /// Limit a value to the passed maximum.
        /// </summary>
        /// <param name="value"> The value to limit. </param>
        /// <param name="max"> The maximum that value can be.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float value, float max) =>
            (value > max) ? max : value;

        /// <summary>
        /// Limit a value above a minimum threshold. 
        /// </summary>
        /// <param name="value"> The value to limit. </param>
        /// <param name="min"> The minimum vaue threshold. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float value, float min) =>
            (value < min) ? min : value;

        /// <summary>
        /// Return the absolute value of a number.
        /// </summary>
        /// <param name="value"> The value to absolute. </param>
        /// <returns> The value with the sign set to positive. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value) =>
            ((value < 0) ? -1 : 1) * value;

        /// <summary>
        /// Returns a value clamped within the range [Min, Max]. 
        /// </summary>
        /// <param name="value"> The inital value to clamp. </param>
        /// <param name="min">   The minimum range. </param>
        /// <param name="max">   The maximum range. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max) =>
            (value < min) ? min : (value > max) ? max : value;

        /// <summary>
        /// Returns a value clamped within the range [Min, Max]. 
        /// </summary>
        /// <param name="value"> The inital value to clamp. </param>
        /// <param name="range">   The range [Min, Max]. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, Vector2 range) =>
            Clamp(value, range.x, range.y);

        public static Vector3 AngleToUnitCirclePoint(float angle)
        {
            float angleR = Mathf.Deg2Rad * angle;
            return new Vector3(Mathf.Cos(angleR), Mathf.Sin(angleR));
        }

        public static Vector3 ToUnitCirclePoint(this float angle)
        {
            float angleR = Mathf.Deg2Rad * angle;
            return new Vector3(Mathf.Cos(angleR), Mathf.Sin(angleR));
        }

        public static Vector3[] GetPointsOnCircle(this Circle2D circle, int count, Vector3 center)
        {
            Vector3[] positions = new Vector3[count];
            float initialOffset = circle.initial;
            float stepRate = 360 / count;
            float currentStep = initialOffset;

            for (int i = 0; i < count; ++i)
            {
                currentStep += stepRate;
                Vector3 point = currentStep.ToUnitCirclePoint();
                positions[i] = center + (currentStep.ToUnitCirclePoint() * circle.radius);
            }
            return positions;
        }

        public static Vector3[] RandomiseOnCircle(this Circle2D circle, int count, Vector3 center)
        {
            Vector3[] positions = new Vector3[count];
            float initialOffset = UnityEngine.Random.Range(0, 90);
            float stepRate = 360 / count;
            float currentStep;

            for (int i = 0; i < count; ++i)
            {
                currentStep = (i * stepRate) + initialOffset + UnityEngine.Random.Range(30, 270);
                positions[i] = center + (currentStep.ToUnitCirclePoint() * circle.radius);
            }

            return positions;
        }

        public static void UpdateIncrement(this Circle2D circle)
        {
            circle.initial += (circle.antiClockwise) ? circle.increment : -circle.increment;
            circle.initial %= 360;
        }

        public static float GetLineMidpoint(Vector2 vec)
        {
            return vec.magnitude / 2;
        }
    }

    namespace Collision
    {
        public static class AABB
        {
            public static bool AABBCollisionCheck(Box box, Vector2 position)
            {
                Vector2 min = box.GetMin;
                Vector2 max = box.GetMax;

                if ((min.x < position.x && max.x > position.x) &&
                    (min.y < position.y && max.y > position.y))
                {
                    return true;
                }
                return false;
            }

            //TODO: TEST THIS FUNCTION. 
            public static bool AABBCollisionCheck(Box a, Box b)
            {
                Vector2 minA = a.GetMin;
                Vector2 maxA = a.GetMax;
                Vector2 minB = b.GetMin;
                Vector2 maxB = b.GetMax;

                if ((minA.x < minB.x && maxA.x > maxB.x) &&
                    (minA.y < minB.y && maxA.y > minB.y))
                {
                    return true;
                }
                return false;
            }
        }

        public static class Radial
        {
            public static bool RadialCheckInsideSphere(Sphere sphere, Vector3 position)
            {
                //Get the two positions. 
                Vector2 spherePos = sphere.center;
                Vector2 objectPos = position;
                                
                //Get the range of the sphere. 
                float range = sphere.radius;

                //Get the distance between the two objects. 
                float magnitude = Mathf.Abs((spherePos - objectPos).magnitude);

                ////Debug the results. 
                //Debug.Log("Detection Radius: " +  range);
                //Debug.Log("Current Distance: " +  magnitude);
                //Debug.Log("Collision Result: " + (magnitude >= range));
                return (magnitude < range);
            }
        }
    }

    namespace ShapeRandomisation
    {
        public class Arc2D
        {
            public float minArc; 
            public float maxArc;
            public float radius;
        }

        [System.Serializable]
        public class Circle2D
        {
            public float radius;
            public float initial = 0;
            [Range(0, 360)]
            public float increment;
            public bool antiClockwise = false; 
        }

        public static class SphericalRandomisation
        {
            private static Vector3 GenerateRandomUnitArcPoint(Arc2D arc)
            {
                float angleR = UnityEngine.Random.Range(arc.minArc, arc.maxArc) * Mathf.Deg2Rad;
                return new Vector3(Mathf.Cos(angleR), Mathf.Sin(angleR), 0) * arc.radius;
            }

            public static Vector3[] RandomiseSequentialOnSphere(float radius, int count, Vector3 center)
            {
                Vector3[] positions = new Vector3[count];
                float initialOffset = UnityEngine.Random.Range(0, 360);
                float stepRate = 360 / count;
                float currentStep = initialOffset; 

                for (int i = 0; i < count; ++i)
                {
                    currentStep += stepRate;
                    Vector3 point = currentStep.ToUnitCirclePoint();
                    positions[i] = center + ( new Vector3(point.x * radius, point.y * radius));
                }

                return positions;
            }

            public static Vector3[] RandomiseOnSphere(float radius, int count, Vector3 center)
            {
                Vector3[] positions = new Vector3[count];
                float initialOffset = UnityEngine.Random.Range(0, 180);
                float stepRate = 360 / count;
                float currentStep;

                for (int i = 0; i < count; ++i)
                {
                    currentStep = (i * stepRate) + initialOffset + UnityEngine.Random.Range(30, 270);
                    positions[i] = currentStep.ToUnitCirclePoint() * radius;
                }

                return positions;
            }

            public static Vector3[] RandomiseOnArc(Arc2D arc, int count)
            {
                Vector3[] points = new Vector3[count];

                for (int i = 0; i < count; ++i)
                {
                    points[i] = GenerateRandomUnitArcPoint(arc);
                }

                return points;
            }
        }
    }
}
