using System;
using System.Runtime.Serialization;

namespace RestConsumer.Exceptions
{
	public class HttpPatchException: Exception
	{
		public HttpPatchException():base()
		{
		}
        public HttpPatchException(string message) : base(message)
        {
        }
        public HttpPatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public HttpPatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

