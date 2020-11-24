using NMS.Leo.Typed.Validation;

namespace NCallerUT.Model
{
    public class NormalNiceActValidationStrategy : LeoValidationStrategy
    {
        public NormalNiceActValidationStrategy() : base(typeof(NiceAct))
        {
            RuleFor("Name").NotEmpty().MinLength(4).MaxLength(15);
        }
    }

    public class GenericNiceActValidationStrategy : LeoValidationStrategy<NiceAct>
    {
        public GenericNiceActValidationStrategy()
        {
            RuleFor(x => x.Name).NotEmpty().MinLength(4).MaxLength(15);
        }
    }
}