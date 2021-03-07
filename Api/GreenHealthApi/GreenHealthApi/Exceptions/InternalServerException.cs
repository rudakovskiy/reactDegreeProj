using System;

namespace GreenHealthApi.Exceptions
{
    public class InternalServerException: Exception
    {
        public InternalServerException(string message, Exception e)
            : base(message)
        {
        }
    }
}