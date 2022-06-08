using NMS.Leo.Typed.Core.Members;
using NMS.Leo.Typed.Core.Repeat;

namespace NMS.Leo.Typed.Core;

internal interface ICoreVisitor
{
    Type SourceType { get; }
        
    bool IsStatic { get; }
        
    AlgorithmKind AlgorithmKind { get; }
        
    object Instance { get; }

    HistoricalContext ExposeHistoricalContext();

    Lazy<MemberHandler> ExposeLazyMemberHandler();
        
    ILeoVisitor Owner { get; }
        
    bool LiteMode { get; }
}
    
internal interface ICoreVisitor<T>
{
    Type SourceType { get; }
        
    bool IsStatic { get; }
        
    AlgorithmKind AlgorithmKind { get; }
        
    T Instance { get; }

    HistoricalContext<T> ExposeHistoricalContext();

    Lazy<MemberHandler> ExposeLazyMemberHandler();
        
    ILeoVisitor<T> Owner { get; }
        
    bool LiteMode { get; }
}