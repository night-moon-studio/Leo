namespace NMS.Leo.Typed.Validation
{
    public interface ILeoWaitForMessageValueRuleBuilder
    {
        ILeoValueRuleBuilder WithMessage(string message);
    }

    public interface ILeoWaitForMessageValueRuleBuilder<T>
    {
        ILeoValueRuleBuilder<T> WithMessage(string message);
    }

    public interface ILeoWaitForMessageValueRuleBuilder<T, TVal>
    {
        ILeoValueRuleBuilder<T, TVal> WithMessage(string message);
    }
}