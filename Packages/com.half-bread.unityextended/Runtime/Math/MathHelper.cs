using System;
using UnityEngine;
using Unity.Burst;
using UnityEngineInternal;

namespace UnityEngine.Extended
{
    /// <summary>
    /// Global utility for math stuff with optimizations to make everything as fast as possible
    /// <br/><br/>
    /// No more separation between UnityEngine.Mathf, System.Math and MathF! :D
    /// </summary>
    [BurstCompile]
    public static partial class MathHelper
    {
        // Mathf reference
        static MathHelper()
        {
            
        }

        public const float Deg2Rad = Mathf.Deg2Rad;
        public const float Rad2Deg = Mathf.Deg2Rad;

        public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled)
            ? MathfInternal.FloatMinNormal
            : MathfInternal.FloatMinDenormal;

        public static readonly float ApproxEpsilon = Epsilon * 8f;

        // dont ask why
        public static float E = Epsilon;

        // we use MathF because it actually performs floating point calculations under the hood
        // unity est visee

        public static float Abs(float value)
        {
            return MathF.Abs(value);
        }

        public static float Sin(float value)
        {
            return MathF.Sin(value);
        }

        public static float Sinh(float value)
        {
            return MathF.Sinh(value);
        }

        public static float Asin(float value)
        {
            return MathF.Asin(value);
        }

        public static float Asinh(float value)
        {
            return MathF.Asinh(value);
        }

        public static float Cos(float value)
        {
            return MathF.Cos(value);
        }

        public static float Cosh(float value)
        {
            return MathF.Cosh(value);
        }

        public static float Acos(float value)
        {
            return MathF.Acos(value);
        }

        public static float Acosh(float value)
        {
            return MathF.Acosh(value);
        }

        public static float Tan(float value)
        {
            return MathF.Tan(value);
        }

        public static float Tanh(float value)
        {
            return MathF.Tanh(value);
        }

        public static float Atan(float value)
        {
            return MathF.Atan(value);
        }

        public static float Atanh(float value)
        {
            return MathF.Atanh(value);
        }

        public static float Atan2(float x, float y)
        {
            return MathF.Atan2(x, y);
        }

        public static float Min(float a, float b)
        {
            return MathF.Min(a, b);
        }

        public static float Max(float a, float b)
        {
            return MathF.Max(a, b);
        }

        /// <returns>The distance (absolute difference) between 2 values.</returns>
        public static float Distance(float a, float b)
        {
            return MathF.Abs(a - b);
        }

        /// <returns>The absolute difference between 2 values.</returns>
        [Obsolete("Just use Distance() instead.")]
        public static float AbsDiff(float a, float b)
        {
            return MathF.Abs(a - b);
        }

        public static float Ceil(float value)
        {
            return MathF.Ceiling(value);
        }

        public static int Ceil2Int(float value)
        {
            return (int)MathF.Ceiling(value);
        }

        public static float Floor(float value)
        {
            return MathF.Floor(value);
        }

        public static int Floor2Int(float value)
        {
            return (int)MathF.Floor(value);
        }

        public static float Clamp(float value, float min, float max)
        {
            return (float)Math.Clamp(value, min, max); // no idea why MathF doesn't have Clamp :|
        }

        public static float Clamp01(float value)
        {
            return Clamp(value, 0f, 1f);
        }

        public static bool Approximately(float a, float b)
        {
            return Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), ApproxEpsilon);
        }
    }
}