using System;

namespace GreenHealthApi.Exceptions
{
    public class WrongFileTypeException : Exception
    {
        public WrongFileTypeException(string message) : base(message)
        {
            
        }
    }
}