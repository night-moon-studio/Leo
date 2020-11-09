using System;
using Leo.Typed.Core;

namespace NMS.Leo.Typed.Core
{
    internal class HistoricalContext
    {
        public HistoricalContext(Type sourceType, AlgorithmType algorithmType)
        {
            SourceType = sourceType;
            AlgorithmType = algorithmType;
        }

        public Type SourceType { get; }

        public AlgorithmType AlgorithmType { get; }

        protected Action<DictBase> _handleHistory;

        public void RegisterOperation(Action<DictBase> change)
        {
            if (change != null)
            {
                _handleHistory = _handleHistory == null
                    ? change
                    : _handleHistory += change;
            }
        }

        public object Repeat()
        {
            var handler = SafeLeoHandleSwitcher.Switch(AlgorithmType)(SourceType);
            handler.New();
            _handleHistory?.Invoke(handler);

            return handler.GetInstance();
        }

        public object Repeat(object instance)
        {
            var handler = SafeLeoHandleSwitcher.Switch(AlgorithmType)(SourceType);
            handler.SetObjInstance(instance);
            _handleHistory?.Invoke(handler);

            return handler.GetInstance();
        }
    }

    internal class HistoricalContext<TObject> : HistoricalContext
    {
        public HistoricalContext(AlgorithmType algorithmType) : base(typeof(TObject), algorithmType) { }

        public TObject Repeat(TObject instance)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<TObject>(AlgorithmType)().With<TObject>();

            handler.SetInstance(instance);

            _handleHistory?.Invoke(handler);

            return handler.GetInstance();
        }

        public new TObject Repeat()
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<TObject>(AlgorithmType)().With<TObject>();

            handler.New();

            _handleHistory?.Invoke(handler);

            return handler.GetInstance();
        }
    }
}