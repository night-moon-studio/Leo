using System;
using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Core.Loop
{
    internal class LeoLooper : ILeoLooper
    {
        private readonly ILeoVisitor _visitor;
        private readonly Lazy<MemberHandler> _memberHandler;
        private readonly InternalLeoLoopingContext _context;

        public LeoLooper(ILeoVisitor visitor, Lazy<MemberHandler> memberHandler, Action<string, object, LeoMember> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public LeoLooper(ILeoVisitor visitor, Lazy<MemberHandler> memberHandler, Action<string, object> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public LeoLooper(ILeoVisitor visitor, Lazy<MemberHandler> memberHandler, Action<LeoLoopContext> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public ILeoVisitor BackToVisitor() => _visitor;

        public void Fire()
        {
            var needLeoMember = _context.NeedLeoMember;
            var needLeoIndex = _context.NeedLeoNumber;
            var index = 0;
            foreach (var name in _visitor.GetMemberNames())
            {
                if (needLeoMember && needLeoIndex)
                    _context.Do(name, _visitor[name], _memberHandler.Value[name], index++);
                else if (needLeoMember)
                    _context.Do(name, _visitor[name], _memberHandler.Value[name]);
                else
                    _context.Do(name, _visitor[name], null);
            }
        }

        public ILeoVisitor FireAndBack()
        {
            Fire();
            return BackToVisitor();
        }
    }

    internal class LeoLooper<T> : ILeoLooper<T>
    {
        private readonly ILeoVisitor<T> _visitor;
        private readonly Lazy<MemberHandler> _memberHandler;
        private readonly InternalLeoLoopingContext _context;

        public LeoLooper(ILeoVisitor<T> visitor, Lazy<MemberHandler> memberHandler, Action<string, object, LeoMember> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public LeoLooper(ILeoVisitor<T> visitor, Lazy<MemberHandler> memberHandler, Action<string, object> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public LeoLooper(ILeoVisitor<T> visitor, Lazy<MemberHandler> memberHandler, Action<LeoLoopContext> loopAct)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _memberHandler = memberHandler ?? throw new ArgumentNullException(nameof(memberHandler));
            _context = new InternalLeoLoopingContext(loopAct);
        }

        public ILeoVisitor<T> BackToVisitor() => _visitor;

        public void Fire()
        {
            var needLeoMember = _context.NeedLeoMember;
            var needLeoIndex = _context.NeedLeoNumber;
            var index = 0;
            foreach (var name in _visitor.GetMemberNames())
            {
                if (needLeoMember && needLeoIndex)
                    _context.Do(name, _visitor[name], _memberHandler.Value[name], index++);
                else if (needLeoMember)
                    _context.Do(name, _visitor[name], _memberHandler.Value[name]);
                else
                    _context.Do(name, _visitor[name], null);
            }
        }

        public ILeoVisitor<T> FireAndBack()
        {
            Fire();
            return BackToVisitor();
        }
    }
}