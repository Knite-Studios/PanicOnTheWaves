﻿using UnityEngine;

namespace Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Adds a Vector3 to a Vector2.
        /// </summary>
        public static Vector3 Add(this Vector2 vector2, Vector3 vector3)
            => new(vector2.x + vector3.x, vector2.y + vector3.y, vector3.z);

        /// <summary>
        /// Subtracts a Vector3 from a Vector2.
        /// </summary>
        public static Vector3 Subtract(this Vector2 vector2, Vector3 vector3)
            => new(vector2.x - vector3.x, vector2.y - vector3.y, vector3.z);

        /// <summary>
        /// Multiplies a Vector2 by a Vector3.
        /// </summary>
        public static Vector3 Multiply(this Vector2 vector2, Vector3 vector3)
            => new(vector2.x * vector3.x, vector2.y * vector3.y, vector3.z);

        /// <summary>
        /// Divides a Vector2 by a Vector3.
        /// </summary>
        public static Vector3 Divide(this Vector2 vector2, Vector3 vector3)
        {
            // Prevent division by zero.
            vector3.x = vector3.x == 0 ? 1 : vector3.x;
            vector3.y = vector3.y == 0 ? 1 : vector3.y;
            return new Vector3(vector2.x / vector3.x, vector2.y / vector3.y, vector3.z);
        }

        /// <summary>
        /// Rounds the Vector to the nearest integer.
        /// </summary>
        public static Vector2 Round(this Vector2 vector2) =>
            new(Mathf.Round(vector2.x), Mathf.Round(vector2.y));

        /// <summary>
        /// Sets the x value of a Vector2.
        /// </summary>
        public static Vector3 SetX(this Vector2 vector2, float x)
            => new(x, vector2.y);

        /// <summary>
        /// Sets the y value of a Vector2.
        /// </summary>
        public static Vector3 SetY(this Vector2 vector2, float y)
            => new(vector2.x, y);
        
        /// <summary>
        /// Converts a Vector2 to a Vector3.
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector2) =>
            new(vector2.x, vector2.y, 0);
    }

    public static class Vector3Extensions
    {
        /// <summary>
        /// Adds a Vector2 to a Vector3.
        /// </summary>
        public static Vector3 Add(this Vector3 vector3, Vector2 vector2)
            => new(vector3.x + vector2.x, vector3.y + vector2.y, vector3.z);

        /// <summary>
        /// Subtracts a Vector2 from a Vector3.
        /// </summary>
        public static Vector3 Subtract(this Vector3 vector3, Vector2 vector2)
            => new(vector3.x - vector2.x, vector3.y - vector2.y, vector3.z);

        /// <summary>
        /// Multiplies a Vector3 by a Vector2.
        /// </summary>
        public static Vector3 Multiply(this Vector3 vector3, Vector2 vector2)
            => new(vector3.x * vector2.x, vector3.y * vector2.y, vector3.z);

        /// <summary>
        /// Divides a Vector3 by a Vector2.
        /// </summary>
        public static Vector3 Divide(this Vector3 vector3, Vector2 vector2)
        {
            // Prevent division by zero.
            vector2.x = vector2.x == 0 ? 1 : vector2.x;
            vector2.y = vector2.y == 0 ? 1 : vector2.y;
            return new Vector3(vector3.x / vector2.x, vector3.y / vector2.y, vector3.z);
        }

        /// <summary>
        /// Returns a new Vector3 with the x, y, and z values overridden if the new Vector3 values are not zero.
        /// </summary>
        /// <param name="vector3">The original Vector3.</param>
        /// <param name="newVector3">The new Vector3 to override the original Vector3.</param>
        /// <returns>Overridden Vector3.</returns>
        public static Vector3 OverrideNonZero(this Vector3 vector3, Vector3 newVector3)
            => new(newVector3.x != 0 ? newVector3.x : vector3.x,
                newVector3.y != 0 ? newVector3.y : vector3.y,
                newVector3.z != 0 ? newVector3.z : vector3.z);

        /// <summary>
        /// Sets the x value of a Vector3.
        /// </summary>
        public static Vector3 SetX(this Vector3 vector3, float x)
            => new(x, vector3.y, vector3.z);

        /// <summary>
        /// Sets the y value of a Vector3.
        /// </summary>
        public static Vector3 SetY(this Vector3 vector3, float y)
            => new(vector3.x, y, vector3.z);

        /// <summary>
        /// Sets the z value of a Vector3.
        /// </summary>
        public static Vector3 SetZ(this Vector3 vector3, float z)
            => new(vector3.x, vector3.y, z);

        /// <summary>
        /// Converts a Vector3 to a Vector2.
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector3) =>
            new(vector3.x, vector3.y);

        /// <summary>
        /// Converts a Vector3 to a Vector2.
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector3 vector3) =>
            new(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));

        /// <summary>
        /// Converts a Vector3Int to a Vector2Int.
        /// </summary>
        public static Vector2Int ToVector2(this Vector3Int vector3) =>
            new(vector3.x, vector3.y);
    }
}
