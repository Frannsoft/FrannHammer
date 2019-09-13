using System;
using System.Runtime.Serialization;

namespace FrannHammer.Domain.Contracts
{
    public class QueryParameterException : Exception
    {
        public QueryParameterException()
        {
        }

        public QueryParameterException(string message) : base(message)
        {
        }

        public QueryParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
