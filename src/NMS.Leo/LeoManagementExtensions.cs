using NMS.Leo.Metadata;

namespace NMS.Leo;

public static class LeoManagementExtensions
{
    public static void AllowLeoPrivate(this Type type)
    {
        LeoMemberAccessibilityMan.AllowTypePrivate(type);
    }
}