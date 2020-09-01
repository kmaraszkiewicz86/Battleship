using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipShared.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
