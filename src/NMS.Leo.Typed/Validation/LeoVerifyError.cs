namespace NMS.Leo.Typed.Validation;

[Serializable]
public class LeoVerifyError
{
    public string ErrorMessage { get; internal set; }

    public ValidatorType ViaValidatorType { get; internal set; } = ValidatorType.BuildIn;

    public string ValidatorName { get; internal set; } = "LeoBuildInValidator";
}