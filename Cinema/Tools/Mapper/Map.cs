using Cinema.Abstract;
using Cinema.Models;
using Microsoft.Data.SqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Tools.Mapper
{
    public class Map<T1, T2> : IMap where T1 : class where T2 : class
    {
        public Dictionary<PropertyInfo, PropertyInfo> PropertyMappings { get; private set; }
        public Type SourceType { get; private set; }
        public Type DestinationType { get; private set; }
        public Map(Type src, Type dest)
        {
            SourceType = src;
            DestinationType = dest;
            PropertyMappings = new Dictionary<PropertyInfo, PropertyInfo>();
        }

        public Map<T1, T2> AddPropertyRelation(Expression<Func<T1, object>> src, Expression<Func<T2, object>> dest)
        {
            PropertyInfo srcProperty = null;
            PropertyInfo destProperty = null;

            var type = src.Body.GetType();

            if (src.Body is UnaryExpression)
            {
                var srcExp = (UnaryExpression)src.Body;
                if (srcExp.Operand is MemberExpression)
                {
                    var me = (MemberExpression)srcExp.Operand;
                    srcProperty = (PropertyInfo)me.Member;
                }
                else throw new ArgumentException();
            }

            if (src.Body is MemberExpression)
            {
                var srcExp = (MemberExpression)src.Body;
                srcProperty = (PropertyInfo)srcExp.Member;
            }

            if (dest.Body is UnaryExpression)
            {
                var destExp = (UnaryExpression)dest.Body;
                if (destExp.Operand is MemberExpression)
                {
                    var me = (MemberExpression)destExp.Operand;
                    destProperty = (PropertyInfo)me.Member;
                }
                else throw new ArgumentException();
            }

            if (dest.Body is MemberExpression)
            {
                var destExp = (MemberExpression)dest.Body;
                destProperty = (PropertyInfo)destExp.Member;
            }

            PropertyMappings.Add(srcProperty, destProperty);

            return this;
        }
    }
}
