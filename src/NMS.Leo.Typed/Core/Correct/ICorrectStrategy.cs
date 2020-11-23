using System.Collections.Generic;

namespace NMS.Leo.Typed.Core.Correct
{
    internal interface ICorrectStrategy
    {
        IEnumerable<CorrectValueRuleBuilder> GetValueRuleBuilders();
    }

    internal interface ICorrectStrategy<T>
    {
        IEnumerable<CorrectValueRuleBuilder<T>> GetValueRuleBuilders();
    }
}