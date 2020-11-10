using System.Collections.Generic;

namespace NMS.Leo
{
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

        protected virtual Dictionary<string, LeoMember> InternalMembersMetadata { get; } = new Dictionary<string, LeoMember>();

        public IEnumerable<string> GetMemberNames() => InternalMembersMetadata.Keys;

        public IEnumerable<string> GetCanReadMemberNames()
        {
            foreach (var metadata in InternalMembersMetadata)
                if (metadata.Value.CanRead)
                    yield return metadata.Key;
        }

        public IEnumerable<string> GetCanWriteMemberNames()
        {
            foreach (var metadata in InternalMembersMetadata)
                if (metadata.Value.CanWrite)
                    yield return metadata.Key;
        }

        public IEnumerable<LeoMember> GetMembers() => InternalMembersMetadata.Values;

        public IEnumerable<LeoMember> GetCanReadMembers()
        {
            foreach (var metadata in InternalMembersMetadata)
                if (metadata.Value.CanRead)
                    yield return metadata.Value;
        }

        public IEnumerable<LeoMember> GetCanWriteMembers()
        {
            foreach (var metadata in InternalMembersMetadata)
                if (metadata.Value.CanWrite)
                    yield return metadata.Value;
        }

        public LeoMember GetMember(string name) => InternalMembersMetadata.TryGetValue(name, out var member) ? member : default;
    }
}