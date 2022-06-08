using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core.Members;

internal class MemberHandler
{
    private readonly DictBase _handler;
    private readonly Type _sourceType;
    private readonly List<string> _memberNames;

    public MemberHandler(DictBase handler, Type sourceType)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        _sourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
        _memberNames = handler.GetMemberNames().ToList();
    }

    public LeoMember this[string name] => _handler.GetMember(name);

    public LeoMember GetMember(string name) => _handler.GetMember(name);

    public IEnumerable<LeoMember> GetMembers() => _handler.GetMembers();

    public bool Contains(string name) => _memberNames.Contains(name);

    public IEnumerable<string> GetNames() => _memberNames;

    public Type SourceType => _sourceType;

    public static Lazy<MemberHandler> Lazy(Func<MemberHandler> valueFactory, bool liteMode)
    {
        return liteMode
            ? default
            : new Lazy<MemberHandler>(valueFactory);
    }

    internal object GetInstanceObject() => _handler.GetInstance();

    internal object GetValueObject(string name) => _handler[name];

    internal TVal GetValue<TVal>(string name) => _handler.Get<TVal>(name);

    internal TVal GetValue<T, TVal>(Expression<Func<T, TVal>> selector) => _handler.Get<TVal>(PropertySelector.GetPropertyName(selector));
}