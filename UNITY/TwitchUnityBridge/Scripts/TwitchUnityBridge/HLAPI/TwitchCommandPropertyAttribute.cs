#region

using System;

#endregion

namespace TwitchUnityBridge.HLAPI
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TwitchCommandPropertyAttribute : Attribute
    {

        public TwitchCommandPropertyAttribute(int position, bool required = true)
        {
            Position = position;
            Required = required;
        }
        public int Position { get; }
        public bool Required { get; }
    }
}
