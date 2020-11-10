namespace NMS.Leo.Typed
{
    public interface ILeoLooper
    {
        ILeoVisitor BackToVisitor();

        void Fire();

        ILeoVisitor FireAndBack();
    }

    public interface ILeoLooper<T>
    {
        ILeoVisitor<T> BackToVisitor();

        void Fire();

        ILeoVisitor<T> FireAndBack();
    }
}