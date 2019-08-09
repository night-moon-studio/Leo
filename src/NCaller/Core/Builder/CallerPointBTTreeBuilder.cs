using NCaller.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace NCaller.Core
{

    public class CallerPointBTTreeBuilder
    {

        public readonly int Slice;
        public readonly string Caller;
        public readonly PointBTFindTree<BuilderModel> BuildTree;
        public readonly StringBuilder Script;


        public CallerPointBTTreeBuilder(List<BuilderModel> builds, string caller = "Instance", int layer = 0)
        {

            Caller = caller;
            Slice = layer;
            Script = new StringBuilder();
            BuildTree = new PointBTFindTree<BuilderModel>(builds, layer);

        }




        public void Foreach(PointBTFindTree<BuilderModel> node, Action<PointBTFindTree<BuilderModel>, int> nodeAction, Action endAction, int layer = 0)
        {
            if (layer > 0)
            {
                nodeAction(node, layer);
            }


            if (node.Nodes != default)
            {
                foreach (var item in node.Nodes)
                {
                   
                    Foreach(item, nodeAction, endAction, layer + 1);

                }
            }
            

            if (node.PointCode > 0 && !node.IsEnd)
            {
                endAction();
            }
        }




        public StringBuilder GetScript_GetDynamicBase()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override LinkBase Get(string name){");
            script.AppendLine(@"fixed (char* c = name)
            {
                switch(*(long*)(c)){");

                Foreach(BuildTree, (node, layer) =>
                {

                    script.AppendLine($"case {node.PointCode}:");
                    if (node.IsEnd)
                    {

                        //根节点
                        script.AppendLine($"   return {GetCallerExpression(node.Value)}.LinkCaller();");

                    }
                    else if (node.Nodes != default)
                    {

                        //父节点
                        script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                    }

                }, () =>
                {
                    script.AppendLine("}break;");


                });

            script.Append("}}return default;}");


            return script;

        }



        public StringBuilder GetScript_GetObjectByName()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override object GetObject(string name){");
            script.AppendLine(@"fixed (char* c = name)
            {
                switch(*(long*)(c)){");

            Foreach(BuildTree, (node, layer) =>
            {


                if (node.IsEnd)
                {

                    //根节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"   return {GetCallerExpression(node.Value)};");

                }
                else if (node.Nodes != default)
                {

                    //父节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                }

            }, () =>
            {

                script.AppendLine("}break;");

            });


          
            script.Append("}}return default;}");
            return script;

        }




        public StringBuilder GetScript_GetByName()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override T Get<T>(string name){");
            script.AppendLine(@"fixed (char* c = name)
            {
                switch(*(long*)(c)){");

            Foreach(BuildTree, (node, layer) =>
            {


                if (node.IsEnd)
                {

                    //根节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"return (T)((object){GetCallerExpression(node.Value)});");

                }
                else if (node.Nodes != default)
                {

                    //父节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                }

            }, () =>
            {

                script.AppendLine("}break;");

            });


            script.Append("}}return default;}");
            return script;

        }




        public StringBuilder GetScript_GetByIndex()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override T Get<T>(){");
            script.AppendLine(@"fixed (char* c = _name)
            {
                switch(*(long*)(c)){");

            Foreach(BuildTree, (node, layer) =>
            {


                if (node.IsEnd)
                {

                    //根节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"return (T)((object){GetCallerExpression(node.Value)});");

                }
                else if (node.Nodes != default)
                {

                    //父节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                }

            }, () =>
            {

                script.AppendLine("}break;");

            });


            script.Append("}}return default;}");
            return script;

        }




        public StringBuilder GetScript_SetByName()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override void Set(string name,object value){");
            script.AppendLine(@"fixed (char* c = name)
            {
                switch(*(long*)(c)){");

            Foreach(BuildTree, (node, layer) =>
            {


                if (node.IsEnd)
                {

                    //根节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"{GetCallerExpression(node.Value)}=({node.Value.TypeName})value;");
                    script.AppendLine($"break;");

                }
                else if (node.Nodes != default)
                {

                    //父节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                }

            }, () =>
            {

                script.AppendLine("}break;");

            });

            
            script.Append("}}}");
            return script;

        }




        public StringBuilder GetScript_SetByIndex()
        {

            StringBuilder script = new StringBuilder();
            script.AppendLine("public unsafe override void Set(object value){");
            script.AppendLine(@"fixed (char* c = _name)
            {
                switch(*(long*)(c)){");

            Foreach(BuildTree, (node, layer) =>
            {


                if (node.IsEnd)
                {

                    //根节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"{GetCallerExpression(node.Value)}=({node.Value.TypeName})value;");
                    script.AppendLine($"break;");

                }
                else if (node.Nodes != default)
                {

                    //父节点
                    script.AppendLine($"case {node.PointCode}:");
                    script.AppendLine($"switch(*(long*)(c+{4 * layer})){{");

                }

            }, () =>
            {

                script.AppendLine("}break;");

            });


            script.Append("}}}");
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
