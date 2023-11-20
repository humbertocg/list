using System;
using System.Runtime.Serialization;

namespace RestConsumer.Exceptions
{
	public class HttpPutException:Exception
	{
		public HttpPutException():base()
		{
		}
        public HttpPutException(string message) : base(message)
        {
        }
        public HttpPutException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public HttpPutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

