using System;
using UnityEngine;

namespace UnityEngine.Extended
{
    /// <summary>
    /// Stores a reference type value (managed) that can only be set once
    /// via <c>.Assign()</c> / <c>.TryAssign()</c>
    /// <br/><br/>
    /// Fixes the problem of readonly fields / getter properties not being easily functional
    /// due to Unity using functions like <c>Start()</c>.
    /// </summary>
    /// <typeparam name="T">The value type of the value to be stored.</typeparam>
    public class Once<T> where T : class, new()
    {
        /// <summary>
        /// The stored value.
        /// </summary>
        public T value { get; private set; }
        
        /// <summary>
        /// Assign the value.
        /// <br/>
        /// If it fails, an exception is thrown.
        /// </summary>
        /// <param name="value">The value to be assigned</param>
        /// <exception cref="InvalidOperationException">Thrown if the value has already been assigned</exception>
        /// <exception cref="ArgumentNullException">Thrown if the value parameter is null</exception>
        public void Assign(T value)
        {
            if (this.value != null)
                throw new InvalidOperationException("Value has already been assigned.");

            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Attempts to assign the value.
        /// <br/>
        /// If it fails, it quietly returns.
        /// </summary>
        /// <param name="value">The value to be assigned</param>
        public void TryAssign(T value)
        {
            if (this.value != null)
                return;
            
            this.value = value;
        }
    }
}