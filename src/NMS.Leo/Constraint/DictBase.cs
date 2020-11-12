using System.Collections.Generic;

namespace NMS.Leo
{
    public abstract class DictBase<T> : DictBase
    {
        public T Instance;
        public void SetInstance(T value) => Instance = value;

        public override void SetObjInstance(object obj)
        {
            Instance = (T)obj;
        }
    }

    public abstract class DictBase : CallerBase
    {
        public object this[string name]
        {
            get => GetObject(name);
            set => Set(name, value);
        }

        public abstract void SetObjInstance(object obj);
        public abstract unsafe object GetObject(string name);

        protected virtual List<string> InternalMemberNames { get; } = new List<string>();

        public IEnumerable<string> GetMemberNames() => InternalMemberNames;

        public IEnumerable<string> GetCanReadMemberNames()
        {
            foreach (var member in GetCanReadMembers())
                yield return member.MemberName;
        }

        public IEnumerable<string> GetCanWriteMemberNames()
        {
            foreach (var member in GetCanWriteMembers())
                yield return member.MemberName;
        }

        public IEnumerable<LeoMember> GetMembers()
        {
            foreach (var name in InternalMemberNames)
                yield return GetMember(name);
        }

        public IEnumerable<LeoMember> GetCanReadMembers()
        {
            foreach (var name in InternalMemberNames)
            {
                var member = GetMember(name);
                if (member.CanRead)
                    yield return member;
            }
        }

        public IEnumerable<LeoMember> GetCanWriteMembers()
        {
            foreach (var name in InternalMemberNames)
            {
                var member = GetMember(name);
                if (member.CanWrite)
                    yield return member;
            }
        }

        public abstract unsafe LeoMember GetMember(string name);
    }
}