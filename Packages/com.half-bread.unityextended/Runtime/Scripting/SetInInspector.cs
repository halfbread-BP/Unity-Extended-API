using System;

// this doesn't actually do anything lol
// it's moreof placebo

namespace UnityEngine.Extended
{
    /// <summary>
    /// Marks the field/property to be initially set in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SetInInspectorAttribute : Attribute
    { }
}