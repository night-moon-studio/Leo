using System;
using System.Collections.Generic;
using System.Text;
using Natasha;

namespace NCaller.Core
{
    public class CallerActionBuilder
    {
        public readonly string Caller;

        public CallerActionBuilder(string caller = "Instance")
        {
            Caller = caller;
        }

        public StringBuilder GetScript_GetDynamicBase(List<BuilderModel> builds)
        {
            bool IsFirst = true;
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override CallerBase Get(string name){");
            script.Append("int nameCode = _current_name.GetHashCode();");
            foreach (var item in builds)
            {
                if (!item.MemberType.IsOnceType())
                {
                    if (!IsFirst)
                    {
                        script.Append("else ");
                    }
                    script.Append($"if(nameCode == {item.NameHashCode}){{");
                    script.Append($"   return {Caller}.{item.MemberName}.Caller();");
                    script.Append("}");
                    IsFirst = false;
                }
            }
            script.Append("return default;}");
            return script;
        }


        public StringBuilder GetScript_GetByName(List<BuilderModel> builds)
        {
            bool IsFirst = true;
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override T Get<T>(string name){");
            script.Append("int nameCode = name.GetHashCode();");
            foreach (var item in builds)
            {
                if (!IsFirst)
                {
                    script.Append("else ");
                }
                script.Append($"if(nameCode == {item.NameHashCode}){{");
                script.Append($"return (T)((object){Caller}.{item.MemberName});");
                script.Append("}");
                IsFirst = false;
            }
            script.Append("return default;}");
            return script;
        }

        public StringBuilder GetScript_GetByIndex(List<BuilderModel> builds)
        {
            bool IsFirst = true;
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override T Get<T>(){");
            script.Append("int nameCode = _current_name.GetHashCode();");
            foreach (var item in builds)
            {
                if (!IsFirst)
                {
                    script.Append("else ");
                }
                script.Append($"if(nameCode == {item.NameHashCode}){{");
                script.Append($"return (T)((object){Caller}.{item.MemberName});");
                script.Append("}");
                IsFirst = false;
            }
            script.Append("return default;}");
            return script;
        }

        public StringBuilder GetScript_SetByName(List<BuilderModel> builds)
        {
            bool IsFirst = true;
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override void Set(string name,object value){");
            script.Append("int nameCode = name.GetHashCode();");
            foreach (var item in builds)
            {

                if (!IsFirst)
                {
                    script.Append("else ");
                }
                script.Append($"if(nameCode == {item.NameHashCode}){{");
                script.Append($"{Caller}.{item.MemberName}=({item.TypeName})value;");
                script.Append("}");
                IsFirst = false;
            }
            script.Append("}");
            return script;
        }

        public StringBuilder GetScript_SetByIndex(List<BuilderModel> builds)
        {
            bool IsFirst = true;
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override void Set(object value){");
            script.Append("int nameCode = _current_name.GetHashCode();");
            foreach (var item in builds)
            {

                if (!IsFirst)
                {
                    script.Append("else ");
                }
                script.Append($"if(nameCode == {item.NameHashCode}){{");
                script.Append($"{Caller}.{item.MemberName}=({item.TypeName})value;");
                script.Append("}");
                IsFirst = false;
            }
            script.Append("}");
            return script;
        }
    }
}
