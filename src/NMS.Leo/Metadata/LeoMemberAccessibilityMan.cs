using System.Collections.Concurrent;

namespace NMS.Leo.Metadata;

internal static class LeoMemberAccessibilityMan
{
    internal static readonly ConcurrentDictionary<Type, LeoMemberAccessibilityLevel> _accessCache;

    static LeoMemberAccessibilityMan()
    {
        _accessCache = new ConcurrentDictionary<Type, LeoMemberAccessibilityLevel>();
    }

    public static void AllowTypePrivate(Type type)
    {
        _accessCache[type] = LeoMemberAccessibilityLevel.AllowNoPublic;
    }
}