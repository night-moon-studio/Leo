namespace NMS.Leo.Typed.Validation;

public class LeoValidationException : Exception
{
    public LeoValidationException(LeoVerifyResult result) : base(result.ToString())
    {
        VerifyResult = result;
    }

    public LeoVerifyResult VerifyResult { get; }
}