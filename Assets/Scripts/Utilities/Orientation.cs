using UnityEngine;

namespace slaughter.de.Utilities
{
    public enum Orientation
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class OrientationExtensions
    {
        public static Vector3 GetTranslateDirection(this Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Up => Vector3.up,
                Orientation.Down => Vector3.down,
                Orientation.Left => Vector3.left,
                _ => Vector3.right
            };
        }

        public static void SetTransformDirection(this Orientation orientation, Vector3 direction, Transform transform)
        {
            switch (orientation)
            {
                case Orientation.Up:
                    transform.up = direction;
                    break;
                case Orientation.Down:
                    transform.up = -direction;
                    break;
                case Orientation.Left:
                    transform.right = -direction;
                    break;
                default:
                    transform.right = direction;
                    break;
            }
        }
    }
}