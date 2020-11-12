using System;
using System.Collections.Generic;

namespace NMS.Leo.Typed.Core
{
    internal class LeoSelector<TVal> : ILeoSelector<TVal>
    {
        private readonly ILeoVisitor _visitor;
        private readonly Lazy<LeoMemberHandler> _memberHandler;
        private readonly InternalLeoSelectingContext<TVal> _context;

        public LeoSelector(ILeoVisitor visitor, Lazy<LeoMemberHandler> memberHandler, Func<string, object, LeoMember, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public LeoSelector(ILeoVisitor visitor, Lazy<LeoMemberHandler> memberHandler, Func<string, object, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public LeoSelector(ILeoVisitor visitor, Lazy<LeoMemberHandler> memberHandler, Func<LeoLoopContext, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public ILeoVisitor BackToVisitor() => _visitor;

        public IEnumerable<TVal> FireAndReturn()
        {
            var needLeoMember = _context.NeedLeoMember;
            var needLeoIndex = _context.NeedLeoNumber;
            var index = 0;
            foreach (var name in _visitor.GetMemberNames())
            {
                if (needLeoMember && needLeoIndex)
                    yield return _context.Do(name, _visitor[name], _memberHandler.Value[name], index++);
                else if (needLeoMember)
                    yield return _context.Do(name, _visitor[name], _memberHandler.Value[name]);
                else
                    yield return _context.Do(name, _visitor[name], null);
            }
        }

        public ILeoVisitor FireAndBack(out IEnumerable<TVal> returnedVal)
        {
            returnedVal = FireAndReturn();
            return BackToVisitor();
        }
    }

    internal class LeoSelector<T, TVal> : ILeoSelector<T, TVal>
    {
        private readonly ILeoVisitor<T> _visitor;
        private readonly Lazy<LeoMemberHandler> _memberHandler;
        private readonly InternalLeoSelectingContext<TVal> _context;

        public LeoSelector(ILeoVisitor<T> visitor, Lazy<LeoMemberHandler> memberHandler, Func<string, object, LeoMember, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public LeoSelector(ILeoVisitor<T> visitor, Lazy<LeoMemberHandler> memberHandler, Func<string, object, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public LeoSelector(ILeoVisitor<T> visitor, Lazy<LeoMemberHandler> memberHandler, Func<LeoLoopContext, TVal> loopFunc)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoSelectingContext<TVal>(loopFunc);
        }

        public ILeoVisitor<T> BackToVisitor() => _visitor;

        public IEnumerable<TVal> FireAndReturn()
        {
            var needLeoMember = _context.NeedLeoMember;
            var needLeoIndex = _context.NeedLeoNumber;
            var index = 0;
            foreach (var name in _visitor.GetMemberNames())
            {
                if (needLeoMember && needLeoIndex)
                    yield return _context.Do(name, _visitor[name], _memberHandler.Value[name], index++);
                else if (needLeoMember)
                    yield return _context.Do(name, _visitor[name], _memberHandler.Value[name]);
                else
                    yield return _context.Do(name, _visitor[name], null);
            }
        }

        public ILeoVisitor<T> FireAndBack(out IEnumerable<TVal> returnedVal)
        {
            returnedVal = FireAndReturn();
            return BackToVisitor();
        }
    }
}