using System;
using System.Configuration.Internal;

namespace UnityEngine.Extended
{
    /// <summary>
    /// Stores a value type value (unmanaged) that can only be set once
    /// via <c>.Assign()</c> / <c>.TryAssign()</c>
    /// <br/><br/>
    /// Fixes the problem of readonly fields / getter properties not being easily functional
    /// due to Unity using functions like <c>Start()</c>.
    /// </summary>
    /// <typeparam name="T">The reference type of the value to be stored.</typeparam>
    public struct NativeOnce<T> where T : struct
    {
        public T? value { get; private set; }

        public void Assign(T value)
        {
            if (this.value != null)
                throw new InvalidOperationException("Value has already been assigned");

            this.value = value;
        }

        public void TryAssign(T value)
        {
            if (this.value != null)
                return;
            
            this.value = value;
        }
    }
}