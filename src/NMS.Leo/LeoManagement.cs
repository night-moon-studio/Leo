using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;


public static class LeoManagement
{
    internal static readonly ConcurrentDictionary<Type, MemberAccessLevel> _accessCache;

    static LeoManagement()
    {
        _accessCache = new ConcurrentDictionary<Type, MemberAccessLevel>();
    }

    public static void AllowTypePrivate(Type type)
    {
        _accessCache[type] = MemberAccessLevel.AllowNoPublic;
    }
}

public static class LeoTypeExtension
{
    public static void AllowLeoPrivate(this Type type)
    {
        LeoManagement.AllowTypePrivate(type);
    }
}

