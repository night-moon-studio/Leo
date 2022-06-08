namespace NMS.Leo.Typed.Validation;

public interface ILeoValidationStrategy
{
    Type SourceType { get; }
}

public interface ILeoValidationStrategy<T>
{
    Type SourceType { get; }
}