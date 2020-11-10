using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMS.Leo.Builder
{
    internal static class LeoMemberExtensions
    {
        public static StringBuilder ToLeoMemberMetadataScript(this Dictionary<string, LeoMember> leoMembersCache)
        {
            var builder = new StringBuilder();

            /*
             * 样本：
             *
        protected virtual Dictionary<string, LeoMember> InternalMembersMetadata { get; } = new Dictionary<string, LeoMember>()
        {
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
        };
             *
             * */

            if (leoMembersCache.Any())
            {
                var leoMemberDevelopName = typeof(LeoMember).GetDevelopName();
                foreach (var member in leoMembersCache)
                {
                    var v = member.Value;
                    builder.Append(
                        $@"{{ ""{member.Key}"", new {leoMemberDevelopName}({v.CanWrite.L()},{v.CanRead.L()},{v.IsConst.L()},""{v.MemberName}"",typeof({v.MemberType.GetDevelopName()}),{v.IsStatic.L()},{v.IsAsync.L()},{v.IsAbstract.L()},{v.IsVirtual.L()},{v.IsNew.L()},{v.IsOverride.L()},{v.IsReadOnly.L()}) }},");
                }
            }

            return builder;
        }

        private static string L(this bool boolean) => boolean ? "true" : "false";
    }
}