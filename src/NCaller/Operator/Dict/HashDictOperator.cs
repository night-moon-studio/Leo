using NCaller.Builder;
using System;

namespace NCaller.Operator
{

    public class HashDictOperator
    {
        public static DictBase Create(Type type) => HashDictBuilder.Ctor(type);
    }




    public static class HashDictOperator<T>
    {

        public readonly static Func<DictBase> Create;

        static HashDictOperator() => Create = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Hash);

    }


}
