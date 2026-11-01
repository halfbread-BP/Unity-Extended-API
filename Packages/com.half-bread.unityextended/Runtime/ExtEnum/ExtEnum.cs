using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UnityEngine.Extended
{
    /// <summary>
    /// A modular and modding-friendly version of the enum.
    /// <br/><br/>
    /// <b>NOTE:</b> If you believe the enum shouldn't really be relevant in any modding application and/or be static, then it's usually better to use regular enum's as it is <i>much</i> less verbose 
    /// </summary>
    /// <tutorial>
    /// Here's a tutorial for how to use extenums:
    /// <br/><br/>
    /// How you'd traditionally define an enum:
    /// <code>
    /// public enum Test
    /// {
    ///     Hello, World
    /// }
    /// </code>
    /// <br/>
    /// And here's how you'd define an ExtEnum equivalent:
    /// <code>
    /// public class Test : ExtEnum&lt;Test&gt;
    /// {
    ///     public static readonly Test Hello = new("Hello");
    ///     public static readonly Test World = new("World");
    /// 
    ///     public Test(string value, bool register = true) : base(value, register)
    ///     { }
    /// }
    /// </code>
    /// <br/>
    /// Now because of issues that are on a compilation level, somewhere early in your code you have to call: <br/>
    /// <c>ExtEnumInitializer.InitTypes&lt;MyExtEnum&gt;();</c> <br/>
    /// To actually cache it into memory and wtv.
    /// <br/><br/>
    /// And here's how to add entries to an existing enum:
    /// <code>
    /// public static class MyModdedEnum
    /// {
    ///     public static CreatureTemplate.Type NewCreature;
    ///     public static CreatureTemplate.Type AnotherCreature;
    /// 
    ///     public static void RegisterValues()
    ///     {
    ///         NewCreature = new CreatureTemplate.Type("NewCreature", true);
    ///         AnotherCreature = new CreatureTemplate.Type("AnotherCreature, true);
    ///     }
    /// 
    ///     public static void UnregisterValues()
    ///     {
    ///         if (NewCreature is not null)
    ///         {
    ///             NewCreature.Unregister();
    ///             NewCreature = null;
    ///         }
    ///         if (AnotherCreature is not null)
    ///         {
    ///             AnotherCreature.Unregister();
    ///             AnotherCreature = null;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </tutorial>
    /// <remarks>
    /// <i>Oh and if you're curious, ExtEnum is an abbreviation of Extended Enumerator, if that wasn't already obvious.</i>
    /// </remarks>
    /// <typeparam name="T">Your derived class. Self-referencing, e.g: <c>class Test : ExtEnum&lt;Test&gt;</c></typeparam>
    public abstract class ExtEnum<T> where T : ExtEnum<T> // make it so the inheriting class has to fill in itself as a generic... almost
    {
        /// <summary>
        /// The string identifier of this entry.
        /// </summary>
        public readonly string value;

        /// <summary>
        /// The position of this entry inside <see cref="values"/>. Reassign on unregister.
        /// </summary>
        public int index { get; private set; } = -1;

        /// <summary>
        /// The total list of entries.
        /// </summary>
        public static List<T> values { get; } = new();
        
        protected ExtEnum(string value, bool register = true)
        {
            this.value = value;

            if (register)
                Register();
        }
        
        public void Register()
        {
            if (index != -1)
            {
                Debug.LogWarning("ExtEnum has already been registered; exiting.");
                return;
            }

            foreach (T existing in values)
            {
                if (existing.value == value)
                    throw new InvalidOperationException($"ExtEnum entry \"{value}\" is already for type {typeof(T).Name}. " + "Two mods (or own code) are colliding on the same name.");
            }

            index = values.Count;
            values.Add(this as T);
        }

        public void Unregister()
        {
            if (index == -1)
            {
                Debug.LogWarning("ExtEnum has already been unregistered; exiting.");
                return;
            }
            
            values.RemoveAt(index);

            // optimize a tiny*10^9 bit by using a byte
            if (values.Count < 256)
            {
                for (byte i = 0; i < values.Count; ++i)
                    values[i].index = i;
            }
            else // use unsigned short
            {
                for (ushort i = 0; i < values.Count; ++i)
                    values[i].index = i;
            }

            index = -1;
        }

        /// <summary>
        /// Finds an entry by name.
        /// </summary>
        /// <param name="value">The entry name.</param>
        public static T Find(string value)
        {
            foreach (T entry in values)
            {
                if (entry.value == value)
                    return entry;
            }

            // if nothing is found
            return null;
        }

        public override string ToString()
        {
            return value;
        }
        
        public override bool Equals([CanBeNull] object obj)
        {
            return obj is ExtEnum<T> other && other.value == value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(ExtEnum<T> left, ExtEnum<T> right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left == null || right == null)
                return false;

            return left.value == right.value;
        }

        public static bool operator !=(ExtEnum<T> left, ExtEnum<T> right)
        {
            return !(left == right);
        }
    }
}