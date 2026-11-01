using System;
using JetBrains.Annotations;
using Unity.Burst;
using UnityEngine;

// i genuinely have no idea if making it burst compatible changes anything

namespace UnityEngine.Extended
{
    /// <summary>
    /// Represents a percentage value.
    /// <br/><br/>
    /// <b>NOTE:</b> Don't recommend implicitly nor explicitly casting because it's hard to guess whether you want to cast to a literal percentage (0.0 - 1.0) or value (X%)
    /// </summary>
    [BurstCompile]
    public readonly struct Percentage : IEquatable<float>, IComparable<Percentage>
    {
        private readonly float _value;
        private readonly float _percentageValue;

        /// <returns>
        /// The value represented as a float.
        /// </returns>
        public float value
        {
            get { return _value; }
        }

        /// <returns>
        /// The value as represented as a percentage (X%)
        /// </returns>
        public float percentageValue
        {
            get { return _percentageValue; }
        }

        /// <returns>Percentage as a multiplier.</returns>
        public float asMultiplier
        {
            get { return 1f + _value; }
        }

        /// <returns>Percentage as an increase multiplier</returns>
        public float asIncrease
        {
            get { return 1f + _value; }
        }

        /// <returns>Percentage as a decrease multiplier</returns>
        public float asDecrease
        {
            get { return 1f - _value; }
        }
        
        /// <summary>
        /// Constructs a new percentage.
        /// </summary>
        /// <param name="percentageValue">Whether it should use a percentage value (X%) or not (<i>example: 0.2</i>)</param>
        public Percentage(float value, bool percentageValue = true)
        {
            if (percentageValue)
            {
                _value = value / 100f;
                _percentageValue = value;
                return;
            }
            
            _value = value;
            _percentageValue = value * 100f;
        }

        /// <returns>Percentage as a multiplier.</returns>
        [Obsolete("Just use Percentage.asMultiplier instead unless using burst.")]
        [BurstCompile]
        public float AsMultiplier()
        {
            return 1f + _value;
        }
        
        /// <returns>Percentage as a multiplier.</returns>
        [BurstCompile]
        public static float AsMultiplier(Percentage percentage)
        {
            return percentage.asMultiplier;
        }

        /// <returns>Percentage as an increase.</returns>
        [Obsolete("Just use Percentage.asIncrease instead unless using burst.")]
        [BurstCompile]
        public float AsIncrease()
        {
            return 1f + _value;
        }

        /// <returns>Percentage as an increase.</returns>
        [BurstCompile]
        public static float AsIncrease(Percentage percentage)
        {
            return percentage.asIncrease;
        }
        
        /// <returns>Percentage as a decrease.</returns>
        [Obsolete("Just use Percentage.asDecrease instead unless using burst.")]
        [BurstCompile]
        public float AsDecrease()
        {
            return 1f - _value;
        }
        
        /// <returns>Percentage as a decrease.</returns>
        [BurstCompile]
        public static float AsDecrease(Percentage percentage)
        {
            return percentage.asDecrease;
        }

        /// <param name="value"></param>
        /// <returns>A percentage from a float.</returns>
        [BurstCompile]
        public static Percentage FromFloat(float value)
        {
            return new Percentage(value);
        }

        /// <param name="percentage"></param>
        /// <returns>A float from a percentage.</returns>
        [BurstCompile]
        public static float ToFloat(Percentage percentage)
        {
            return percentage.value;
        }
        
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>A percentage from a fraction</returns>
        /// <exception cref="ArgumentException">Thrown if the denominator is 0. (duh)</exception>
        [BurstCompile]
        public static Percentage FromFraction(float numerator, float denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be 0!!");
            
            return new Percentage(numerator / denominator);
        }
        
        /// <param name="value">The percentage value</param>
        /// <returns>
        /// A percentage from a percentage value.
        /// <br/><br/>
        /// <b>NOTE:</b> "percentage value" as in the value represented as a percentage: X%
        /// </returns>
        [BurstCompile]
        public static Percentage FromPercentageValue(float value)
        {
            return new Percentage(value / 100f);
        }

        /// <param name="value">The percentage.</param>
        /// <returns>
        /// A percentage value from a percentage.
        /// <br/><br/>
        /// <b>NOTE:</b> "percentage value" as in the value represented as a percentage: X%
        /// </returns>
        [BurstCompile]
        public static float ToPercentageValue(float value)
        {
            return value * 100f;
        }
        
        /// <param name="value">The percentage.</param>
        /// <returns>
        /// A percentage value from the current percentage.
        /// <br/><br/>
        /// <b>NOTE:</b> "percentage value" as in the value represented as a percentage: X%
        /// </returns>
        [Obsolete("Use Percentage.percentageValue instead unless using burst.")]
        [BurstCompile]
        public float ToPercentageValue()
        {
            return _value * 100f;
        }
        
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="percentageValue">Whether it should use percentage values or not.</param>
        /// <returns>The difference between 2 values as a percentage.</returns>
        [BurstCompile]
        public static Percentage Difference(float value1, float value2, bool percentageValue = false)
        {
            return new Percentage(value1 - value2, percentageValue);
        }

        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="percentageValue">Whether it should use percentage values or not.</param>
        /// <returns>The percentage change between 2 values.</returns>
        /// <exception cref="ArgumentException">Thrown if value1 is 0.</exception>
        [BurstCompile]
        public static Percentage Change(float value1, float value2, bool percentageValue = false)
        {
            if (value1 == 0)
                throw new ArgumentException("Cannot calculate change is value1 is 0.");

            return new Percentage((value2 - value1) / value1, percentageValue);
        }
        
        /// <returns>A new percentage as absolute of the original value.</returns>
        [BurstCompile]
        public Percentage Abs()
        {
            return new Percentage(Mathf.Abs(_value));
        }
        
        /// <returns>A new percentage as absolute of the original percentage.</returns>
        [BurstCompile]
        public static Percentage Abs(Percentage value)
        {
            return new Percentage(Mathf.Abs(value._value));
        }

        /// <returns>A new percentage of the absolute of a float.</returns>
        [BurstCompile]
        public static Percentage Abs(float value, bool percentageValue = true)
        {
            return new Percentage(value, percentageValue);
        }

        [BurstCompile]
        public override string ToString()
        {
            return $"{_percentageValue:F2}%";
        }

        [BurstCompile]
        public string ToString(bool percentageValue = true)
        {
            return (percentageValue) ? $"{_percentageValue:F2}" : $"{_value:F2}";
        }

        [BurstCompile]
        public override bool Equals([CanBeNull] object obj)
        {
            return obj is Percentage other && Equals(other);
        }

        // literally the same thing
        [BurstCompile]
        public bool Equals(float other)
        {
            return Equals(other);
        }
        
        [BurstCompile]
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        
        [BurstCompile]
        public int GetHashCode(bool percentageValue = false)
        {
            return percentageValue ? _percentageValue.GetHashCode() : _value.GetHashCode();
        }

        [Obsolete("Don't recommend using this, use the other CompareTo()")]
        [BurstCompile]
        public int CompareTo(Percentage other)
        {
            return _value.CompareTo(other._value);
        }

        [BurstCompile]
        public int CompareTo(Percentage other, bool percentageValue = false)
        {
            return (percentageValue)
                ? _percentageValue.CompareTo(other._percentageValue)
                : _value.CompareTo(other._value);
        }
        
        // implicit conversion from float to percentage
        public static implicit operator Percentage(float value)
        {
            return new Percentage(value, (value > 1f) ? true : false); // guessing
        }
        
        // implicit conversion from percentage to float
        public static implicit operator float(Percentage value)
        {
            return value._value;
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(Percentage left, Percentage right)
        {
            return left._value < right._value;
        }

        public static bool operator >(Percentage left, Percentage right)
        {
            return left._value > right._value;
        }

        public static bool operator <=(Percentage left, Percentage right)
        {
            return left._value <= right._value;
        }

        public static bool operator >=(Percentage left, Percentage right)
        {
            return left._value >= right._value;
        }

        public static Percentage operator +(Percentage left, Percentage right)
        {
            return new Percentage(left._value + right._value);
        }

        public static Percentage operator +(Percentage percentage, float adder)
        {
            return new Percentage(percentage._value + adder);
        }

        public static Percentage operator -(Percentage left, Percentage right)
        {
            return new Percentage(left._value - right._value);
        }

        public static Percentage operator -(Percentage percentage, float subtractor)
        {
            return new Percentage(percentage._value - subtractor);
        }

        public static Percentage operator *(Percentage left, Percentage right)
        {
            return new Percentage(left._value * right._value);
        }

        public static Percentage operator *(Percentage percentage, float multiplier)
        {
            return new Percentage(percentage._value * multiplier);
        }

        public static Percentage operator /(Percentage left, Percentage right)
        {
            if (right._value == 0)
                throw new DivideByZeroException("Right percentage in division (denominator) cannot be 0!");

            return new Percentage(left._value / right._value);
        }

        public static Percentage operator /(Percentage percentage, float divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Divisor / denominator cannot be 0!");

            return new Percentage(percentage._value / divisor);
        }

        public static Percentage operator ++(Percentage percentage)
        {
            return new Percentage(percentage._value + 1f);
        }

        public static Percentage operator --(Percentage percentage)
        {
            return new Percentage(percentage._value - 1f);
        }

        public static Percentage operator -(Percentage percentage)
        {
            return new Percentage(-percentage._value);
        }
    }
}