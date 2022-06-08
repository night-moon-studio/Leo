namespace NMS.Leo.Typed;

public static class LeoInstanceCreator
{
    public static object Create(Type type, IDictionary<string, object> keyValueCollections, AlgorithmKind kind = AlgorithmKind.Precision)
    {
        return Create(type, keyValueCollections, out _, kind);
    }

    public static object Create(Type type, IDictionary<string, object> keyValueCollections, out ILeoVisitor visitor, AlgorithmKind kind = AlgorithmKind.Precision)
    {
        visitor = LeoVisitorFactory.Create(type, keyValueCollections, kind);
        return visitor.Instance;
    }

    public static T Create<T>(IDictionary<string, object> keyValueCollections, AlgorithmKind kind = AlgorithmKind.Precision)
    {
        return Create<T>(keyValueCollections, out _, kind);
    }

    public static T Create<T>(IDictionary<string, object> keyValueCollections, out ILeoVisitor<T> visitor, AlgorithmKind kind = AlgorithmKind.Precision)
    {
        visitor = LeoVisitorFactory.Create<T>(keyValueCollections, kind);
        return visitor.Instance;
    }
}