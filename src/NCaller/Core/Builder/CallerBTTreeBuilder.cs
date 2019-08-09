using System;
using System.Collections.Generic;
using System.Text;
using Natasha;
using NCaller.Core.Model;


namespace NCaller.Core
{

    public class CallerBTTreeBuilder
    {

        public readonly int Slice;
        public readonly string Caller;
        public readonly BTFindTree<BuilderModel> BuildTree;
        public readonly StringBuilder Script;


        public CallerBTTreeBuilder(List<BuilderModel> builds, string caller = "Instance", int slice = 3)
        {

            Caller = caller;
            Slice = slice;
            Script = new StringBuilder();
            BuildTree = new BTFindTree<BuilderModel>(builds.ToArray(), Slice);

        }




        public void Foreach(BTFindTree<BuilderModel> tree, Action<BTFindTree<BuilderModel>,Action,Action> nodeAction, Action<BTFindTree<BuilderModel>, TreeType> LeavesAction, TreeType treeType= TreeType.None, int deepth = 0)
        {

            if (0 != tree.CompareCode)
            {

                deepth += 1;
                nodeAction(tree,
                    ()=> { Foreach(tree.LssTree, nodeAction, LeavesAction, TreeType.Lss, deepth); },
                    ()=> { Foreach(tree.GtrTree, nodeAction, LeavesAction, TreeType.Gtr, deepth); });
               
            }
            else
            {

                LeavesAction(tree, treeType);

            }

        }




        public StringBuilder GetScript_GetDynamicBase()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public override LinkBase Get(string name){");
            script.AppendLine("int nameCode = name.GetHashCode();");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves,type) => 
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!leaves.Buckets[i].MemberType.IsOnceType())
                    {

                        if (!IsFirst)
                        {

                            script.Append("else ");

                        }
                        script.AppendLine($"if(nameCode == {leaves.Buckets[i].NameHashCode}){{");
                        script.AppendLine($"   return {GetCallerExpression(leaves.Buckets[i])}.Caller();");
                        script.AppendLine("}");
                        IsFirst = false;

                    }

                }

            });
            script.Append("return default;}");


            return script;

        }



        public StringBuilder GetScript_GetObjectByName()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public override object GetObject(string name){");
            script.AppendLine("int nameCode = name.GetHashCode();");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves, type) =>
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!IsFirst)
                    {

                        script.Append("else ");

                    }
                    script.AppendLine($"if(nameCode == {leaves.Buckets[i].NameHashCode}){{");
                    script.AppendLine($"return {GetCallerExpression(leaves.Buckets[i])};");
                    script.AppendLine("}");
                    IsFirst = false;

                }

            });
            script.Append("return default;}");


            return script;

        }




        public StringBuilder GetScript_GetByName()
        {
 
            StringBuilder script = new StringBuilder();
            script.AppendLine("public override T Get<T>(string name){");
            script.AppendLine("int nameCode = name.GetHashCode();");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves, type) => 
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!IsFirst)
                    {

                        script.Append("else ");

                    }
                    script.AppendLine($"if(nameCode == {leaves.Buckets[i].NameHashCode}){{");
                    script.AppendLine($"return (T)((object){GetCallerExpression(leaves.Buckets[i])});");
                    script.AppendLine("}");
                    IsFirst = false;

                }

            });
            script.Append("return default;}");


            return script;

        }




        public StringBuilder GetScript_GetByIndex()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public override T Get<T>(){");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(_nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves, type) => 
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!IsFirst)
                    {
                        script.Append("else ");
                    }
                    script.AppendLine($"if(_nameCode == {leaves.Buckets[i].NameHashCode}){{");
                    script.AppendLine($"return (T)((object){GetCallerExpression(leaves.Buckets[i])});");
                    script.AppendLine("}");
                    IsFirst = false;
                }

            });
            script.Append("return default;}");


            return script;

        }




        public StringBuilder GetScript_SetByName()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public override void Set(string name,object value){");
            script.AppendLine("int nameCode = name.GetHashCode();");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves, type) => 
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!IsFirst)
                    {

                        script.Append("else ");

                    }
                    script.AppendLine($"if(nameCode == {leaves.Buckets[i].NameHashCode}){{");
                    script.AppendLine($"{GetCallerExpression(leaves.Buckets[i])}=({leaves.Buckets[i].TypeName})value;");
                    script.AppendLine("}");
                    IsFirst = false;

                }

            });
            script.Append("}");


            return script;

        }




        public StringBuilder GetScript_SetByIndex()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public override void Set(object value){");


            Foreach(BuildTree, (node, actionLss, actionGtr) =>
            {

                script.AppendLine($"if(_nameCode < {node.CompareCode}){{");
                actionLss();
                script.Append("}else{");
                actionGtr();
                script.Append("}");

            }, (leaves, type) => 
            {

                bool IsFirst = true;
                for (int i = 0; i < leaves.Buckets.Length; i++)
                {

                    if (!IsFirst)
                    {

                        script.Append("else ");

                    }
                    script.AppendLine($"if(_nameCode == {leaves.Buckets[i].NameHashCode}){{");
                    script.AppendLine($"{GetCallerExpression(leaves.Buckets[i])}=({leaves.Buckets[i].TypeName})value;");
                    script.AppendLine("}");
                    IsFirst = false;

                }

            });
            script.Append("}");


            return script;

        }




        public string GetCallerExpression(BuilderModel model)
        {
            if (model.IsStatic)
            {
                return $"{model.DeclareTypeName}.{model.MemberName}";
            }
            else
            {
                return $"{Caller}.{model.MemberName}";
            }
        }




        public enum TreeType
        {
            None,
            Lss,
            Gtr
        }
    }

    
}
