using System;
using System.Runtime.Serialization;

namespace RestConsumer.Exceptions
{
	public class HttpPostException : Exception
	{
		public HttpPostException():base()
		{
		}
        public HttpPostException(string message) : base(message)
        {
        }
        public HttpPostException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public HttpPostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

