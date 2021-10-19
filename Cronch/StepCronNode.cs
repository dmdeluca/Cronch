namespace Cronch
{
    internal struct StepCronNode : ICronNode
    {
        readonly ICronNode _node;
        readonly int _step;

        public StepCronNode(ICronNode node, int step)
        {
            _node = node;
            _step = step;
        }

        public bool Match(int value)
        {
            return _node.Match(value) && value % _step == 0;
        }

        public override string ToString()
        {
            return $"{_node}/{_step}";
        }
    }
}
