using System.Collections.Generic;
using System.Linq;

namespace Cronch
{
    internal struct OrCronNode : ICronNode
    {
        readonly ICronNode[] _crons;

        public OrCronNode(params ICronNode[] crons)
        {
            _crons = crons;
        }

        public bool Match(int value)
        {
            return _crons.Any(c => c.Match(value));
        }

        public override string ToString()
        {
            return string.Join<ICronNode>(',', _crons);
        }
    }
}
