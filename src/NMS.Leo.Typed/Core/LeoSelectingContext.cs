using System;

namespace NMS.Leo.Typed.Core
{
    internal class InternalLeoSelectingContext<TVal>
    {
        private readonly bool _withNumber;
        private readonly bool _withMember;
        private Func<string, object, LeoMember, int, TVal> _action0;
        private Func<string, object, LeoMember, TVal> _action1;
        private Func<string, object, TVal> _action2;

        public InternalLeoSelectingContext(Func<LeoLoopContext, TVal> loopFunc)
        {
            if (loopFunc is null)
                _action0 = (s, o, m, i) => default;
            else
                _action0 = (s, o, m, i) => loopFunc.Invoke(new LeoLoopContext(s, o, m, i));

            _withMember = true;
            _withNumber = true;
        }

        public InternalLeoSelectingContext(Func<string, object, LeoMember, TVal> loopFunc)
        {
            _action1 = loopFunc;
            _withMember = true;
            _withNumber = false;
        }

        public InternalLeoSelectingContext(Func<string, object, TVal> loopFunc)
        {
            _action2 = loopFunc;
            _withMember = false;
            _withNumber = false;
        }

        public TVal Do(string name, object value, LeoMember member, int? index = null)
        {
            if (_withNumber && index.HasValue)
                return _action0 is null ? default : _action0.Invoke(name, value, member, index.Value);
            else if (_withMember)
                return _action1 is null ? default : _action1.Invoke(name, value, member);
            else
                return _action2 is null ? default : _action2.Invoke(name, value);
        }

        public bool NeedLeoMember => _withMember;

        public bool NeedLeoNumber => _withNumber;
    }
}