using System;

namespace GreenHealthApi.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}