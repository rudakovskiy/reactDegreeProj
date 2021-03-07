using System;

namespace GreenHealthApi.Exceptions
{
    public class SqlQueryException : Exception
    {
        public SqlQueryException(string message) : base(message)
        {
            
        }
    }
}