using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<string> GetCanReadMemberNames() => GetCanReadMembers().Select(member => member.MemberName);

        public IEnumerable<string> GetCanWriteMemberNames() => GetCanWriteMembers().Select(member => member.MemberName);

        public IEnumerable<LeoMember> GetMembers() => InternalMemberNames.Select(GetMember);

        public IEnumerable<LeoMember> GetCanReadMembers() => GetMembers().Where(member => member.CanRead);

        public IEnumerable<LeoMember> GetCanWriteMembers() => GetMembers().Where(member => member.CanWrite);

        public abstract unsafe LeoMember GetMember(string name);
    }
}