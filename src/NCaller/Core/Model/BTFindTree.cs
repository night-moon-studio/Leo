using System;

namespace NCaller.Core.Model
{
    public class BTFindTree<T>
    {
        public BTFindTree<T> LssTree;
        public BTFindTree<T> GtrTree;
        public T[] Nodes;
        public int CompareCode;
        public BTFindTree(Memory<T> values,int slice)
        {
            if (values.Length<=slice)
            {
                Nodes = values.Slice(0, values.Length).ToArray();
            }
            else
            {
                var left = values.Length >> 1;
                CompareCode = values.Span[left].GetHashCode();
                LssTree = new BTFindTree<T>(values.Slice(0, left), slice);
                GtrTree = new BTFindTree<T>(values.Slice(left, values.Length - left), slice);
            }
        }
    }
}
