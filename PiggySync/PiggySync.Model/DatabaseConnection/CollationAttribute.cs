using System;

namespace PiggySync.Model.DatabaseConnection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CollationAttribute : Attribute
    {
        public CollationAttribute(string collation)
        {
            Value = collation;
        }

        public string Value { get; private set; }
    }
}