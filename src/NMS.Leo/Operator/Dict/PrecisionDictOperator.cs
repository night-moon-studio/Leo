﻿using Natasha.CSharp;
using Natasha.CSharp.Builder;
using NMS.Leo.Builder;
using NMS.Leo.Constraint;
using System;


namespace NMS.Leo
{

    public static unsafe class PrecisionDictOperator
    {

        public static delegate* managed<Type, DictBase> CreateFromString;
        static PrecisionDictOperator()
        {

            PrecisionDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type);
        }

    }




    public unsafe static class PrecisionDictOperator<T>
    {

        public readonly static delegate* managed<DictBase> Create;
        static PrecisionDictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Precision);
            Create = (delegate* managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }

    }

}