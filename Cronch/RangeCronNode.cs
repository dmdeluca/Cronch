using System.Collections.Generic;
using System.Linq;

namespace Cronch
{
    internal struct RangeCronNode : ICronNode
    {
        readonly int _low;
        readonly int _high;

        public RangeCronNode(int low, int high)
        {
            _low = low;
            _high = high;
        }

        public bool Match(int value)
        {
            return _low <= value && _high >= value;
        }

        public override string ToString()
        {
            return $"{_low}-{_high}";
        }
    }
}
