using System;

namespace RH.App.Infrastructure.Data.Modules.AuditModule.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAuditAttribute : Attribute
    {
    }
}
