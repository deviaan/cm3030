using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEditor;

namespace CharacterController
{
    public struct RayRange
    {
        public RayRange(Bounds bounds, Vector2 dir, float buffer)
        {
            var x1 = dir.x > 0 ? bounds.max.x : bounds.min.x;
            var y1 = dir.y > 0 ? bounds.max.y : bounds.min.y;
            var x2 = dir.x < 0 ? bounds.min.x : bounds.max.x;
            var y2 = dir.y < 0 ? bounds.min.y : bounds.max.y;

            if (dir.y != 0)
            {
                x1 += buffer;
                x2 -= buffer;
            }

            if (dir.x != 0)
            {
                y1 += buffer;
                y2 -= buffer;
            }
            
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }

        public readonly Vector2 Start, End, Dir;
    }

    public enum JumpButtonState
    {
        Pressed,
        Released,
        Neutral
    }
} 