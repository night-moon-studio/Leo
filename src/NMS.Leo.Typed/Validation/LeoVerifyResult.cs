using System;
using System.Collections.Generic;
using System.Linq;

namespace NMS.Leo.Typed.Validation
{
    [Serializable]
    public class LeoVerifyResult
    {
        private readonly List<LeoVerifyFailure> _failures;

        private bool _success;

        private bool InternalSuccess
        {
            get => _success && _failures.Count == 0;
            set
            {
                _success = value;
                if (value) _failures.Clear();
            }
        }

        public LeoVerifyResult()
        {
            _failures = new List<LeoVerifyFailure>();
        }

        public LeoVerifyResult(IEnumerable<LeoVerifyFailure> failures)
        {
            _failures = failures.Where(f => f != null).ToList();
        }

        /// <summary>
        /// Name collection of executed validation rules. 
        /// </summary>
        public string[] NameOfExecutedRules { get; internal set; }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid => InternalSuccess;

        /// <summary>
        /// A collection of errors
        /// </summary>
        public IList<LeoVerifyFailure> Errors
        {
            get
            {
                if (_success)
                    return _failures.AsReadOnly();
                return _failures;
            }
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            return string.Join(separator, _failures.Select(f => f.ToString()));
        }

        #region Success

        public static LeoVerifyResult Success { get; } = new LeoVerifyResult {InternalSuccess = true};

        public static bool IsSuccess(LeoVerifyResult result)
        {
            return result?.InternalSuccess ?? false;
        }

        #endregion

        #region Base

        public static bool operator ==(LeoVerifyResult left, LeoVerifyResult right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            if (left.InternalSuccess && right.InternalSuccess) return true;
            return left.Equals(right);
        }

        public static bool operator !=(LeoVerifyResult left, LeoVerifyResult right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is LeoVerifyResult result)
                return Equals(result);

            return false;
        }

        protected bool Equals(LeoVerifyResult other)
        {
            if (other is null) return false;
            if (other.InternalSuccess && InternalSuccess) return true;

            return Equals(_failures, other._failures) &&
                   InternalSuccess == other.InternalSuccess &&
                   Equals(NameOfExecutedRules, other.NameOfExecutedRules);
        }

        public override int GetHashCode()
        {
            //TODO in next version, we can only use `HashCode.Combine(_failures);`
#if NETCOREAPP2_0
            return _failures.GetHashCode();
#else
            return HashCode.Combine(_failures);
#endif
        }

        #endregion

        public void Raise()
        {
            if (!IsValid)
            {
                throw new LeoValidationException(this);
            }
        }
    }
}