using Natasha;
using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;


namespace NCaller
{

    public class DictOperator
    {

        public static Func<string, DictBase> CreateFromString;
        static DictOperator()
        {

            CreateFromString = item => default;
            CreateFromString = PrecisionDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public static class DictOperator<T>
    {

        public readonly static Func<DictBase> Create;
        static DictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Precision);
            Create = (Func<DictBase>)(CtorOperator.Default.NewDelegate(dynamicType));
        }

    }

}
