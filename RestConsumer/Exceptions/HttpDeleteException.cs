using System;
using System.Runtime.Serialization;

namespace RestConsumer.Exceptions
{
	public class HttpDeleteException: Exception
	{
		public HttpDeleteException(): base()
		{
		}
        public HttpDeleteException(string message) : base(message)
        {
        }
        public HttpDeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public HttpDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

