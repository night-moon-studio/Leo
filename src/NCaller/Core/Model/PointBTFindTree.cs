using NCaller.Constraint;
using NCaller.ExtensionAPI;
using System.Collections.Generic;

namespace NCaller.Core.Model
{
    public class PointBTFindTree<T> where T: PointNameStandard
    {
        public readonly List<PointBTFindTree<T>> Nodes;
        public readonly T Value;
        public readonly bool IsEnd;
        public readonly long PointCode;

        public PointBTFindTree(T value, int layer = 0)
        {

            Value = value;
            IsEnd = true;
            PointCode = value.PointName.GetLong(layer);

        }




        public PointBTFindTree(IEnumerable<T> values, int layer = 0, long pCode = 0)
        {

            PointCode = pCode;
            Nodes = new List<PointBTFindTree<T>>();
            var valuesDict = new Dictionary<long, List<T>>();
            var codeSets = new HashSet<long>();


            foreach (var item in values)
            {

                if (item.PointName.Length < layer * 4)
                {
                    Nodes.Add(new PointBTFindTree<T>(item, layer));
                }
                else
                {

                    long code = item.PointName.GetLong(layer);
                    if (!codeSets.Contains(code))
                    {

                        codeSets.Add(code);
                        valuesDict[code] = new List<T>();

                    }
                    valuesDict[code].Add(item);

                }

            }


            foreach (var item in valuesDict)
            {

                if (item.Value.Count == 1)
                {
                    Nodes.Add(new PointBTFindTree<T>(item.Value[0], layer));
                }
                else
                {
                    Nodes.Add(new PointBTFindTree<T>(item.Value, layer + 1, item.Key));
                }

            }

        }

    }

}
