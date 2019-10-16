using NCaller.Builder;
using System;


namespace NCaller
{

    public class LinkOperator
    {
        public static LinkBase Create(Type type) => PrecisionLinkBuilder.Ctor(type);
    }




    public class LinkOperator<T>
    {
        public readonly static Func<LinkBase> Create;
        static LinkOperator() => Create = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Precision);
    }

}
