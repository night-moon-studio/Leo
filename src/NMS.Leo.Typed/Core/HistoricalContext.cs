using System;
using System.Linq;
using System.Reflection;

namespace NMS.Leo.Typed.Core
{
    internal class HistoricalContext
    {
        public HistoricalContext(Type sourceType, LeoType leoType)
        {
            SourceType = sourceType;
            AlgorithmType = leoType;
        }

        public Type SourceType { get; }

        public LeoType AlgorithmType { get; }

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

            // Get 'Instance' Field and touch from 'Instance' Field by reflection.
            //
            var fieldInfo = handler.GetType().GetField("Instance", BindingFlags.Instance | BindingFlags.NonPublic);

            return fieldInfo?.GetValue(handler);
        }

        public object Repeat(object instance)
        {
            var handler = SafeLeoHandleSwitcher.Switch(AlgorithmType)(SourceType);
            handler.SetObjInstance(instance);
            _handleHistory?.Invoke(handler);

            // Return the instance directly.
            //
            return instance;
        }
    }

    internal class HistoricalContext<TObject> : HistoricalContext
    {
        public HistoricalContext(LeoType leoType) : base(typeof(TObject), leoType) { }

        public TObject Repeat(TObject instance)
        {
            var handler = (DictBase<TObject>)UnsafeLeoHandleSwitcher.Switch<TObject>(AlgorithmType)();

            handler.SetInstance(instance);

            _handleHistory?.Invoke(handler);

            return handler.Instance;

            // Return the instance directly.
            //
            //return instance;
        }

        public TObject Repeat()
        {
            var handler = (DictBase<TObject>)UnsafeLeoHandleSwitcher.Switch<TObject>(AlgorithmType)();

            handler.New();

            _handleHistory?.Invoke(handler);

            return handler.Instance;

            // Get 'Instance' Field and touch from 'Instance' Field by reflection.
            //
            //var fieldInfo = typeof(DictBase<TObject>)
            //    .GetField("Instance", BindingFlags.Instance | BindingFlags.NonPublic);

            //return (TObject)fieldInfo?.GetValue(handler);
        }
    }
}