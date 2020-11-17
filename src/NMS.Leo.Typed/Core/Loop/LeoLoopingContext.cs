using System;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Loop
{
    internal class InternalLeoLoopingContext
    {
        private readonly bool _withNumber;
        private readonly bool _withMember;
        private Action<string, object, LeoMember, int> _action0;
        private Action<string, object, LeoMember> _action1;
        private Action<string, object> _action2;

        public InternalLeoLoopingContext(Action<LeoLoopContext> loopAct)
        {
            if (loopAct is null)
                _action0 = (s, o, m, i) => { };
            else
                _action0 = (s, o, m, i) => loopAct.Invoke(new LeoLoopContext(s, o, m, i));

            _withMember = true;
            _withNumber = true;
        }

        public InternalLeoLoopingContext(Action<string, object, LeoMember> loopAct)
        {
            _action1 = loopAct;
            _withMember = true;
            _withNumber = false;
        }

        public InternalLeoLoopingContext(Action<string, object> loopAct)
        {
            _action2 = loopAct;
            _withMember = false;
            _withNumber = false;
        }

        public void Do(string name, object value, LeoMember member, int? index = null)
        {
            if (_withNumber && index.HasValue)
                _action0?.Invoke(name, value, member, index.Value);
            else if (_withMember)
                _action1?.Invoke(name, value, member);
            else
                _action2?.Invoke(name, value);
        }

        public bool NeedLeoMember => _withMember;

        public bool NeedLeoNumber => _withNumber;
    }
}