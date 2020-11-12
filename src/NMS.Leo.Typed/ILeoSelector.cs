using System.Collections.Generic;

namespace NMS.Leo.Typed
{
    public interface ILeoSelector<TVal>
    {
        ILeoVisitor BackToVisitor();

        IEnumerable<TVal> FireAndReturn();

        ILeoVisitor FireAndBack(out IEnumerable<TVal> returnedVal);
    }
    
    public interface ILeoSelector<T, TVal>
    {
        ILeoVisitor<T> BackToVisitor();

        IEnumerable<TVal> FireAndReturn();

        ILeoVisitor<T> FireAndBack(out IEnumerable<TVal> returnedVal);
    }
}