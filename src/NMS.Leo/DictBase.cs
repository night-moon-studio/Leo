using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NMS.Leo.Core;
using NMS.Leo.Metadata;

namespace NMS.Leo
{
#if NET5_0
[SkipLocalsInit]
#endif
    public abstract class DictBase<T> : DictBase
    {
        public T Instance;
        
        public void SetInstance(T value) => Instance = value;

        public override void SetObjInstance(object obj)
        {
            Instance = (T) obj;
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

        protected virtual HashSet<string> InternalMemberNames { get; } = new HashSet<string>();

        public IEnumerable<string> GetMemberNames() => InternalMemberNames;

        public IEnumerable<string> GetCanReadMemberNames() => GetCanReadMembers().Select(member => member.MemberName);

        public IEnumerable<string> GetCanWriteMemberNames() => GetCanWriteMembers().Select(member => member.MemberName);

        public IEnumerable<LeoMember> GetMembers() => InternalMemberNames.Select(GetMember);

        public IEnumerable<LeoMember> GetCanReadMembers() => GetMembers().Where(member => member.CanRead);

        public IEnumerable<LeoMember> GetCanWriteMembers() => GetMembers().Where(member => member.CanWrite);

        public abstract unsafe LeoMember GetMember(string name);

        public bool Contains(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && InternalMemberNames.Contains(name);
        }
    }
}