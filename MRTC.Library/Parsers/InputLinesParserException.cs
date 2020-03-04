using System;
using System.Runtime.Serialization;

namespace MRTC.Library.Parsers
{
	[Serializable]
	public class InputLinesParserException : MRTCException
	{
		public InputLinesParserException()
		{
		}

		public InputLinesParserException(string message) : base(message)
		{
		}

		public InputLinesParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InputLinesParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}