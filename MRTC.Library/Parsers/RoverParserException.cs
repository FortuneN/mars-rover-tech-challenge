using System;
using System.Runtime.Serialization;

namespace MRTC.Library.Parsers
{
	[Serializable]
	public class RoverParserException : MRTCException
	{
		public RoverParserException()
		{
		}

		public RoverParserException(string message) : base(message)
		{
		}

		public RoverParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected RoverParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}