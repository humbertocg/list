using System;
using System.Runtime.Serialization;

namespace RestConsumer.Exceptions
{
	public class HttpGetException: Exception
	{
		public HttpGetException(): base()
		{
		}

        public HttpGetException(string message) : base(message)
        {
        }
        public HttpGetException(string message,Exception innerException) : base(message,innerException)
        {
        }
        public HttpGetException(SerializationInfo info, StreamingContext context) : base(info,context)
        {
        }
    }
}

