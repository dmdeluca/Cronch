namespace Cronch
{
    internal interface ICronNode
    {
        bool Match(int dateTime);
    }
}
