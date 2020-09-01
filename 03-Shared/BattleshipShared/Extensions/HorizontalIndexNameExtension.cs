using System;
using System.Collections.Generic;
using System.Text;
using BattleshipShared.Exceptions;

namespace BattleshipShared.Extensions
{
    public static class HorizontalIndexNameExtension
    {
        private static char[] horizontalIndexNames = "ABCDEFGHIJ".ToCharArray();

        public static char GetHorizontalIndexName(this int horizontalIndex)
        {
            if (horizontalIndex >= horizontalIndexNames.Length)
            {
                throw new BadRequestException("The horizontal index is out of the range");
            }

            return horizontalIndexNames[horizontalIndex];
        }
    }
}
