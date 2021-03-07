using System;

namespace GreenHealthApi.Exceptions
{
    public class ImageDoesNotExistException : Exception
    {
        public ImageDoesNotExistException(string message) : base(message)
        {
            
        }
    }
}