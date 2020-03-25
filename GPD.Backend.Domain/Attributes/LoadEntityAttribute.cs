using System;

namespace GPD.Backend.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LoadEntityAttribute : Attribute
    {
        public string NameForeignKey { get; set; }
        
        public Type TypeRepository { get; set; }
    }
}
