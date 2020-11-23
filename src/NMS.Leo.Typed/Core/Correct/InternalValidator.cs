using System.Collections.Generic;
using System.Linq;
using NMS.Leo.Typed.Core.Members;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct
{
    internal static class InternalValidator
    {
        public static LeoVerifyResult Valid(MemberHandler handler, List<CorrectValueRule> rules)
        {
            var len = rules.Count;
            var failures = new List<LeoVerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];
                var value = valueRule.GetValue(handler);

                ValidCore(value, valueRule, failures, nameOfExecutedRules);
            }

            return MakeResult(failures, nameOfExecutedRules);
        }

        public static LeoVerifyResult ValidOne(MemberHandler handler, List<CorrectValueRule> rules, string name)
        {
            if (rules is null || !rules.Any())
                return LeoVerifyResult.Success;

            var valueRule = rules.FirstOrDefault(r => r.Name == name);

            if (valueRule is null)
                return LeoVerifyResult.Success;

            var failures = new List<LeoVerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            ValidCore(valueRule.GetValue(handler), valueRule, failures, nameOfExecutedRules);

            return MakeResult(failures, nameOfExecutedRules);
        }

        public static LeoVerifyResult ValidOne(string name, object value, List<CorrectValueRule> rules)
        {
            if (rules is null || !rules.Any())
                return LeoVerifyResult.Success;

            var valueRule = rules.FirstOrDefault(r => r.Name == name);

            if (valueRule is null)
                return LeoVerifyResult.Success;

            var failures = new List<LeoVerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            ValidCore(value, valueRule, failures, nameOfExecutedRules);

            return MakeResult(failures, nameOfExecutedRules);
        }

        public static LeoVerifyResult ValidMany(IDictionary<string, object> keyValueCollections, List<CorrectValueRule> rules)
        {
            var len = rules.Count;
            var failures = new List<LeoVerifyFailure>();
            var nameOfExecutedRules = new List<string>();
            
            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];
                if (keyValueCollections.TryGetValue(valueRule.Name, out var value))
                    ValidCore(value, valueRule, failures, nameOfExecutedRules);
            }

            return MakeResult(failures, nameOfExecutedRules);
        }

        private static void ValidCore(object value, CorrectValueRule valueRule, List<LeoVerifyFailure> failures, List<string> nameOfExecutedRules)
        {
            var verifyValSet = valueRule
                               .ValidValue(value)
                               .Where(x => x.IsSuccess == false)
                               .ToList();

            if (verifyValSet.Any())
            {
                var count = verifyValSet.Count;

                var failure = new LeoVerifyFailure(
                    valueRule.Name,
                    $"Member {valueRule.Name} encountered {(count == 1 ? "an error" : "some errors")} during verification.",
                    value)
                {
                    Details = verifyValSet.Select(verifyVal => new LeoVerifyError {ErrorMessage = verifyVal.ErrorMessage}).ToList()
                };

                failures.Add(failure);
                nameOfExecutedRules.AddRange(verifyValSet.Select(verifyVal => verifyVal.NameOfExecutedRule));
            }
        }

        private static LeoVerifyResult MakeResult(List<LeoVerifyFailure> failures, List<string> nameOfExecutedRules)
        {
            if (failures.Any())
            {
                return new LeoVerifyResult(failures)
                {
                    NameOfExecutedRules = nameOfExecutedRules.Distinct().ToArray(),
                };
            }

            return LeoVerifyResult.Success;
        }
    }
}