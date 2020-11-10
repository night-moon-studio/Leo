using System;
using System.Collections.Generic;
using System.Linq;
using NMS.Leo;

namespace Leo.Typed.Core
{
    internal class LeoMemberHandler
    {
        private readonly DictBase _handler;
        private readonly Type _sourceType;
        private readonly List<string> _memberNames;

        public LeoMemberHandler(DictBase handler, Type sourceType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            _memberNames = handler.GetMemberNames().ToList();
        }

        public LeoMember this[string name] => _handler.GetMember(name);
        
        public LeoMember GetMember(string name) => _handler.GetMember(name);

        public IEnumerable<LeoMember> GetMembers() => _handler.GetMembers();

        public bool Contains(string name) => _memberNames.Contains(name);

        public IEnumerable<string> GetNames() => _memberNames;
    }
}