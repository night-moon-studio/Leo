using NCaller.Builder;
using System;

namespace NCaller.Operator
{


    public class FuzzyDictOperator
    {
        public static DictBase Create(Type type) => FuzzyDictBuilder.Ctor(type);
    }




    public static class FuzzyDictOperator<T>
    {

        public readonly static Func<DictBase> Create;

        static FuzzyDictOperator() => Create = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);

    }

}
