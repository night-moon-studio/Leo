using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NMS.Leo;

namespace Leo.Typed.Core
{
    internal static class DictBaseExtensions
    {
        public static DictBase<TObj> With<TObj>(this DictBase handler)
        {
            return (DictBase<TObj>)handler;
        }

        public static object GetInstance(this DictBase handler)
        {
            // Get 'Instance' Field and touch from 'Instance' Field by reflection.
            //
            var fieldInfo = handler.GetType().GetField("Instance", BindingFlags.Instance | BindingFlags.Public);

            return fieldInfo?.GetValue(handler);
        }

        public static TObject GetInstance<TObject>(this DictBase<TObject> handler)
        {
            // Get 'Instance' Field and touch from 'Instance' Field by reflection.
            //
            var fieldInfo = typeof(DictBase<TObject>)
                .GetField("Instance", BindingFlags.Instance | BindingFlags.Public);

            return (TObject)fieldInfo?.GetValue(handler);
        }
    }
}
