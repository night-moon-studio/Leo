using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NMS.Leo.Typed
{
    public interface ILeoVisitor
    {
        Type SourceType { get; }

        bool IsStatic { get; }

        AlgorithmKind AlgorithmKind { get; }

        void SetValue(string name, object value);

        void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value);

        void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value);

        object GetValue(string name);

        TValue GetValue<TValue>(string name);

        object GetValue<TObj>(Expression<Func<TObj, object>> expression);

        TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression);

        object this[string name] { get; set; }

        bool TryRepeat(out object result);

        bool TryRepeat(object instance, out object result);

        ILeoRepeater ForRepeat();

        IEnumerable<string> GetMemberNames();

        LeoMember GetMember(string name);

        ILeoLooper ForEach(Action<string, object, LeoMember> loopAct);

        ILeoLooper ForEach(Action<string, object> loopAct);

        ILeoLooper ForEach(Action<LeoLoopContext> loopAct);

        ILeoSelector<TVal> Select<TVal>(Func<string, object, LeoMember, TVal> loopFunc);

        ILeoSelector<TVal> Select<TVal>(Func<string, object, TVal> loopFunc);

        ILeoSelector<TVal> Select<TVal>(Func<LeoLoopContext, TVal> loopFunc);

        Dictionary<string, object> ToDictionary();
    }

    public interface ILeoVisitor<T> : ILeoVisitor
    {
        void SetValue(Expression<Func<T, object>> expression, object value);

        void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value);

        object GetValue(Expression<Func<T, object>> expression);

        TValue GetValue<TValue>(Expression<Func<T, TValue>> expression);

        bool TryRepeat(out T result);

        bool TryRepeat(T instance, out T result);

        new ILeoRepeater<T> ForRepeat();

        LeoMember GetMember<TValue>(Expression<Func<T, TValue>> expression);

        new ILeoLooper<T> ForEach(Action<string, object, LeoMember> loopAct);

        new ILeoLooper<T> ForEach(Action<string, object> loopAct);

        new ILeoLooper<T> ForEach(Action<LeoLoopContext> loopAct);

        new ILeoSelector<T, TVal> Select<TVal>(Func<string, object, LeoMember, TVal> loopFunc);

        new ILeoSelector<T, TVal> Select<TVal>(Func<string, object, TVal> loopFunc);

        new ILeoSelector<T, TVal> Select<TVal>(Func<LeoLoopContext, TVal> loopFunc);
    }
}