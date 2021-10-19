namespace Cronch
{
    internal struct ValueCronNode : ICronNode
    {
        readonly int _value;

        public ValueCronNode(int value)
        {
            _value = value;
        }

        public bool Match(int value)
        {
            return value == _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
