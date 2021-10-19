using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Cronch.Tests
{
    public class ParseMatchTest
    {
        [Theory]
        [ClassData(typeof(Matches_ClassData))]
        public void Matches(string cron, DateTime match, bool shouldMatch = true)
        {
            Cron parsed = Cron.Parse(cron);
            bool isMatch = parsed.Match(match);
            Assert.Equal(isMatch, shouldMatch);
        }
    }

    internal class Matches_ClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { " * * * * * ", DateTime.Now };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 1, 0, 0) };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 2, 0, 0) };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 0, 0, 0) };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 11, 0, 0) };
            yield return new object[] { " 0 0 * * * ", new DateTime(2021, 1, 1, 0, 0, 0) };
            yield return new object[] { " 0 0 1 * * ", new DateTime(2021, 1, 1, 0, 0, 0) };
            yield return new object[] { " 0 0 2 * * ", new DateTime(2021, 1, 2, 0, 0, 0) };
            yield return new object[] { " * * * * 1 ", new DateTime(2021, 10, 18, 0, 0, 0) };
            yield return new object[] { " * * * * 2 ", new DateTime(2021, 10, 19, 0, 0, 0) };
            yield return new object[] { " * * 15 * 3 ", new DateTime(2021, 10, 15, 0, 0, 0) };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 1, 2, 0), false };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 2, 5, 0), false };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 0, 10, 0), false };
            yield return new object[] { " 0 * * * * ", new DateTime(2021, 1, 1, 11, 59, 0), false };
            yield return new object[] { " 0 0 * * * ", new DateTime(2021, 1, 1, 1, 0, 0), false };
            yield return new object[] { " 0 0 1 * * ", new DateTime(2021, 1, 1, 23, 0, 0), false };
            yield return new object[] { " * */2 * * * ", new DateTime(2021, 1, 1, 0, 0, 0) };
            yield return new object[] { " * */2 * * * ", new DateTime(2021, 1, 1, 14, 0, 0) };
            yield return new object[] { " * */2 * * * ", new DateTime(2021, 1, 1, 1, 0, 0), false };
            yield return new object[] { " * */2 * * * ", new DateTime(2021, 1, 1, 15, 0, 0), false };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
