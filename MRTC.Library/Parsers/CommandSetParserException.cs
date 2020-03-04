using System;
using System.Runtime.Serialization;

namespace MRTC.Library.Parsers
{
	[Serializable]
	public class CommandSetParserException : MRTCException
	{
		public CommandSetParserException()
		{
		}

		public CommandSetParserException(string message) : base(message)
		{
		}

		public CommandSetParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CommandSetParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}