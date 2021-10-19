namespace Cronch
{
    internal struct WildcardCronNode : ICronNode
    {
        public bool Match(int value)
        {
            return true;
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
