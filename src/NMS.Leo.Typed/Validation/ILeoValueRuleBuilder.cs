namespace NMS.Leo.Typed.Validation
{
    public interface ILeoValueRuleBuilder
    {
        ILeoValueRuleBuilder AppendRule();

        ILeoValueRuleBuilder OverwriteRule();

        ILeoValueRuleBuilder Required();

        ILeoValueRuleBuilder RequiredNull();

        ILeoValueRuleBuilder Length(int min, int max);

        ILeoValueRuleBuilder MinLength(int min);

        ILeoValueRuleBuilder MaxLength(int max);
        
        ILeoValueRuleBuilder NotEqual(object value);
    }

    public interface ILeoValueRuleBuilder<T>
    {
        ILeoValueRuleBuilder<T> AppendRule();

        ILeoValueRuleBuilder<T> OverwriteRule();

        ILeoValueRuleBuilder<T> Required();

        ILeoValueRuleBuilder<T> RequiredNull();

        ILeoValueRuleBuilder<T> Length(int min, int max);

        ILeoValueRuleBuilder<T> MinLength(int min);

        ILeoValueRuleBuilder<T> MaxLength(int max);
        
        ILeoValueRuleBuilder<T> NotEqual(object value);
    }

    public interface ILeoValueRuleBuilder<T, TVal> : ILeoValueRuleBuilder<T>
    {
        new ILeoValueRuleBuilder<T, TVal> AppendRule();

        new ILeoValueRuleBuilder<T, TVal> OverwriteRule();

        new ILeoValueRuleBuilder<T, TVal> Required();

        new ILeoValueRuleBuilder<T, TVal> RequiredNull();

        new ILeoValueRuleBuilder<T, TVal> Length(int min, int max);

        new ILeoValueRuleBuilder<T, TVal> MinLength(int min);

        new ILeoValueRuleBuilder<T, TVal> MaxLength(int max);
        
        new ILeoValueRuleBuilder<T, TVal> NotEqual(object value);
    }
}