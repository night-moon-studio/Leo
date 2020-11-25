using System;
using System.Collections;
using System.Linq;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueAnyToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueAnyToken";

        public static int[] _mutuallyExclusiveFlags = {90121};

        private readonly Func<object, bool> _func;

        public ValueAnyToken(LeoMember member, Func<object, bool> func) : base(member)
        {
            _func = func;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Any;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};
            var flag = false;

            if (value is ICollection collection)
            {
                if (collection.Cast<object>().Any(one => _func.Invoke(one)))
                {
                    flag = true;
                }

                if (!flag)
                {
                    UpdateVal(val, value);
                }
            }
            else
            {
                UpdateVal(val, value, "The type is not a collection or an array, and an exception occurs when using AntToken.");
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = message ?? "There are no members that meet the conditions in the array or collection.";
        }

        public override string ToString() => NAME;
    }
}