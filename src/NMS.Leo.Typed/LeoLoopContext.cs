namespace NMS.Leo.Typed
{
    public class LeoLoopContext
    {
        public string Name { get; }

        public object Value { get; }

        public LeoMember Metadata { get; }

        public int Index { get; }

        public LeoLoopContext(string name, object value, LeoMember member, int index)
        {
            Name = name;
            Value = value;
            Metadata = member;
            Index = index;
        }
    }
}