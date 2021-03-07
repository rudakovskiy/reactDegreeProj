using System;

namespace GreenHealthApi.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}