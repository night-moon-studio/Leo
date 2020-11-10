using System;

namespace NMS.Leo.Typed.Core
{
    internal class LeoRepeater : ILeoRepeater
    {
        private HistoricalContext NormalHistoricalContext { get; set; }

        public LeoRepeater(HistoricalContext context)
        {
            NormalHistoricalContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public object Play(object originalObj)
        {
            return NormalHistoricalContext.Repeat(originalObj);
        }

        public object NewAndPlay()
        {
            return NormalHistoricalContext.Repeat();
        }
    }

    internal class LeoRepeater<T> : ILeoRepeater<T>
    {
        private HistoricalContext<T> GenericHistoricalContext { get; set; }

        public LeoRepeater(HistoricalContext<T> context)
        {
            GenericHistoricalContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T Play(T originalObj)
        {
            return GenericHistoricalContext.Repeat(originalObj);
        }

        object ILeoRepeater.Play(object originalObj)
        {
            return GenericHistoricalContext.Repeat(originalObj);
        }

        public T NewAndPlay()
        {
            return GenericHistoricalContext.Repeat();
        }

        object ILeoRepeater.NewAndPlay()
        {
            return ((HistoricalContext) GenericHistoricalContext).Repeat();
        }
    }

    internal class EmptyRepeater : ILeoRepeater
    {
        private readonly Type _sourceType;

        public EmptyRepeater(Type sourceType)
        {
            _sourceType = sourceType;
        }

        public object Play(object originalObj) => originalObj;

        public object NewAndPlay() => Activator.CreateInstance(_sourceType);
    }

    internal class EmptyRepeater<T> : ILeoRepeater<T>
    {
        public T Play(T originalObj) => originalObj;

        object ILeoRepeater.Play(object originalObj) => originalObj;

        public T NewAndPlay() => Activator.CreateInstance<T>();

        object ILeoRepeater.NewAndPlay() => Activator.CreateInstance(typeof(T));
    }

    internal class StaticEmptyRepeater : ILeoRepeater
    {
        public object Play(object originalObj) => originalObj;

        public object NewAndPlay() => null;

        public static StaticEmptyRepeater Instance { get; } = new StaticEmptyRepeater();
    }

    internal class StaticEmptyRepeater<T> : ILeoRepeater<T>
    {
        public T Play(T originalObj) => originalObj;

        object ILeoRepeater.Play(object originalObj) => originalObj;

        public T NewAndPlay() => default;

        object ILeoRepeater.NewAndPlay() => null;

        public static StaticEmptyRepeater<T> Instance { get; } = new StaticEmptyRepeater<T>();
    }
}