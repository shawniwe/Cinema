using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cinema.Abstract
{
    public interface IMap
    {
        public Dictionary<PropertyInfo, PropertyInfo> PropertyMappings { get; }
        Type SourceType { get; }
        Type DestinationType { get; }
    }
}