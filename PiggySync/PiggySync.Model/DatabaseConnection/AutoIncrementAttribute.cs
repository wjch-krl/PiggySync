using System;

namespace PiggySync.Model.DatabaseConnection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoIncrementAttribute : Attribute
    {
    }
}