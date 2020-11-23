using System;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct
{
    internal class CorrectWaitForMessageValueRuleBuilder : ILeoWaitForMessageValueRuleBuilder
    {
        private readonly CorrectValueRuleBuilder _builder;
        private readonly Func<object, bool> _func;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder builder, Func<object,bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public ILeoValueRuleBuilder WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc = o => _func.Invoke(o) 
                ? new CustomVerifyResult {VerifyResult = true} 
                : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};

            _builder.Must(realFunc);
            
            return _builder;
        }
    }

    internal class CorrectWaitForMessageValueRuleBuilder<T> : ILeoWaitForMessageValueRuleBuilder<T>
    {
        private readonly CorrectValueRuleBuilder<T> _builder;
        private readonly Func<object, bool> _func;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T> builder, Func<object,bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public ILeoValueRuleBuilder<T> WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc = o => _func.Invoke(o) 
                ? new CustomVerifyResult {VerifyResult = true} 
                : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};

            _builder.Must(realFunc);
            
            return _builder;
        }
    }

    internal class CorrectWaitForMessageValueRuleBuilder<T, TVal> : ILeoWaitForMessageValueRuleBuilder<T, TVal>
    {
        private readonly CorrectValueRuleBuilder<T, TVal> _builder;
        private readonly Func<object, bool> _func;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T, TVal> builder, Func<object,bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public ILeoValueRuleBuilder<T, TVal> WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc = o => _func.Invoke(o) 
                ? new CustomVerifyResult {VerifyResult = true} 
                : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};

            _builder.Must(realFunc);
            
            return _builder;
        }
    }
}