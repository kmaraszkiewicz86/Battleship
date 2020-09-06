using System;
using BattleshipShared.Exceptions;
using BattleshipShared.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipSharedTests.Extensions
{
    [TestFixture]
    public class HorizontalIndexNameExtensionTests
    {
        [TestCase(0, ExpectedResult = 'A')]
        [TestCase(5, ExpectedResult = 'F')]
        [TestCase(9, ExpectedResult = 'J')]
        public char GetHorizontalIndexName_WhenIndexIsInRange_ThenReturnValidIndexName(int index)
        {
            return index.GetHorizontalIndexName();
        }

        [Test]
        public void GetHorizontalIndexName_WhenIndexIsOutOfTheRange_ThenShouldThrowBadRequestException()
        {
            var index = 10;

            Action action = () =>
            {
                index.GetHorizontalIndexName();
            };

            action.Should().Throw<BadRequestException>("The horizontal index is out of the range");
        }
    }
}
