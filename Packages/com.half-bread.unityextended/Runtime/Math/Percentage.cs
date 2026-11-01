using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UnityEngine.Extended
{
    /// <summary>
    /// Represents a percentage value.
    /// <br/><br/>
    /// <b>NOTE:</b> Don't recommend implicitly nor explicitly casting because it's hard to guess whether you want to cast to a literal percentage (0.0 - 1.0) or value (X%)
    /// </summary>
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
        [Obsolete("Just use Percentage.asMultiplier instead.")]
        public float AsMultiplier()
        {
            return 1f + _value;
        }
        
        /// <returns>Percentage as a multiplier.</returns>
        public static float AsMultiplier(Percentage percentage)
        {
            return percentage.asMultiplier;
        }

        /// <returns>Percentage as an increase.</returns>
        [Obsolete("Just use Percentage.asIncrease instead.")]
        public float AsIncrease()
        {
            return 1f + _value;
        }

        /// <returns>Percentage as an increase.</returns>
        public static float AsIncrease(Percentage percentage)
        {
            return percentage.asIncrease;
        }
        
        [Obsolete("Just use Percentage.asDecrease instead.")]
        /// <returns>Percentage as a decrease.</returns>
        public float AsDecrease()
        {
            return 1f - _value;
        }
        
        /// <returns>Percentage as a decrease.</returns>
        public static float AsDecrease(Percentage percentage)
        {
            return percentage.asDecrease;
        }

        /// <param name="value"></param>
        /// <returns>A percentage from a float.</returns>
        public static Percentage FromFloat(float value)
        {
            return new Percentage(value);
        }

        /// <param name="percentage"></param>
        /// <returns>A float from a percentage.</returns>
        public static float ToFloat(Percentage percentage)
        {
            return percentage.value;
        }
        
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>A percentage from a fraction</returns>
        /// <exception cref="ArgumentException">Thrown if the denominator is 0. (duh)</exception>
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
        [Obsolete("Use Percentage.percentageValue instead.")]
        public float ToPercentageValue()
        {
            return _value * 100f;
        }
        
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="percentageValue">Whether it should use percentage values or not.</param>
        /// <returns>The difference between 2 values as a percentage.</returns>
        public static Percentage Difference(float value1, float value2, bool percentageValue = false)
        {
            return new Percentage(value1 - value2, percentageValue);
        }
        
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="percentageValue">Whether it should use percentage values or not.</param>
        /// <returns>The percentage change between 2 values.</returns>
        /// <exception cref="ArgumentException">Thrown if value1 is 0.</exception>
        public static Percentage Change(float value1, float value2, bool percentageValue = false)
        {
            if (value1 == 0)
                throw new ArgumentException("Cannot calculate change is value1 is 0.");
            
            return new Percentage((value2 - value1) / value1, percentageValue);
        }
        
        public override string ToString()
        {
            return $"{_percentageValue:F2}%";
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

        public override bool Equals([CanBeNull] object obj)
        {
            return obj is Percentage other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        
        public int GetHashCode(bool percentageValue = false)
        {
            return percentageValue ? _percentageValue.GetHashCode() : _value.GetHashCode();
        }
    }
}