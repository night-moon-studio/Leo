using System.Text;

namespace NMS.Leo.Typed.Validation;

[Serializable]
public class LeoVerifyFailure
{
    private LeoVerifyFailure() { }

    public LeoVerifyFailure(
        string propertyName,
        string errorMessage,
        object verifiedValue = null)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        VerifiedValue = verifiedValue;
    }

    /// <summary>
    /// The name of property
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// The error message
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// The verified value
    /// </summary>
    public object VerifiedValue { get; set; }

    /// <summary>
    /// Error details
    /// </summary>
    public List<LeoVerifyError> Details { get; set; }

    /// <summary>
    /// Error code
    /// </summary>
    public long Code { get; set; }

    /// <summary>
    /// To string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(ErrorMessage);

        if (Details != null && Details.Any())
        {
            builder.AppendLine();
            builder.AppendLine("Detail(s):");
            foreach (var error in Details)
            {
                builder.AppendLine($"  - {error.ErrorMessage}");
            }
        }

        return builder.ToString();
    }

    public static LeoVerifyFailure Create(string propertyName, string errorMessage)
    {
        return new LeoVerifyFailure(propertyName, errorMessage);
    }

    public static LeoVerifyFailure Create(string propertyName, string errorMessage, object verifiedValue)
    {
        return new LeoVerifyFailure(propertyName, errorMessage, verifiedValue);
    }
}