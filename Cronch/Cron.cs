using System;
using System.Linq;
using System.Xml;

namespace Cronch
{
    public struct Cron
    {
        private const int _minute = 0;
        private const int _hour = 1;
        private const int _month = 3;
        private const int _day = 2;
        private const int _dow = 4;
        private static readonly ICronNode _anyCron = new WildcardCronNode();
        private readonly ICronNode[] _nodes;

        private Cron(ICronNode[] nodes)
        {
            _nodes = nodes;
        }

        public static Cron Parse(string cron)
        {
            var nodes = cron.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(ConvertToCron)
                .ToArray();

            return new Cron(nodes);
        }

        public bool Match(DateTime dateTime)
        {
            return _nodes[_minute].Match(dateTime.Minute)
                && _nodes[_hour].Match(dateTime.Hour)
                && _nodes[_month].Match(dateTime.Month)
                && GetDayValid(dateTime);
        }

        private bool GetDayValid(DateTime dateTime)
        {
            bool dayMatch = _nodes[_day].Match(dateTime.Day);
            bool dowMatch = _nodes[_dow].Match((int)dateTime.DayOfWeek);
            if (_nodes[_day] is WildcardCronNode || _nodes[_dow] is WildcardCronNode)
                return dayMatch && dowMatch;
            return dayMatch || dowMatch;
        }

        public override string ToString()
        {
            return string.Join<ICronNode>(' ', _nodes);
        }

        private static ICronNode ConvertToCron(string cronItem)
        {
            if (cronItem == "*")
                return _anyCron;

            if (cronItem.Contains("/"))
            {
                var split = cronItem.Split("/");
                return new StepCronNode(ConvertToCron(split[0]), int.Parse(split[1]));
            }

            if (cronItem.Contains("-"))
                return ConvertToRangeCron(cronItem);

            if (int.TryParse(cronItem, out var value))
                return new ValueCronNode(value);

            return cronItem.Split(",")
                .Select(ConvertToCron)
                .Aggregate((c1, c2) => new OrCronNode(c1, c2));
        }

        private static ICronNode ConvertToRangeCron(string cronItem)
        {
            var items = cronItem.Split("-");
            var low = int.Parse(items[0]);
            var high = int.Parse(items[1]);
            return new RangeCronNode(low, high);
        }
    }
}
